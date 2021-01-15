using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMapCoords : MonoBehaviour
{
    private Grid<Tile> _grid;
    
    // Start is called before the first frame update
    void Start()
    {
        //var tiles = _grid.GetGrid();
        //var myTile = tiles[0, 0];
        //for (int x = 0; x < tiles.GetLength(0); x += 1){
        //    for (int z = 0; z < tiles.GetLength(1); z += 1)
        //    {
        //        Debug.Log(tiles[x, z].Type);
        //    }
        //}
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
        Debug.Log(tiles);
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


    public List<List<int>> GetObstacleCoords()
    {
        var returnList = new List<List<int>>();

        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();
        Debug.Log(tiles);
        var myTile = tiles[0, 0];
        for (int x = 0; x < tiles.GetLength(0); x += 1)
        {
            for (int z = 0; z < tiles.GetLength(1); z += 1)
            {
                if (tiles[x, z].Type == TileType.Obstacle)
                {
                    returnList.Add(new List<int> { x, z });
                }
            }
        }
        return returnList;

    }


    public List<List<int>> GetGrassTiles()
    {
        var returnList = new List<List<int>>();

        _grid = TileGridController.Instance.GetGrid();
        var tiles = _grid.GetGrid();
        Debug.Log(tiles);
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


}
