using System;
using UnityEngine;

/// <summary>
/// Represents inventory items displayed in character and player inventories.
/// </summary>
[Serializable]
public class InventoryItem
{
    /// <summary>
    /// Number of stacks of this item.
    /// </summary>
    public int Count;

    /// <summary>
    /// Position in the character or player inventory.
    /// </summary>
    [HideInInspector]
    public int InventoryPosition = -1;

    /// <summary>
    /// The item data that this inventory item references.
    /// </summary>
    public Item Item;
}
