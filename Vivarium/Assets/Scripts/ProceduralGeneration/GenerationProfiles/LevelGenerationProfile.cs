using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Level Profile", menuName = "Level Profile", order = 1)]
public class LevelGenerationProfile : ScriptableObject
{
    public GridGenerationProfile GridProfile;

    public GameObject PrimaryHighlightPrefab;
    public GameObject SecondaryHighlightPrefab;
    public GameObject TertiaryHighlightPrefab;
    public GameObject QuaternaryHighlightPrefab;
    public GameObject LevelObjectivePrefab;

    public int MinEnemyCharacters;
    public int MaxEnemyCharacters;
    public List<CharacterGenerationProfile> PossibleEnemyCharacters;

    public int MinPlayerCharacters;
    public int MaxPlayerCharacters;
    public List<CharacterGenerationProfile> GuaranteedPlayerCharacters;
    public List<CharacterGenerationProfile> PossiblePlayerCharacters;

    public List<InventoryItem> StartingItems;
    public float OnLevelStartHeal = 500;
    public float OnLevelStartShieldRegen = 500;

    public LootTable PossilbleRewards;
    public List<LootTable> TreasureChests;
    public int ChestGenerationSubdivisions = 3;

    public List<CharacterGenerationProfile> RewardCharacters;

    public AISettings AISettings;
}
