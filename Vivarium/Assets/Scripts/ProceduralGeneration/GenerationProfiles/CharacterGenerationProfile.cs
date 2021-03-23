using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Character Profile", order = 2)]
public class CharacterGenerationProfile : ScriptableObject
{
    [Header("Visuals")]
    public List<GameObject> PossibleModels;
    public List<string> PossibleNames;
    public List<Sprite> PossiblePortraits;
    public GameObject HealthBarPrefab;

    [Header("Boss Data (optional)")]
    public CharacterType Type = CharacterType.Normal;
    public int MaxSummons = 6;
    public int StartingSummons = 3;
    public int ActionsPerTurn = 2;

    [Header("Stats")]
    public float MinMaxHealth;
    public float MaxMaxHealth;
    public float MinAttackDamage;
    public float MaxAttackDamage;
    public float MinMoveRange;
    public float MaxMoveRange;
    public int MaxItems = 3;
    public int Aggro;

    public List<TileType> NavigableTiles;
    public bool CanMoveThroughCharacters = false;

    [Header("Additional Data")]
    public WeaponGenerationProfile WeaponProfile;
    public ShieldGenerationProfile ShieldProfile;
    public AttributesGenerationProfile AttributeProfile;
    public AICharacterHeuristics AICharacterHeuristics;
    public LootTable CharacterLootTable;

    [Header("Animation")]
    public RuntimeAnimatorController AnimationController;
}
