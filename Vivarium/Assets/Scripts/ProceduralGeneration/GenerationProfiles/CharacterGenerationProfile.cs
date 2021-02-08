using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Character Profile", order = 2)]
public class CharacterGenerationProfile : ScriptableObject
{
    public List<GameObject> PossibleModels;
    public List<string> PossibleNames;
    public List<Sprite> PossiblePortraits;

    public float MinMaxHealth;
    public float MaxMaxHealth;

    public float MinAttackDamage;
    public float MaxAttackDamage;

    public float MinMoveRange;
    public float MaxMoveRange;

    public List<TileType> NavigableTiles;

    public WeaponGenerationProfile WeaponProfile;
    public ShieldGenerationProfile ShieldProfile;
    public AttributesGenerationProfile AttributeProfile;
    public AICharacterHeuristics AICharacterHeuristics;

    public GameObject HealthBarPrefab;
    public LootTable CharacterLootTable;

    // for animation
    public RuntimeAnimatorController AnimationController;
}
