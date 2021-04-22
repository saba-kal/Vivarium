﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <see cref="MoveController"/> for characters that take up more the one tile on the grid.
/// </summary>
public class BigCharacterMoveController : MoveController
{

    private BigCharacterController _bigCharacterController;
    private int _characterTileSize;

    protected override void VirtualStart()
    {
        base.VirtualStart();
        _bigCharacterController = GetComponent<BigCharacterController>();
        _characterTileSize = _bigCharacterController.GetSize();
    }

    /// <inheritdoc cref="MoveController.CalculateAvailableMoves"/>
    public override Dictionary<(int, int), Tile> CalculateAvailableMoves()
    {
        //TODO: Override this method to handle different character sizes. Currently, big characters can phase through single tile passages.
        return base.CalculateAvailableMoves();
    }

    /// <inheritdoc cref="MoveController.MoveToTile(Tile, Tile, System.Action, bool)"/>
    public override void MoveToTile(Tile fromTile, Tile toTile, System.Action onMoveComplete = null, bool skipMovement = false)
    {
        if (fromTile == null || toTile == null)
        {
            return;
        }

        for (var x = 0; x < _characterTileSize; x++)
        {
            for (var y = 0; y < _characterTileSize; y++)
            {
                var adjascentFromTile = _grid.GetValue(fromTile.GridX + x, fromTile.GridY + y);
                if (adjascentFromTile != null)
                {
                    adjascentFromTile.CharacterControllerId = null;
                }

                var adjascentToTile = _grid.GetValue(toTile.GridX + x, toTile.GridY + y);
                if (adjascentFromTile != null)
                {
                    adjascentToTile.CharacterControllerId = _bigCharacterController.Id;
                }
            }
        }

        CommandController.Instance.ExecuteCommand(
            new MoveCommand(
                gameObject,
                _breadthFirstSearch.GetPathToTile(toTile),
                Constants.CHAR_MOVE_SPEED,
                onMoveComplete));
    }
}
