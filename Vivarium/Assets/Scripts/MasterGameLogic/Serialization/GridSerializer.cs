using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridSerializer
{

    public static string Serialize(Grid<Tile> grid)
    {
        var gridArray = grid.GetGrid();
        var gridJObject = new GridJObject
        {
            Width = gridArray.GetLength(0),
            Height = gridArray.GetLength(1),
            TileSize = grid.GetCellSize(),
            OriginX = grid.GetOrigin().x,
            OriginY = grid.GetOrigin().y,
            OriginZ = grid.GetOrigin().z,
            Rows = new List<GridRowJObject>()
        };

        for (var x = 0; x < gridArray.GetLength(0); x++)
        {
            var gridRowJObject = new GridRowJObject();
            gridRowJObject.Tiles = new List<TileJObject>();

            for (var y = 0; y < gridArray.GetLength(1); y++)
            {

                gridRowJObject.Tiles.Add(new TileJObject
                {
                    CharacterControllerId = gridArray[x, y].CharacterControllerId,
                    Name = gridArray[x, y].Name,
                    Type = gridArray[x, y].Type,
                    GridX = gridArray[x, y].GridX,
                    GridY = gridArray[x, y].GridY,
                    IsObjective = gridArray[x, y].IsObjective
                });
            }
            gridJObject.Rows.Add(gridRowJObject);
        }

        return JsonUtility.ToJson(gridJObject);
    }

    public static Grid<Tile> Deserialize(string gridString)
    {
        try
        {
            var gridJObject = JsonUtility.FromJson<GridJObject>(gridString);
            return new Grid<Tile>(
                gridJObject.Width,
                gridJObject.Height,
                gridJObject.TileSize,
                new Vector3(gridJObject.OriginX, gridJObject.OriginY, gridJObject.OriginZ),
                (int x, int y, Grid<Tile> grid) =>
                {
                    var tile = new Tile(x, y, grid);
                    tile.CharacterControllerId = gridJObject.Rows[x].Tiles[y].CharacterControllerId;
                    tile.Type = gridJObject.Rows[x].Tiles[y].Type;
                    tile.IsObjective = gridJObject.Rows[x].Tiles[y].IsObjective;
                    return tile;
                });
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }
}
