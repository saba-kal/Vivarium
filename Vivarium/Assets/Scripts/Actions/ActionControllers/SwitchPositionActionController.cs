using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class SwitchPositionActionController : ActionController
{
    //public Vector3 ProjectileStartPosition;
    //public Transform ProjectileTransform;

    protected override void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        if (targetCharacter == null)
        {
            UnityEngine.Debug.LogWarning($"Cannot execute action on target character {targetCharacter.Character.Name} because it is null. Most likely, the character is dead");
            return;
        }
        
        var targetPosition = targetCharacter.gameObject.transform.position;
        var playerPosition = transform.position;
        
        Tile targetTile = TileGridController.Instance.GetGrid().GetValue(targetPosition);
        Tile playerTile = TileGridController.Instance.GetGrid().GetValue(playerPosition);
        List <Tile> playerPath = new List<Tile>();
        List <Tile> targetPath = new List<Tile>();
        playerPath.Add(targetTile);
        targetPath.Add(playerTile);

        
        Boolean playerCanMove = CharacterController.Character.NavigableTiles.Contains(targetTile.Type);
        Boolean targetCanMove = targetCharacter.Character.NavigableTiles.Contains(playerTile.Type);
        if (!(playerCanMove && targetCanMove))
        {
            UnityEngine.Debug.Log("Attempted to switch positions onto unnavigable tile ");
            return;
        }

        var damage = StatCalculator.CalculateStat(ActionReference, StatType.Damage);
        var health = targetCharacter.GetHealthController().GetCurrentHealth();
        var shield = targetCharacter.GetHealthController().GetCurrentShield();

        MoveCharacter(CharacterController, playerPath);

        //Checks if target will die before moving them
        //Otherwise another thread may try to move an object after it is destroyed, or overwrite the CharacterControllerId set in this thread
        if((health + shield) > damage)
        {
            MoveCharacter(targetCharacter, targetPath);
            playerTile.CharacterControllerId = targetCharacter.Id;
        }
        else
        {
            playerTile.CharacterControllerId = null;
        }


        targetCharacter.TakeDamage(damage);
        targetTile.CharacterControllerId = CharacterController.Id;
        UnityEngine.Debug.Log($"{targetCharacter.Character.Name} took {damage} damage from {CharacterController.Character.Name}.");
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
                    null));
        }
    }
}
