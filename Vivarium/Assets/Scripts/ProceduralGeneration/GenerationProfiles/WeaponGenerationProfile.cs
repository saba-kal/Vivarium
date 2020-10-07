using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Weapon Profile", menuName = "Weapon Profile", order = 4)]
public class WeaponGenerationProfile : ScriptableObject
{
    public List<string> PossibleNames;

    public AttributesGenerationProfile AttributeProfile;
    public ActionsGenerationProfile ActionsProfile;
}
