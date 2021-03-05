using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetMapCoords : MonoBehaviour
{
    private Grid<Tile> _grid;

    // Start is called before the first frame update
    void Start()
    {

    }

    public Tile[,] GetGrid()
    {
        return TileGridController.Instance.GetGrid().GetGrid();
    }

    public Grid<Tile> GetGridObject()
    {
        return TileGridController.Instance.GetGrid();
    }

    public List<List<int>> GetWaterCoords()
    {
        var returnList = new List<List<int>>();

        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();
        var myTile = tiles[0, 0];
        for (int x = 0; x < tiles.GetLength(0); x += 1)
        {
            for (int z = 0; z < tiles.GetLength(1); z += 1)
            {
                if (tiles[x, z].Type == TileType.Water)
                {
                    returnList.Add(new List<int> { x, z });
                }
            }
        }
        return returnList;

    }

    public List<List<int>> GetAllCoords()
    {
        var returnList = new List<List<int>>();
        var width = GetWidth();
        var height = GetHeight();

        for (int x = 0; x < width; x += 1)
        {
            for (int z = 0; z < height; z += 1)
            {
                returnList.Add(new List<int> { x, z });
            }
        }
        return returnList;
    }

    public List<List<int>> GetOuterBorderCoords()
    {
        var returnList = new List<List<int>>();

        return returnList;
    }

    public List<List<int>> GetCoastalCoords()
    {
        var width = GetWidth();
        var height = GetHeight();

        var returnList = new List<List<int>>();

        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();
        var myTile = tiles[0, 0];
        for (int x = 0; x < tiles.GetLength(0); x += 1)
        {
            for (int z = 0; z < tiles.GetLength(1); z += 1)
            {
                if (tiles[x, z].Type == TileType.Grass)
                {
                    var minusZ = z - 1;
                    if (minusZ < 0)
                    {
                        minusZ = 0;
                    }
                    var plusZ = z + 1;
                    if (plusZ >= tiles.GetLength(1))
                    {
                        plusZ = tiles.GetLength(1) - 1;
                    }

                    var minusX = x - 1;
                    if (minusX < 0)
                    {
                        minusX = 0;
                    }
                    var plusX = x + 1;
                    if (plusX >= tiles.GetLength(0))
                    {
                        plusX = tiles.GetLength(0) - 1;
                    }

                    if (tiles[plusX, z].Type == TileType.Water ||
                        tiles[minusX, z].Type == TileType.Water ||
                        tiles[x, plusZ].Type == TileType.Water ||
                        tiles[x, minusZ].Type == TileType.Water ||
                        tiles[minusX, minusZ].Type == TileType.Water ||
                        tiles[plusX, plusZ].Type == TileType.Water ||
                        tiles[plusX, minusZ].Type == TileType.Water ||
                        tiles[minusX, plusZ].Type == TileType.Water)
                    {
                        returnList.Add(new List<int> { x, z });
                    }
                }
            }
        }
        return returnList;
    }

    public int GetWidth()
    {
        return TileGridController.Instance.GridWidth;
    }

    public int GetHeight()
    {
        return TileGridController.Instance.GridHeight;
    }

    public List<List<int>> GetObstacleCoords()
    {
        var returnList = new List<List<int>>();

        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();

        for (int x = 0; x < tiles.GetLength(0); x += 1)
        {
            for (int z = 0; z < tiles.GetLength(1); z += 1)
            {
                if (TileIsObstacle(tiles[x, z]))
                {
                    returnList.Add(new List<int> { x, z });
                }
            }
        }
        return returnList;

    }


    public List<List<List<int>>> GetVerticalGroupObjects(int startSection, int stopSection, int verticalStart, int verticalStop)
    {
        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();

        var returnList = new List<List<List<int>>>();

        var groupedObstacle = new List<List<int>>();



        for (int x = verticalStart; x < verticalStop; x += 1)
        {
            for (int z = startSection; z < stopSection; z += 1)
            {

                if (TileIsObstacle(tiles[x, z]))
                {
                    groupedObstacle.Add(new List<int> { x, z });

                }
                else
                {
                    if (groupedObstacle.Count > 0)
                    {
                        var addGroup = new List<List<int>>(groupedObstacle);
                        returnList.Add(addGroup);
                        groupedObstacle = new List<List<int>>();
                    }
                }
            }
            if (groupedObstacle.Count > 0)
            {
                var addGroup = new List<List<int>>(groupedObstacle);
                returnList.Add(addGroup);
                groupedObstacle = new List<List<int>>();
            }
        }


        return returnList;
    }


    public List<List<List<int>>> GetHorizontalGroupObjects(int startSection, int stopSection, int verticalStart, int verticalStop)
    {

        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();

        var returnList = new List<List<List<int>>>();

        if (stopSection > tiles.GetLength(1))
        {
            Debug.Log("BAD REQUESTED SECTION IS TOO LARGE");
            return returnList;
        }



        var groupedObstacle = new List<List<int>>();

        for (int z = startSection; z < stopSection; z += 1)
        {
            for (int x = verticalStart; x < verticalStop; x += 1)
            {
                if (TileIsObstacle(tiles[x, z]))
                {
                    groupedObstacle.Add(new List<int> { x, z });

                }
                else
                {
                    if (groupedObstacle.Count > 0)
                    {
                        var addGroup = new List<List<int>>(groupedObstacle);
                        returnList.Add(addGroup);
                        groupedObstacle = new List<List<int>>();
                    }
                }
            }
            if (groupedObstacle.Count > 0)
            {
                var addGroup = new List<List<int>>(groupedObstacle);
                returnList.Add(addGroup);
                groupedObstacle = new List<List<int>>();
            }
        }

        return returnList;
    }


    public List<List<int>> GetGrassTiles()
    {
        var returnList = new List<List<int>>();

        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();
        var myTile = tiles[0, 0];
        for (int x = 0; x < tiles.GetLength(0); x += 1)
        {
            for (int z = 0; z < tiles.GetLength(1); z += 1)
            {
                if (tiles[x, z].Type == TileType.Grass)
                {
                    returnList.Add(new List<int> { x, z });
                }
            }
        }
        return returnList;

    }

    private bool TileIsObstacle(Tile tile)
    {
        return tile.Type == TileType.Obstacle && tile.SpawnType != TileSpawnType.TreasureChest;
    }
}
