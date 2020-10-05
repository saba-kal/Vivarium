using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 1)]
public class Character : ScriptableObject
{
    public int Id;
    public string Name;
    public float MaxHealth;
    public float MoveRange;
    public List<Attribute> Attributes;
    public Weapon Weapon;
    public List<TileType> NavigableTiles;
}
