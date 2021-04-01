using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Item : ScriptableObject
{
    public string Id;
    public FlavorTextData ItemFlavorText;
    public ItemType Type;
    public Sprite Icon;
    public GameObject Model;
    public bool CanBeStacked;

    public FlavorText Flavor { get => FlavorText.FromFlavorTextData(ItemFlavorText); }
}
