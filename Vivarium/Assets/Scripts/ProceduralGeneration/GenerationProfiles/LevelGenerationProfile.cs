using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A profile used to generate a level randomly.
/// </summary>
[CreateAssetMenu(fileName = "New Level Profile", menuName = "Level Profile", order = 1)]
public class LevelGenerationProfile : ScriptableObject
{
    /// <summary>
    /// The <see cref="GridGenerationProfile"/> of how the grid of this level will be generated.
    /// </summary>
    public GridGenerationProfile GridProfile;

    /// <summary>
    /// <see cref="GridVisualSettings"/> contains prefabs for many of the visuals within the level, such as tile highlights.
    /// </summary>
    public GridVisualSettings VisualSettings;

    /// <summary>
    /// The <see cref="CharacterGenerationProfile"/> used to generate the boss of the level, if relevant.
    /// </summary>
    public CharacterGenerationProfile BossCharacter;

    /// <summary>
    /// The minimum number of enemy characters that will be placed within the level.
    /// </summary>
    public int MinEnemyCharacters;

    /// <summary>
    /// The maximum number of enemy characters that will be placed within the level.
    /// </summary>
    public int MaxEnemyCharacters;

    /// <summary>
    /// A list of <see cref="CharacterGenerationProfile"/> for all possible enemy characters that can be generated for the level.
    /// </summary>
    public List<CharacterGenerationProfile> PossibleEnemyCharacters;

    /// <summary>
    /// The minimum number of player characters that will be placed within the level.
    /// </summary>
    public int MinPlayerCharacters;

    /// <summary>
    /// The maximum number of player characters that will be placed within the level.
    /// </summary>
    public int MaxPlayerCharacters;

    /// <summary>
    /// A list of <see cref="CharacterGenerationProfile"/> for all character profiles that are guaranteed be used to generate player characters.
    /// </summary>
    public List<CharacterGenerationProfile> GuaranteedPlayerCharacters;

    /// <summary>
    /// A list of <see cref="CharacterGenerationProfile"/> for all possible character profiles that can be used when generating random player characters.
    /// </summary>
    public List<CharacterGenerationProfile> PossiblePlayerCharacters;

    /// <summary>
    /// A list of <see cref="InventoryItem"/> that will start in the player's inventory at the beginning of the level.
    /// </summary>
    public List<InventoryItem> StartingItems;

    /// <summary>
    /// The amount of health that player characters will heal at the start of the level.
    /// </summary>
    public float OnLevelStartHeal = 500;

    /// <summary>
    /// The amount of damage that player characters' shields will recover from at the start of the level.
    /// </summary>
    public float OnLevelStartShieldRegen = 500;

    /// <summary>
    /// <see cref="LootTable"/> of rewards that the player may be able to choose from upon beating the level.
    /// </summary>
    public LootTable PossilbleRewards;

    /// <summary>
    /// <see cref="LootTable"/> of items that player characters may receive from chests on this level.
    /// </summary>
    public List<LootTable> TreasureChests;

    /// <summary>
    /// The number of subdivisions the grid is broken into when generating the location of chests.
    /// </summary>
    public int ChestGenerationSubdivisions = 3;

    /// <summary>
    /// <see cref="CharacterGenerationProfile"/> for characters that the player can recruit upon beating the level.
    /// </summary>
    public List<CharacterGenerationProfile> RewardCharacters;

    /// <summary>
    /// <see cref="AISettings"/> regarding the enemy character AI.
    /// </summary>
    public AISettings AISettings;

    /// <summary>
    /// Shows if this is for the tutorial
    /// </summary>
    public bool IsTutorial;

    /// <summary>
    /// Name of the cut scene to play at the start of the level, if one exists.
    /// </summary>
    public string CutSceneName;
}
