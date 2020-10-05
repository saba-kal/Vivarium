using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiantFloatingEyeAIController : AIController
{

    public override void Execute(List<CharacterController> playerCharacters)
    {
        _playerCharacters = playerCharacters;
        if (AICanMove(out var targetMoveTile))
        {
            _aiCharacter.MoveToTile(targetMoveTile);
        }
        else if (AICanAttack(out var attack, out var targetAttackTile))
        {
            _aiCharacter.PerformAction(attack, targetAttackTile);
        }
    }

    protected override bool AICanMove(out Tile targetTile)
    {
        targetTile = null;
        if (!_aiCharacter.IsAbleToMove())
        {
            return false;
        }

        var availableMoves = _aiCharacter.CalculateAvailableMoves();
        var minDistance = float.MaxValue;

        foreach (var playerCharacter in _playerCharacters)
        {
            var closestTileToPlayer = GetClosestTileToPlayer(
                playerCharacter.transform.position,
                availableMoves,
                out var distance);

            if (_aiCharacter.IsAbleToMoveToTile(closestTileToPlayer) &&
                distance < minDistance)
            {
                targetTile = closestTileToPlayer;
                minDistance = distance;
            }
        }

        return targetTile != null;
    }
}
