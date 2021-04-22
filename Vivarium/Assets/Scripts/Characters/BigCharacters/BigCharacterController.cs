using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

/// <summary>
/// <see cref="CharacterController"/> for characters that take up more the one tile on the grid.
/// </summary>
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

    /// <inheritdoc cref="CharacterController.MoveToTile(Tile, System.Action, bool)"/>
    public override void MoveToTile(Tile tile, System.Action onMoveComplete = null, bool skipMovement = false)
    {
        base.MoveToTile(tile, onMoveComplete);
    }

    /// <summary>
    /// Gets the size of the character.
    /// </summary>
    /// <returns>The tile width/height of the character.</returns>
    public int GetSize()
    {
        return CharacterTileSize;
    }

    /// <inheritdoc cref="CharacterController.IsAbleToMoveToTile(Tile)"/>
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
