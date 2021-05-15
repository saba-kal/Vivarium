using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// Logic for what the tool tip displays. 
/// </summary>
public class TooltipView : MonoBehaviour
{
    public string Id;
    public TextMeshProUGUI TooltipTitle;
    public TextMeshProUGUI TooltipDescription;
    public float BaseHeight = 100f;

    /// <summary>
    /// Displays the stats for an action
    /// </summary>
    /// <param name="action">The action to be displayed by the tool tip</param>
    public void DisplayAction(Action action)
    {
        var maxRange = StatCalculator.CalculateStat(action, StatType.AttackMaxRange);

        TooltipTitle.text = action.Flavor.Name;
        TooltipDescription.text = action.Flavor.Description;

        var minDamage = StatCalculator.CalculateStat(action, StatType.Damage, StatCalculationType.Min);
        var maxDamage = StatCalculator.CalculateStat(action, StatType.Damage, StatCalculationType.Max);

        if (Mathf.Approximately(minDamage, maxDamage))
        {
            TooltipDescription.text += $"\n - DMG: {minDamage:n0}";
        }
        else
        {
            TooltipDescription.text += $"\n - DMG: {minDamage:n0}-{maxDamage:n0}";
        }
        TooltipDescription.text += $"\n - RANGE: {maxRange:n0}";

        CalculateTooltipHeight();
        Id = $"Action - {action.Id}";
    }

    /// <summary>
    /// Displays the stats for an item
    /// </summary>
    /// <param name="item">The item to be displayed by the tool tip</param>
    public void DisplayItem(Item item)
    {
        TooltipTitle.text = item.Flavor.Name;
        TooltipDescription.text = item.Flavor.Description;

        switch (item.Type)
        {
            case ItemType.Weapon:
                DisplayWeaponStats((Weapon)item);
                break;
            case ItemType.Shield:
                DisplayShieldStats((Shield)item);
                break;
        }

        CalculateTooltipHeight();
        Id = $"Item - {item.Id}";
    }

    /// <summary>
    /// Displays the stats of the character
    /// </summary>
    /// <param name="character">The character to be displayed by the tool tip</param>
    public void DisplayCharacter(Character character)
    {
        TooltipTitle.text = character.Flavor.Name;
        TooltipDescription.text = "Weapon: " + character.Weapon.Flavor.Name + "\n";
        TooltipDescription.text += "Health: " + (int)character.MaxHealth + "\n";
        TooltipDescription.text += "Base attack: " + (int)character.AttackDamage + "\n";
        TooltipDescription.text += "Movement: " + (int)character.MoveRange + "\n";

        CalculateTooltipHeight();
        Id = $"Character - {character.Id}";
    }

    /// <summary>
    /// Displays the stats of a weapon
    /// </summary>
    /// <param name="weapon">The weapon to be displayed by the tool tip</param>
    private void DisplayWeaponStats(Weapon weapon)
    {
        var weaponStats = "\n\n<size=120%>Actions:</size>";
        foreach (var action in weapon.Actions)
        {
            var maxRange = StatCalculator.CalculateStat(action, StatType.AttackMaxRange);
            var minDamage = StatCalculator.CalculateStat(action, StatType.Damage, StatCalculationType.Min);
            var maxDamage = StatCalculator.CalculateStat(action, StatType.Damage, StatCalculationType.Max);

            if (Mathf.Approximately(minDamage, maxDamage))
            {
                weaponStats += $"\n - {action.Flavor.Name}: {minDamage:n0} DMG, {maxRange:n0} RANGE";
            }
            else
            {
                weaponStats += $"\n - {action.Flavor.Name}: {minDamage:n0}-{maxDamage:n0} DMG, {maxRange:n0} RANGE";
            }
        }

        TooltipDescription.text += weaponStats;
    }

    /// <summary>
    /// Displays the stats of a shield
    /// </summary>
    /// <param name="shield">The shield to be displayed by the tool tip</param>
    private void DisplayShieldStats(Shield shield)
    {
        TooltipDescription.text += $"\n - Shield amount: {shield.Health:n0}";
    }

    public void DisplayGenericText(string providedText)
    {
        TooltipTitle.text = "";
        TooltipDescription.text = providedText;
        CalculateTooltipHeight();
    }
    /// <summary>
    /// Calculates the height of the tool tip based on the title and description
    /// </summary>
    private void CalculateTooltipHeight()
    {
        var rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, BaseHeight + TooltipDescription.preferredHeight);
    }
}
