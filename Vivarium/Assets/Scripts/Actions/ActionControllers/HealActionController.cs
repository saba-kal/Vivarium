using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealActionController : ArcProjectileActionController
{
    protected override void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        if (targetCharacter == null)
        {
            Debug.LogWarning($"Cannot execute action on target character {targetCharacter.Character.Name} because it is null. Most likely, the character is dead");
            return;
        }

        var healAmount = StatCalculator.CalculateStat(_characterController.Character, ActionReference, StatType.Damage);
        targetCharacter.Heal(healAmount);
        Debug.Log($"{_characterController.Character.Name} healed {targetCharacter.Character.Name} by {healAmount} points.");
    }
}
