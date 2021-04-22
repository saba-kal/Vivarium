using UnityEngine;
using System.Collections;

/// <summary>
/// Generates a Weapon based on a given WeaponGenerationProfile.
/// </summary>
public class WeaponGenerator
{
    /// <summary>
    /// Generates a Weapon based on a given WeaponGenerationProfile.
    /// </summary>
    /// <param name="weaponProfile"><see cref="WeaponGenerationProfile"/> containing the information used to generate the weapon.</param>
    /// <returns>A randomly generated <see cref="Weapon"/>.</returns>
    public Weapon GenerateWeapon(WeaponGenerationProfile weaponProfile)
    {
        if (weaponProfile.PossibleWeapons == null || weaponProfile.PossibleWeapons.Count == 0)
        {
            return null;
        }

        return weaponProfile.PossibleWeapons[Random.Range(0, weaponProfile.PossibleWeapons.Count)];
    }
}
