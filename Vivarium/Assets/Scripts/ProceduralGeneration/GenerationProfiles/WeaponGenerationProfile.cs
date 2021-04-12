using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A profile used to generate a weapon randomly.
/// </summary>
[CreateAssetMenu(fileName = "New Weapon Profile", menuName = "Weapon Profile", order = 4)]
public class WeaponGenerationProfile : ScriptableObject
{
    /// <summary>
    /// A list of all possible <see cref="Weapon"/>s that could be used.
    /// </summary>
    public List<Weapon> PossibleWeapons;
}
