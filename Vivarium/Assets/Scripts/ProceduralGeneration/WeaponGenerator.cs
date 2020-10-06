using UnityEngine;
using System.Collections;

public class WeaponGenerator
{
    public Weapon GenerateWeapon(WeaponGenerationProfile weaponProfile)
    {
        var weapon = new Weapon();
        weapon.Name = weaponProfile.PossibleNames[Random.Range(0, weaponProfile.PossibleNames.Count)];
        weapon.Attributes = new AttributesGenerator().GenerateAttributes(weaponProfile.AttributeProfile);

        return weapon;
    }
}
