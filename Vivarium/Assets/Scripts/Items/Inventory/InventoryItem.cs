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

    /// <summary>
    /// Copies another inventory item object.
    /// </summary>
    /// <param name="inventoryItem">The inventory item to copy.</param>
    /// <returns>A new <see cref="InventoryItem"/> object.</returns>
    public static InventoryItem Copy(InventoryItem inventoryItem)
    {
        if (inventoryItem == null)
        {
            return null;
        }

        return new InventoryItem
        {
            Count = inventoryItem.Count,
            InventoryPosition = inventoryItem.InventoryPosition,
            Item = inventoryItem.Item
        };
    }
}
