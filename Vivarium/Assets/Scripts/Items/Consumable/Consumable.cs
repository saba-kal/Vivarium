using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable", order = 8)]

/// <summary>
/// Initializes a consumable item object in Unity that can be used to create new individual consumable items
/// </summary>
public class Consumable : Item
{
    /// <summary>
    /// Uses the consumableType enum to allow different types of consumables to be selected
    /// </summary>
    public ConsumableType ConsumableType;

    /// <summary>
    /// Adds a field for the amount of value a consumable will give to it's corresponding stat
    /// </summary>
    public float value;

    /// <summary>
    /// Adds a field for a particle effect to be added to a consumable
    /// </summary>
    public GameObject ParticleEffect;
}
