using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 7)]

/// <summary>
/// Initializes a weapon object in Unity that can be used to create new individual weapons
/// </summary>
public class Weapon : Item
{
    //Adds a field for attributes like added damage or increased range
    public List<Attribute> Attributes;
    
    //Adds a field that determines the type of weapon and action the object will have
    public List<Action> Actions;

    // The visual model depicting the weapon
    public GameObject WeaponModel;
}
