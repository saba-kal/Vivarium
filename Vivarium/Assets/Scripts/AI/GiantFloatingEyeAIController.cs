using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GiantFloatingEyeAIController : AIController
{
    public override void Execute(List<CharacterController> playerCharacters)
    {
        _playerCharacters = playerCharacters;
        if (AICanMove(out var path))
        {
            _aiCharacter.MoveAlongPath(path);
        }
        else if (AICanAttack(out var attack, out var targetAttackTile))
        {
            _aiCharacter.PerformAction(attack, targetAttackTile);
        }
    }
}
