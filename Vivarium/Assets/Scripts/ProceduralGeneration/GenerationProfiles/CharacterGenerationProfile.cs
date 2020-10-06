using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Character Profile", order = 2)]
public class CharacterGenerationProfile : ScriptableObject
{
    public List<GameObject> PossibleModels;
    public List<string> PossibleNames;

    public float MinMaxHealth;
    public float MaxMaxHealth;

    public float MinMoveRange;
    public float MaxMoveRange;

    public List<TileType> NavigableTiles;

    public WeaponGenerationProfile WeaponProfile;
    public AttributesGenerationProfile AttributeProfile;
}
