using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 7)]
public class Weapon : Item
{
    public List<Attribute> Attributes;
    public List<Action> Actions;
}
