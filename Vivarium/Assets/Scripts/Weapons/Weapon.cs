using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 2)]
public class Weapon : ScriptableObject
{
    public string Name;
    public List<Attribute> Attributes;
    public List<Action> Actions;
}
