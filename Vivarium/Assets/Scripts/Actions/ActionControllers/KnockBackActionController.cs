﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Action controller for knocking back units when they are attacked.
/// </summary>
public class KnockBackActionController : ActionController
{
    //public Vector3 ProjectileStartPosition;
    //public Transform ProjectileTransform;

    protected override void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        if (targetCharacter == null)
        {
            UnityEngine.Debug.LogWarning($"Cannot execute action on target character {targetCharacter.Character.Flavor.Name} because it is null. Most likely, the character is dead");
            return;
        }

        var targetPosition = targetCharacter.gameObject.transform.position;
        var playerPosition = transform.position;
        var targetX = TileGridController.Instance.GetGrid().GetValue(targetPosition).GridX;
        var targetY = TileGridController.Instance.GetGrid().GetValue(targetPosition).GridY;
        var playerX = TileGridController.Instance.GetGrid().GetValue(playerPosition).GridX;
        var playerY = TileGridController.Instance.GetGrid().GetValue(playerPosition).GridY;
        var adjustedX = AdjustCoordinate(playerX, targetX);
        var adjustedY = AdjustCoordinate(playerY, targetY);

        CommandController.Instance.ExecuteCommand(
            new MakeCharacterFaceTileCommand(
                _characterController,
                TileGridController.Instance.GetGrid().GetValue(targetPosition),
                true));

        var newPosition = TileGridController.Instance.GetGrid().GetWorldPosition(adjustedX, adjustedY);
        Tile toTile = TileGridController.Instance.GetGrid().GetValue(newPosition);
        Tile fromTile = TileGridController.Instance.GetGrid().GetValue(targetPosition);
        List<Tile> path = new List<Tile>();
        path.Add(toTile);

        var damage = StatCalculator.CalculateStat(_characterController.Character, ActionReference, StatType.Damage);
        if (toTile == null)
        {
            UnityEngine.Debug.Log("Attempted to knock enemy into null tile");
            targetCharacter.TakeDamage(damage);
            return;
        }

        bool drowned = false;
        if (toTile.Type == TileType.Water && targetCharacter.Character.Type != CharacterType.QueenBee)
        {
            drowned = true;
        }
        else if (!targetCharacter.Character.NavigableTiles.Contains(toTile.Type) ||
            toTile.CharacterControllerId != null)
        {
            UnityEngine.Debug.Log("Attempted to knock enemy into a tile it cannot travel on");
            targetCharacter.TakeDamage(damage);
            return;
        }


        var health = targetCharacter.GetHealthController().GetCurrentHealth();
        var shield = targetCharacter.GetHealthController().GetCurrentShield();

        //Checks if target will die before moving them
        //Otherwise another thread may try to move an object after it is destroyed, or overwrite the CharacterControllerId set in this thread.
        //Also, check if move will drown a boss. If so, do not move the character.
        if ((health + shield) > damage &&
            (toTile.Type != TileType.Water || targetCharacter.Character.Type != CharacterType.QueenBee))
        {
            CommandController.Instance.ExecuteCommand(
                new MoveCommand(
                    targetCharacter.gameObject,
                    path,
                    Constants.CHAR_MOVE_SPEED,
                    null,
                    false));
            fromTile.CharacterControllerId = null;
            toTile.CharacterControllerId = targetCharacter.Id;
        }


        PlaySound();
        targetCharacter.TakeDamage(damage);
        UnityEngine.Debug.Log($"{targetCharacter.Character.Flavor.Name} took {damage} damage from {_characterController.Character.Flavor.Name}.");
        if (drowned)
        {
            //TODO: modify character controller to have a separate drowning animation
            targetCharacter.TakeDamage(0, true);
            UnityEngine.Debug.Log("Target was knocked into the water and drowned");
        }
    }

    private int AdjustCoordinate(int playerCoordinate, int targetCoordinate)
    {
        var adjustedCoordinate = targetCoordinate;
        if (playerCoordinate > targetCoordinate)
        {
            adjustedCoordinate--;
        }
        else if (playerCoordinate < targetCoordinate)
        {
            adjustedCoordinate++;
        }
        return adjustedCoordinate;
    }

}
