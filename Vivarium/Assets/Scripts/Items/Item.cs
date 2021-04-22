using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents items displayed in character inventories, player inventories, and the rewards screen.
/// </summary>
[Serializable]

public class Item : ScriptableObject
{
    /// <summary>
    /// ID linked to this item
    /// </summary>
    public string Id;

    /// <summary>
    /// Name and description data of various game entities.
    /// </summary>
    public FlavorTextData ItemFlavorText;

    /// <summary>
    /// Determines the type of item out of Weapon, Shield, or Consumable
    /// </summary>
    public ItemType Type;

    /// <summary>
    /// The icon linked to the item
    /// </summary>
    public Sprite Icon;

    /// <summary>
    /// The model linked to the item
    /// </summary>
    public GameObject Model;

    /// <summary>
    /// A check to see if the item can be stacked or not
    /// </summary>
    public bool CanBeStacked;

    public FlavorText Flavor { get => FlavorText.FromFlavorTextData(ItemFlavorText); }
}
