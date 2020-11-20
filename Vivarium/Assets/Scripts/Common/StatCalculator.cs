using UnityEngine;
using System.Collections.Generic;

public static class StatCalculator
{
    public static float CalculateStat(Action action, StatType statType)
    {
        var baseValue = GetStatFromAction(action, statType);
        return CalculateStat(baseValue, statType, action.Attributes);
    }

    public static float CalculateStat(Character character, StatType statType)
    {
        var baseValue = GetStatFromCharacter(character, statType);
        return CalculateStat(baseValue, statType, character.Attributes);
    }

    public static float CalculateStat(float baseValue, StatType statType, List<Attribute> attributes)
    {
        var statMultiplier = 1f;

        foreach (var attribute in attributes)
        {
            if (attribute.Type != statType ||
                Random.value > attribute.ChanceToApply)
            {
                continue;
            }

            var value = Random.Range(attribute.MinValue, attribute.MaxValue);

            if (attribute.Formula == AttributeFormula.Additive)
            {
                baseValue += value;
            }

            if (attribute.Formula == AttributeFormula.Multiplicative)
            {
                statMultiplier += value;
            }
        }

        return baseValue * statMultiplier;
    }

    private static float GetStatFromAction(Action action, StatType statType)
    {
        switch (statType)
        {
            case StatType.Damage:
                return action.BaseDamage;
            case StatType.AttackMinRange:
                return action.MinRange;
            case StatType.AttackMaxRange:
                return action.MaxRange;
            case StatType.AttackAOE:
                return action.AreaOfAffect;
        }

        return 0f;
    }

    private static float GetStatFromCharacter(Character character, StatType statType)
    {
        switch (statType)
        {
            case StatType.Health:
                return character.MaxHealth;
            case StatType.MoveRadius:
                return character.MoveRange;
        }

        return 0f;
    }
}
