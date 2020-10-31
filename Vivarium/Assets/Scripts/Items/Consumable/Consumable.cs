using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable", order = 8)]
public class Consumable : Item
{
    public ConsumableType ConsumableType;
    public int charges;
    public float value;
}
