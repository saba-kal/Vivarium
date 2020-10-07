using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Character
{
    public string Id;
    public string Name;
    public float MaxHealth;
    public float MoveRange;
    public List<Attribute> Attributes;
    public Weapon Weapon;
    public List<TileType> NavigableTiles;
}
