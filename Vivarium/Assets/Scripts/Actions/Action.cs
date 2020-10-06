using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Action", menuName = "Action", order = 3)]
public class Action : ScriptableObject
{
    public Guid Id;
    public string Name;
    public float BaseDamage;
    [Range(0, 1)]
    public float CriticalChance;
    public float CriticalMultiplier;
    public float Range;
    public float AreaOfAffect;
    public ActionTarget ActionTargetType;
    public ActionControllerType ControllerType;
    public List<Attribute> Attributes;
}
