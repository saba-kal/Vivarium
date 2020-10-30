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
        var gricCellPosition = TileGridController.Instance.GetGrid().ConvertToGridCellPosition(transform.position);
        transform.position = gricCellPosition;
        var gridPosition = TileGridController.Instance.GetGrid().GetValue(transform.position);

        for (int x = 0; x < CharacterTileSize; x++)
        {
            for (int y = 0; y < CharacterTileSize; y++)
            {
                var adjascentTile = TileGridController.Instance.GetGrid().GetValue(gridPosition.GridX + x, gridPosition.GridY + y);
                if (adjascentTile != null)
                {
                    adjascentTile.CharacterControllerId = Id;
                }
            }
        }
    }

    public override void MoveToTile(Tile tile, System.Action onMoveComplete = null)
    {
        base.MoveToTile(tile, onMoveComplete);
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
                var adjascentTile = TileGridController.Instance.GetGrid().GetValue(tile.GridX + x, tile.GridY + y);
                if (adjascentTile == null || !string.IsNullOrEmpty(adjascentTile.CharacterControllerId))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
