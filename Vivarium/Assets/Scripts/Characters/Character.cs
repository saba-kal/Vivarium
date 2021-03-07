using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Character
{
    public string Id;
    public string Name;
    public CharacterType Type = CharacterType.Normal;
    public Sprite Portrait;
    public float MaxHealth;
    public float AttackDamage;
    public float MoveRange;
    public AICharacterHeuristics AICharacterHeuristics;
    public List<Attribute> Attributes;
    public Weapon Weapon;
    public Shield Shield;
    public List<TileType> NavigableTiles;
    public LootTable CharacterLootTable;
    public int MaxItems = 3;
    public bool CanMoveThroughCharacters = false;
}
