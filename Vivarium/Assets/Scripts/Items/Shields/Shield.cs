using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Initializes a shield object in Unity that can be used to create new individual shields
/// </summary>
[CreateAssetMenu(fileName = "New Shield", menuName = "Shield", order = 7)]
public class Shield : Item
{
    /// <summary>
    /// Adds a field for the amount of health a shield provides.
    /// </summary>
    public float Health;
}
