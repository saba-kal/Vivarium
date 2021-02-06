using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Item : ScriptableObject
{
    public string Id;
    public string Name;
    public string Description;
    public ItemType Type;
    public Sprite Icon;
    public GameObject Model;
    public bool CanBeStacked;
}
