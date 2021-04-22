using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Utility to calculate the stat of character, actions, and interactions betwene character and actions
/// </summary>
public static class StatCalculator
{
    /// <summary>
    /// Calculates the stat of an action
    /// </summary>
    /// <param name="action">Action involved in stat calculation</param>
    /// <param name="statType">The stype of stat</param>
    /// <param name="calcType">The type of calculation</param>
    /// <returns></returns>
    public static float CalculateStat(Action action, StatType statType, StatCalculationType calcType = StatCalculationType.Default)
    {
        var baseValue = GetStatFromAction(action, statType);
        return CalculateStat(baseValue, statType, action.Attributes, calcType);
    }

    /// <summary>
    /// Calculates the stat of a character
    /// </summary>
    /// <param name="character"></param>
    /// <param name="statType"></param>
    /// <param name="calcType"></param>
    /// <returns></returns>
    public static float CalculateStat(Character character, StatType statType, StatCalculationType calcType = StatCalculationType.Default)
    {
        var baseValue = GetStatFromCharacter(character, statType);
        return CalculateStat(baseValue, statType, character.Attributes, calcType);
    }

    /// <summary>
    /// Calculates the stat of a character interacting with an action
    /// </summary>
    /// <param name="character"></param>
    /// <param name="action"></param>
    /// <param name="statType"></param>
    /// <param name="calcType"></param>
    /// <returns></returns>
    public static float CalculateStat(Character character, Action action, StatType statType, StatCalculationType calcType = StatCalculationType.Default)
    {
        var baseValue = GetStatFromCharacter(character, statType) + GetStatFromAction(action, statType);
        return CalculateStat(baseValue, statType, character.Attributes.Concat(action.Attributes).ToList(), calcType);
    }

    /// <summary>
    /// Calculates a stat
    /// </summary>
    /// <param name="baseValue">The value of the state</param>
    /// <param name="statType">The stype of stat</param>
    /// <param name="attributes">The attributes of the stat</param>
    /// <param name="calcType">the calculation type</param>
    /// <returns></returns>
    public static float CalculateStat(float baseValue, StatType statType, List<Attribute> attributes, StatCalculationType calcType)
    {
        var statMultiplier = 1f;

        foreach (var attribute in attributes)
        {
            if (attribute.Type != statType)
            {
                continue;
            }

            if (calcType == StatCalculationType.Default && Random.value > attribute.ChanceToApply ||
                calcType == StatCalculationType.Min && attribute.ChanceToApply < 1)
            {
                continue;
            }

            var value = Random.Range(attribute.MinValue, attribute.MaxValue);
            if (calcType == StatCalculationType.Max)
            {
                value = attribute.MaxValue;
            }
            else if (calcType == StatCalculationType.Min)
            {
                value = attribute.MinValue;
            }

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
            case StatType.Damage:
                return character.AttackDamage;
        }

        return 0f;
    }
}

/// <summary>
/// Types of Stat Calculations
/// </summary>
public enum StatCalculationType
{
    Default = 0,
    Min = 1,
    Max = 2,
}
