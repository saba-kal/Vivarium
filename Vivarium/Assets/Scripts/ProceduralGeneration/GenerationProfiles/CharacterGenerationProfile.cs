using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A profile used to generate random Characters.
/// </summary>
[CreateAssetMenu(fileName = "New Character Profile", menuName = "Character Profile", order = 2)]
public class CharacterGenerationProfile : ScriptableObject
{
    /// <summary>
    /// A list of all possible game models that can be used for the character.
    /// </summary>
    [Header("Visuals")]
    public List<GameObject> PossibleModels;

    /// <summary>
    /// A list of all possible Sprites that can be used for the character's portrait.
    /// </summary>
    public List<Sprite> PossiblePortraits;

    /// <summary>
    /// A prefab used to define the aesthetics of the character's health bar.
    /// </summary>
    public GameObject HealthBarPrefab;

    /// <summary>
    /// <see cref="FlavorTextData"/> defining the possible names and description of the character.
    /// </summary>
    public FlavorTextData CharacterFlavorText;

    /// <summary>
    /// <see cref="CharacterType"/>, non-normal types used exclusively for boss level enemies.
    /// </summary>
    [Header("Boss Data (optional)")]
    public CharacterType Type = CharacterType.Normal;

    /// <summary>
    /// The max number of characters that this character can summon, used exclusively for boss level enemies.
    /// </summary>
    public int MaxSummons = 6;

    /// <summary>
    /// The number of characters that are spawned at the beginning of the boss level.
    /// </summary>
    public int StartingSummons = 3;

    /// <summary>
    /// The number of actions the character is allowed per turn, used exclusively for boss level enemies.
    /// </summary>
    public int ActionsPerTurn = 2;

    /// <summary>
    /// The minimum value of the character's health when at full health.
    /// </summary>
    [Header("Stats")]
    public float MinMaxHealth;

    /// <summary>
    /// The maximum value of the character's health when at full health.
    /// </summary>
    public float MaxMaxHealth;

    /// <summary>
    /// The minimum value of the character's attack stat.
    /// </summary>
    public float MinAttackDamage;

    /// <summary>
    /// The maximum value of the character's attack stat.
    /// </summary>
    public float MaxAttackDamage;

    /// <summary>
    /// The minimum number of tiles the character can move in a single turn.
    /// </summary>
    public float MinMoveRange;

    /// <summary>
    /// The maximum number of tiles the character can move in a single turn.
    /// </summary>
    public float MaxMoveRange;

    /// <summary>
    /// The maximum number of items the character can hold in their inventory.
    /// </summary>
    public int MaxItems = 3;

    /// <summary>
    /// A modifier on the likelihood of this character being targeted by enemy attacks.
    /// </summary>
    public int Aggro;

    /// <summary>
    /// A list of all <see cref="TileType"/> that the character is able to travel across.
    /// </summary>
    public List<TileType> NavigableTiles;

    /// <summary>
    /// Whether or not the character can move through other characters. Used to keep bosses from being trapped.
    /// </summary>
    public bool CanMoveThroughCharacters = false;

    /// <summary>
    /// The <see cref="WeaponGenerationProfile"/> of possible weapons the character can start with.
    /// </summary>
    [Header("Additional Data")]
    public WeaponGenerationProfile WeaponProfile;

    /// <summary>
    /// The <see cref="ShieldGenerationProfile"/> of a possible shield the character can start with.
    /// </summary>
    public ShieldGenerationProfile ShieldProfile;

    /// <summary>
    /// The <see cref="AttributesGenerationProfile"/> of all possible attributes this character can have.
    /// </summary>
    public AttributesGenerationProfile AttributeProfile;

    /// <summary>
    /// The set of <see cref="AICharacterHeuristics"/> defining how this character will act when controlled by the AI.
    /// </summary>
    public AICharacterHeuristics AICharacterHeuristics;

    /// <summary>
    /// A <see cref="LootTable"/> defining the items in the character's starting inventory.
    /// </summary>
    public LootTable CharacterLootTable;

    /// <summary>
    /// The controller of the character's in-game animations.
    /// </summary>
    [Header("Animation")]
    public RuntimeAnimatorController AnimationController;
}
