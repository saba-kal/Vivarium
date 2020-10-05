using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    public event EventHandler<OnGridCellChangedEventArgs> OnGridCellChanged;
    public class OnGridCellChangedEventArgs : EventArgs
    {
        public int X;
        public int Y;
    }

    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;

    private T[,] _grid;

    public Grid(
        int width,
        int height,
        float cellSize,
        Vector3 originPosition,
        Func<int, int, Grid<T>, T> createGridObject)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _grid = new T[width, height];
        for (var x = 0; x < _grid.GetLength(0); x++)
        {
            for (var y = 0; y < _grid.GetLength(1); y++)
            {
                _grid[x, y] = createGridObject(x, y, this);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * _cellSize + _originPosition;
    }

    public Vector3 GetWorldPositionCentered(int x, int y)
    {
        var cellWorldPosition = GetWorldPosition(x, y);
        cellWorldPosition.x += _cellSize * 0.5f;
        cellWorldPosition.z += _cellSize * 0.5f;
        return cellWorldPosition;
    }

    public void GetGridCoordinates(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
    }

    public Vector3 ConvertToGridCellPosition(Vector3 worldPosition)
    {
        GetGridCoordinates(worldPosition, out var x, out var y);
        var cellWorldPosition = GetWorldPosition(x, y);
        cellWorldPosition.x += _cellSize * 0.5f;
        cellWorldPosition.z += _cellSize * 0.5f;
        return cellWorldPosition;
    }

    public T[,] GetGrid()
    {
        return _grid;
    }

    public void SetValue(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _grid[x, y] = value;
        }
    }

    public void SetValue(Vector3 worldPosition, T value)
    {
        GetGridCoordinates(worldPosition, out var x, out var y);
        SetValue(x, y, value);
    }

    public T GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _grid[x, y];
        }

        return default;
    }

    public T GetValue(Vector3 worldPosition)
    {
        GetGridCoordinates(worldPosition, out var x, out var y);
        return GetValue(x, y);
    }

    public void TriggerGridCellChange(int x, int y)
    {
        OnGridCellChanged?.Invoke(this, new OnGridCellChangedEventArgs { X = x, Y = y });
    }

    public void PreviewGrid()
    {
        for (var x = 0; x < _grid.GetLength(0); x++)
        {
            for (var y = 0; y < _grid.GetLength(1); y++)
            {
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1));
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y));
            }
        }
        Gizmos.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height));
        Gizmos.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height));
    }


    public List<T> GetLine(Vector3 fromPosition, Vector3 toPosition)
    {
        var points = new List<T>();

        var p0 = new Vector2(fromPosition.x, fromPosition.z);
        var p1 = new Vector2(toPosition.x, toPosition.z);
        var N = DiagonalDistance(p0, p1);

        for (var step = 0; step <= N; step++)
        {
            var t = N == 0 ? 0f : step / N;
            var lerpedPoint = LerpPoint(p0, p1, t);
            GetGridCoordinates(new Vector3(lerpedPoint.x, 0f, lerpedPoint.y), out var gridX, out var gridY);

            var gridObj = GetValue(gridX, gridY);
            if (gridObj != null)
            {
                points.Add(gridObj);
            }
        }
        return points;
    }

    private float DiagonalDistance(Vector2 p0, Vector2 p1)
    {
        var dx = p1.x - p0.x;
        var dy = p1.y - p0.y;
        return Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy));
    }

    private Vector2 LerpPoint(Vector2 p0, Vector2 p1, float t)
    {
        return new Vector2(Lerp(p0.x, p1.x, t),
                         Lerp(p0.y, p1.y, t));
    }

    private float Lerp(float start, float end, float t)
    {
        return start + t * (end - start);
    }

    public float GetCellSize()
    {
        return _cellSize;
    }

    public Vector3 GetOrigin()
    {
        return _originPosition;
    }
}
