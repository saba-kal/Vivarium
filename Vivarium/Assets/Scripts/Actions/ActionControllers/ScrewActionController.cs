using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;

/// <summary>
/// Action controller for moving a character 90 degrees clockwise when attacked.
/// </summary>
public class ScrewActionController : ActionController
{
    private Grid<Tile> _grid;
    protected override void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        _grid = TileGridController.Instance.GetGrid();
        if (targetCharacter == null)
        {
            UnityEngine.Debug.LogWarning($"Cannot execute action on target character {targetCharacter.Character.Flavor.Name} because it is null. Most likely, the character is dead");
            return;
        }

        var targetPosition = targetCharacter.gameObject.transform.position;
        Tile targetTile = TileGridController.Instance.GetGrid().GetValue(targetPosition);
        Tile newTile = NewLocation(targetCharacter);

        bool targetCanMove = false;
        bool drowned = false;
        if (newTile.Type == TileType.Water && targetCharacter.Character.Type != CharacterType.QueenBee)
        {
            drowned = true;
            targetCanMove = true;
        }
        else if (newTile != null && newTile.CharacterControllerId == null)
        {
            targetCanMove = targetCharacter.Character.NavigableTiles.Contains(newTile.Type);
        }

        if (newTile.Type == TileType.Water && targetCharacter.Character.Type == CharacterType.QueenBee)
        {
            targetCanMove = false;
        }

        if (!targetCanMove)
        {
            UnityEngine.Debug.Log("Attempted to move target onto unnavigable tile");
        }

        var damage = StatCalculator.CalculateStat(_characterController.Character, ActionReference, StatType.Damage);
        var health = targetCharacter.GetHealthController().GetCurrentHealth();
        var shield = targetCharacter.GetHealthController().GetCurrentShield();

        bool surviveAttack;
        if ((health + shield) > damage)
        {
            surviveAttack = true;
        }
        else
        {
            surviveAttack = false;
        }
        targetCharacter.TakeDamage(damage);
        UnityEngine.Debug.Log($"{targetCharacter.Character.Flavor.Name} took {damage} damage from {_characterController.Character.Flavor.Name}.");

        if (targetCanMove && surviveAttack)
        {
            targetCharacter.gameObject.transform.position = _grid.GetWorldPositionCentered(newTile.GridX, newTile.GridY);
            newTile.CharacterControllerId = targetCharacter.Id;
            targetTile.CharacterControllerId = null;
        }
        if (drowned)
        {
            //TODO: modify character controller to have a separate drowning animation
            targetCharacter.TakeDamage(0, true);
            UnityEngine.Debug.Log("Target was knocked into the water and drowned");
        }
    }

    private void MoveCharacter(CharacterController characterController, List<Tile> path)
    {
        if (characterController.gameObject != null)
        {
            CommandController.Instance.ExecuteCommand(
                new MoveCommand(
                    characterController.gameObject,
                    path,
                    Constants.CHAR_MOVE_SPEED,
                    null,
                    false));
        }
    }

    private Tile NewLocation(CharacterController targetCharacter)
    {
        var targetPosition = targetCharacter.transform.position;
        Tile targetTile = _grid.GetValue(targetPosition);
        var startingX = targetTile.GridX;
        var startingY = targetTile.GridY;

        var playerPosition = transform.position;
        Tile playerTile = _grid.GetValue(playerPosition);
        var playerX = playerTile.GridX;
        var playerY = playerTile.GridY;

        var relativeX = startingX - playerX;
        var relativeY = startingY - playerY;

        int newRelativeX;
        int newRelativeY;

        newRelativeY = relativeX * -1;
        newRelativeX = relativeY;

        int finalX = playerX + newRelativeX;
        int finalY = playerY + newRelativeY;

        return _grid.GetValue(finalX, finalY);
    }
}
