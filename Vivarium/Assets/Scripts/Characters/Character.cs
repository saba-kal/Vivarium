using UnityEngine;
using System.Collections.Generic;
using System;

public class Character
{
    public Guid Id;
    public string Name;
    public float MaxHealth;
    public float MoveRange;
    public List<Attribute> Attributes;
    public Weapon Weapon;
    public List<TileType> NavigableTiles;
}
