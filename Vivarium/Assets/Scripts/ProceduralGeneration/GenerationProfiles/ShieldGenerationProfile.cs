using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A profile used to generate a shield randomly.
/// </summary>
[CreateAssetMenu(fileName = "New Shield Profile", menuName = "Shield Profile", order = 4)]
public class ShieldGenerationProfile : ScriptableObject
{
    /// <summary>
    /// A list of all possible <see cref="Shield"/>s that could be used.
    /// </summary>
    public List<Shield> PossibleShields;
}
