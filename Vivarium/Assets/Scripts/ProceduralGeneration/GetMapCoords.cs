using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Utility to get the coordinates of tiles from a level
/// </summary>
public class GetMapCoords : MonoBehaviour
{
    private Grid<Tile> _grid;

    /// <summary>
    /// Gets a Tile array from the TileGridController
    /// </summary>
    /// <returns>Tile Array</returns>
    public Tile[,] GetGrid()
    {
        return TileGridController.Instance.GetGrid().GetGrid();
    }

    /// <summary>
    /// Gets the grid object from the Tile Grid Controller
    /// </summary>
    /// <returns>Grid object</returns>
    public Grid<Tile> GetGridObject()
    {
        return TileGridController.Instance.GetGrid();
    }

    /// <summary>
    /// Gets the coordinates for all the water coords.
    /// </summary>
    /// <returns>A list of lists. The inner list represent a coordinate, and the outer list holds those coordinates.</returns>
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

    /// <summary>
    /// Converts a list of tiles to a list of coordinates
    /// </summary>
    /// <param name="tiles">A list of tiles</param>
    /// <returns>A list of lists. The inner list represent a coordinate, and the outer list holds those coordinates.</returns>
    public List<List<int>> TilesToCoords(List<Tile> tiles)
    {
        var returnList = new List<List<int>>();
        for (int i = 0; i < tiles.Count; i +=1)
        {
            var newEntry = new List<int>() { tiles[i].GridX, tiles[i].GridY };
            returnList.Add(newEntry);
        }

        return returnList;
    }

    /// <summary>
    /// Takes two lists of coordinates and returns the a new list of coordinates with the second list of coordinates removed from the first.
    /// </summary>
    /// <param name="mainCoords">The list of coordinates to filter from</param>
    /// <param name="filterCoords">The list of coordinates to filter</param>
    /// <returns>A list of lists. The inner list represent a coordinate, and the outer list holds those coordinates.</returns>
    public List<List<int>> FilterCoords(List<List<int>> mainCoords, List<List<int>> filterCoords)
    {
        var returnList = new List<List<int>>();
        foreach (var coord in mainCoords)
        {
            var isFilterCoord = false;
            foreach(var filterCoord in filterCoords)
            {
                if (coord[0] == filterCoord[0] && coord[1] == filterCoord[1])
                {
                    isFilterCoord = true;
                    break;
                }
            }
            if (isFilterCoord == false)
            {
                returnList.Add(coord);
            }

        }

        return returnList;
    }

    /// <summary>
    /// Gets the coordinates for all the tiles in the level.
    /// </summary>
    /// <returns>A list of lists. The inner list represent a coordinate, and the outer list holds those coordinates.</returns>
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



    /// <summary>
    /// Gets the coordinates for all the grass tiles next to a water tile.
    /// </summary>
    /// <returns>A list of lists. The inner list represent a coordinate, and the outer list holds those coordinates.</returns>
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


    /// <summary>
    /// Returns the width of the level
    /// </summary>
    public int GetWidth()
    {
        return TileGridController.Instance.GridWidth;
    }

    /// <summary>
    /// Returns the height of the level
    /// </summary>
    public int GetHeight()
    {
        return TileGridController.Instance.GridHeight;
    }

    /// <summary>
    /// Gets the coordinates for all the obstacle tiles.
    /// </summary>
    /// <returns>A list of lists. The inner list represent a coordinate, and the outer list holds those coordinates.</returns>
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

    /// <summary>
    /// Returns a list of grouped coordinates indicating the set of vertically adjacent obstacle tiles
    /// </summary>
    /// <param name="startSection">The z tile coordinate to start looking for vertically adjacent obstacle tiles</param>
    /// <param name="stopSection">The z tile coordinate to stop looking for vertically adjacent obstacle tiles</param>
    /// <param name="verticalStart">The x tile coordinate to start looking for vertically adjacent obstacle tiles</param>
    /// <param name="verticalStop">The x tile coordinate to stop looking for vertically adjacent obstacle tiles</param>
    /// <returns></returns>
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


    /// <summary>
    /// Returns a list of grouped coordinates indicating the set of horizontally adjacent obstacle tiles
    /// </summary>
    /// <param name="startSection">The z tile coordinate to start looking for horizontally adjacent obstacle tiles</param>
    /// <param name="stopSection">The z tile coordinate to stop looking for horizontally adjacent obstacle tiles</param>
    /// <param name="verticalStart">The x tile coordinate to start looking for horizontally adjacent obstacle tiles</param>
    /// <param name="verticalStop">The x tile coordinate to stop looking for horizontally adjacent obstacle tiles</param>
    /// <returns></returns>
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


    /// <summary>
    /// Gets the coordinates for all the grass tiles.
    /// </summary>
    /// <returns>A list of lists. The inner list represent a coordinate, and the outer list holds those coordinates.</returns>
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
