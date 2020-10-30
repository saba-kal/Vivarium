using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Weapon Profile", menuName = "Weapon Profile", order = 4)]
public class ShieldGenerationProfile : ScriptableObject
{
    public List<Shield> PossibleShields;
}
