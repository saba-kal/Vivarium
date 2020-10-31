using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Shield Profile", menuName = "Shield Profile", order = 4)]
public class ShieldGenerationProfile : ScriptableObject
{
    public List<Shield> PossibleShields;
}
