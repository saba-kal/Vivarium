using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class BigCharacterController : CharacterController
{
    public int CharacterTileSize = 1;
    public GameObject GridHighlightPrefab;

    protected override void VirtualStart()
    {
        base.VirtualStart();
        for (int x = 0; x < CharacterTileSize; x++)
        {
            for (int y = 0; y < CharacterTileSize; y++)
            {
                var highlightObject = Instantiate(GridHighlightPrefab);
                highlightObject.transform.SetParent(transform);
                highlightObject.transform.localPosition = new Vector3(x, 0.01f, y);
            }
        }
    }

    protected override void PlaceSelfInGrid()
    {
        var gricCellPosition = _grid.ConvertToGridCellPosition(transform.position);
        transform.position = gricCellPosition;
        var gridPosition = _grid.GetValue(transform.position);

        for (int x = 0; x < CharacterTileSize; x++)
        {
            for (int y = 0; y < CharacterTileSize; y++)
            {
                var adjascentTile = _grid.GetValue(gridPosition.GridX + x, gridPosition.GridY + y);
                if (adjascentTile != null)
                {
                    adjascentTile.CharacterControllerId = Id;
                }
            }
        }
    }

    public override void MoveToTile(Tile tile)
    {
        base.MoveToTile(tile);
    }

    public int GetSize()
    {
        return CharacterTileSize;
    }

    public override bool IsAbleToMoveToTile(Tile tile)
    {
        if (tile == null)
        {
            return false;
        }

        for (int x = 0; x < CharacterTileSize; x++)
        {
            for (int y = 0; y < CharacterTileSize; y++)
            {
                var adjascentTile = _grid.GetValue(tile.GridX + x, tile.GridY + y);
                if (adjascentTile == null || adjascentTile.CharacterControllerId != null)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
