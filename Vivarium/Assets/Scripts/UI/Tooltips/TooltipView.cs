using UnityEngine;
using System.Collections;
using TMPro;

public class TooltipView : MonoBehaviour
{
    public string Id;
    public TextMeshProUGUI TooltipTitle;
    public TextMeshProUGUI TooltipDescription;
    public float BaseHeight = 100f;

    public void DisplayAction(Action action)
    {
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

        CalculateTooltipHeight();
        Id = $"Action - {action.Id}";
    }

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

    private void DisplayWeaponStats(Weapon weapon)
    {
        var weaponStats = "\n\n<size=120%>Actions:</size>";
        foreach (var action in weapon.Actions)
        {
            var minDamage = StatCalculator.CalculateStat(action, StatType.Damage, StatCalculationType.Min);
            var maxDamage = StatCalculator.CalculateStat(action, StatType.Damage, StatCalculationType.Max);

            if (Mathf.Approximately(minDamage, maxDamage))
            {
                weaponStats += $"\n - {action.Flavor.Name}: {minDamage:n0} DMG";
            }
            else
            {
                weaponStats += $"\n - {action.Flavor.Name}: {minDamage:n0}-{maxDamage:n0} DMG";
            }
        }

        TooltipDescription.text += weaponStats;
    }

    private void DisplayShieldStats(Shield shield)
    {
        TooltipDescription.text += $"\n - Shield amount: {shield.Health:n0}";
    }

    private void CalculateTooltipHeight()
    {
        var rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, BaseHeight + TooltipDescription.preferredHeight);
    }
}
