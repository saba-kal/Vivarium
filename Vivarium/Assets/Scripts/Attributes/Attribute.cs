using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Attribute", menuName = "Attribute", order = 4)]
public class Attribute : ScriptableObject
{
    public string Name;
    public float MinValue;
    public float MaxValue;
    [Range(0f, 1f)]
    public float ChanceToApply = 1f;
    public AttributeFormula Formula;
    public StatType Type;
}