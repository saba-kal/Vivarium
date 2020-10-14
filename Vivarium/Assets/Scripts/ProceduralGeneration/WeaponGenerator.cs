using UnityEngine;
using System.Collections;

public class WeaponGenerator
{
    public Weapon GenerateWeapon(WeaponGenerationProfile weaponProfile)
    {
        if (weaponProfile.PossibleWeapons == null || weaponProfile.PossibleWeapons.Count == 0)
        {
            return null;
        }

        return weaponProfile.PossibleWeapons[Random.Range(0, weaponProfile.PossibleWeapons.Count)];
    }
}
