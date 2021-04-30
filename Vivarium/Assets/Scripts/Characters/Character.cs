using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Holds data that represents a playable or enemy character.
/// </summary>
[Serializable]
public class Character
{
    /// <summary>
    /// The unique ID associated with this character.
    /// </summary>
    public string Id;

    /// <summary>
    /// The <see cref="FlavorText"/> describing this character.
    /// </summary>
    public FlavorText Flavor;

    /// <summary>
    /// The type of character that this is.
    /// </summary>
    public CharacterType Type = CharacterType.Normal;

    /// <summary>
    /// Portrait of the character used to display its profile.
    /// </summary>
    public Sprite Portrait;

    /// <summary>
    /// The character's max health.
    /// </summary>
    public float MaxHealth;

    /// <summary>
    /// The character's damage when performing attack actions.
    /// </summary>
    public float AttackDamage;

    /// <summary>
    /// How far the character can move on the grid.
    /// </summary>
    public float MoveRange;

    /// <summary>
    /// If an enemy AI, the heuristics used by <see cref="GridPointCalculator"/> to evaluate tile point values.
    /// </summary>
    public AICharacterHeuristics AICharacterHeuristics;

    /// <summary>
    /// List of <see cref="Attribute"/> that the character has.
    /// </summary>
    public List<Attribute> Attributes;

    /// <summary>
    /// The equipped <see cref="Weapon"/>.
    /// </summary>
    public Weapon Weapon;

    /// <summary>
    /// The equipped <see cref="Shield"/>.
    /// </summary>
    public Shield Shield;

    /// <summary>
    /// List of <see cref="TileType"/> that the character can navigate.
    /// </summary>
    public List<TileType> NavigableTiles;

    /// <summary>
    /// Items that the character drops when killed.
    /// </summary>
    public LootTable CharacterLootTable;

    /// <summary>
    /// Max number of items that the character can hold
    /// </summary>
    public int MaxItems = 3;

    /// <summary>
    /// Whether or not this character can move through other characters.
    /// </summary>
    public bool CanMoveThroughCharacters = false;

    /// <summary>
    /// How much this character can attract AI's attention.
    /// </summary>
    public int Aggro;

    /// <summary>
    /// The type of unit the character is
    /// </summary>
    public string unitType;
}
