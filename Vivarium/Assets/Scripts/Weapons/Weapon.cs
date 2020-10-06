using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : ScriptableObject
{
    public string Name;
    public List<Attribute> Attributes;
    public List<Action> Actions;
}
