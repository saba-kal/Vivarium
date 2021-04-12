using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles all game grid logic.
/// </summary>
/// <typeparam name="T">The type for each grid square.</typeparam>
public class Grid<T>
{
    /// <summary>
    /// Event gets called every time a grid cell changes.
    /// </summary>
    public event EventHandler<OnGridCellChangedEventArgs> OnGridCellChanged;

    /// <summary>
    /// Contains method arguments for when a grid cell changes.
    /// </summary>
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

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="width">Width of the grid.</param>
    /// <param name="height">Height of the grid.</param>
    /// <param name="cellSize">The height and width of each grid cell.</param>
    /// <param name="originPosition">The center position of the grid.</param>
    /// <param name="createGridObject">Callback function for initializing each grid cell.</param>
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

    /// <summary>
    /// Gets the world position of a grid cell using grid coordinates.
    /// </summary>
    /// <param name="x">The x position of the grid cell.</param>
    /// <param name="y">The y position of the grid cell.</param>
    /// <returns>World position of the grid cell.</returns>
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * _cellSize + _originPosition;
    }

    /// <summary>
    /// Gets the centered world position of a grid cell using grid coordinates.
    /// </summary>
    /// <param name="x">The x position of the grid cell.</param>
    /// <param name="y">The y position of the grid cell.</param>
    /// <returns>World position of the grid cell.</returns>
    public Vector3 GetWorldPositionCentered(int x, int y)
    {
        var cellWorldPosition = GetWorldPosition(x, y);
        cellWorldPosition.x += _cellSize * 0.5f;
        cellWorldPosition.z += _cellSize * 0.5f;
        return cellWorldPosition;
    }

    /// <summary>
    /// Gets the grid coordinates of a grid cell using world position.
    /// </summary>
    /// <param name="worldPosition">The world position.</param>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    public void GetGridCoordinates(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
    }

    /// <summary>
    /// Converts a slightly off center world position to be centered in a grid cell.
    /// </summary>
    /// <param name="worldPosition">The slightly off center world position.</param>
    /// <returns>Centered world position.</returns>
    public Vector3 ConvertToGridCellPosition(Vector3 worldPosition)
    {
        GetGridCoordinates(worldPosition, out var x, out var y);
        var cellWorldPosition = GetWorldPosition(x, y);
        cellWorldPosition.x += _cellSize * 0.5f;
        cellWorldPosition.z += _cellSize * 0.5f;
        return cellWorldPosition;
    }

    /// <summary>
    /// Gets the 2D grid array.
    /// </summary>
    /// <returns>2D array of grid cells.</returns>
    public T[,] GetGrid()
    {
        return _grid;
    }

    /// <summary>
    /// Sets a specific grid cell using grid coordinates.
    /// </summary>
    /// <param name="x">The x coordinate of the grid cell.</param>
    /// <param name="y">The y coordinate of the grid cell.</param>
    /// <param name="value">The value to set.</param>
    public void SetValue(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _grid[x, y] = value;
        }
    }

    /// <summary>
    /// Sets a specific grid cell using world coordinates.
    /// </summary>
    /// <param name="worldPosition">The world coordinates.</param>
    /// <param name="value">The value to set.</param>
    public void SetValue(Vector3 worldPosition, T value)
    {
        GetGridCoordinates(worldPosition, out var x, out var y);
        SetValue(x, y, value);
    }

    /// <summary>
    /// Gets a grid cell value using grid coordinates.
    /// </summary>
    /// <param name="x">The x coordinate of the grid cell.</param>
    /// <param name="y">The y coordinate of the grid cell.</param>
    /// <returns>The value located in the given x and y positions.</returns>
    public T GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _grid[x, y];
        }

        return default;
    }

    /// <summary>
    /// Gets a grid cell value using world position.
    /// </summary>
    /// <param name="worldPosition">The world coordinates.</param>
    /// <returns>The value located in the given world positions.</returns>
    public T GetValue(Vector3 worldPosition)
    {
        GetGridCoordinates(worldPosition, out var x, out var y);
        return GetValue(x, y);
    }

    /// <summary>
    /// Triggers the grid cell change event.
    /// </summary>
    /// <param name="x">x coordinate of the cell that changed.</param>
    /// <param name="y">y coordinate of the cell that changed.</param>
    public void TriggerGridCellChange(int x, int y)
    {
        OnGridCellChanged?.Invoke(this, new OnGridCellChangedEventArgs { X = x, Y = y });
    }

    /// <summary>
    /// Previews the grid using Gizmo lines.
    /// </summary>
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

    /// <summary>
    /// Gets a line on the grid.
    /// </summary>
    /// <param name="fromPosition">Start world position of the line.</param>
    /// <param name="toPosition">End world position of the line.</param>
    /// <returns>List of grid cells that form the line.</returns>
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

    /// <summary>
    /// Gets the width and height of any given cell.
    /// </summary>
    /// <returns>The width and height of a grid cell.</returns>
    public float GetCellSize()
    {
        return _cellSize;
    }

    /// <summary>
    /// Gets the grid's origin position.
    /// </summary>
    /// <returns>The grid's origin position.</returns>
    public Vector3 GetOrigin()
    {
        return _originPosition;
    }

    /// <summary>
    /// Gets grid cells adjacent to the given x and y coordinates.
    /// </summary>
    /// <param name="x">The x coordinate of the grid cell.</param>
    /// <param name="y">The y coordinate of the grid cell.</param>
    /// <param name="includeDiagonals">Whether or not to also get diagonal grid cells.</param>
    /// <returns>List of adjacent grid cells.</returns>
    public List<T> GetAdjacentTiles(int x, int y, bool includeDiagonals = false)
    {
        var adjacentTiles = new List<T>();

        var preCheckedTiles = new List<T>()
        {
            GetValue(x, y + 1), //Top
            GetValue(x + 1, y), //Right
            GetValue(x, y - 1), //Bottom
            GetValue(x - 1, y) //Left
        };

        if (includeDiagonals)
        {
            var diagonalTiles = new List<T>()
            {
                GetValue(x + 1, y + 1), //Top-right
                GetValue(x + 1, y - 1), //Bottom-right
                GetValue(x - 1, y - 1), //Bottom-left
                GetValue(x - 1, y + 1) //Top-left
            };

            preCheckedTiles = preCheckedTiles.Concat(diagonalTiles).ToList();
        }

        foreach (var possibleAdjacentTile in preCheckedTiles)
        {
            if (possibleAdjacentTile != null)
            {
                adjacentTiles.Add(possibleAdjacentTile);
            }
        }

        return adjacentTiles;
    }
}
