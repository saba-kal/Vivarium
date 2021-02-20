using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

public class PierceActionController : ActionController
{
    public override void Execute(Tile targetTile, System.Action onActionComplete = null)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        _delay = ActionReference.ActionTriggerDelay;
        var areaOfAffect = StatCalculator.CalculateStat(ActionReference, StatType.AttackAOE);
        var characterGridTile = _characterController.GetGridPosition();
        var affectedTiles = TileGridController.Instance.GetTilesInColumn(targetTile.GridX, targetTile.GridY, 0, areaOfAffect, characterGridTile.GridX, characterGridTile.GridY);

        CommandController.Instance.ExecuteCommand(
            new MakeCharacterFaceTileCommand(
                _characterController,
                targetTile,
                true));

        PlaySound();
        this.ExecuteAction(affectedTiles);
        onActionComplete?.Invoke();
    }

}