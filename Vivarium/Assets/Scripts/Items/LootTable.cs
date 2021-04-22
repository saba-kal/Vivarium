using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

/// <summary>
/// Represents items that could potentially be dropped in loot tables.
/// </summary>
[Serializable]
public class DroppableItem
{

    /// <summary>
    /// The item data that this droppable item references.
    /// </summary>
    public Item Item;

    [Range(0, 1f)]

    /// <summary>
    /// The percentage to drop that this item has.
    /// </summary>
    public float ChanceToDrop;
}

/// <summary>
/// Represents the loot table object that can be populated with items and used for different things like rewards or chests.
/// </summary>
[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table", order = 9)]
public class LootTable : ScriptableObject
{
    /// <summary>
    /// The list of potential items that can be dropped.
    /// </summary>
    public List<DroppableItem> DroppableItems;

     /// <summary>
     /// Compares the ordered list of items against a random value to determine which item should be dropped
     /// </summary>
     /// <param name="numberOfItems">A count of the number of items in the list</param>
     /// <returns>An item picked to be given to the player</returns>
    public List<Item> Pick(int numberOfItems)
    {
        var droppedItems = new List<Item>();
        var droppableItems = CopyDroppableItems();

        for (int i = 0; i < numberOfItems; i++)
        {
            var totalChance = DroppableItems.Sum(x => x.ChanceToDrop);
            var randValue = UnityEngine.Random.Range(0f, totalChance);

            var orderedItems = droppableItems.OrderBy(x => x.ChanceToDrop).ToList();
            var itemWasDropped = false;
            float currentChance = 0;

            for (int j = 0; j < orderedItems.Count; j++)
            {
                var droppableItem = orderedItems[j];
                currentChance += droppableItem.ChanceToDrop;

                if (randValue <= currentChance)
                {
                    droppedItems.Add(droppableItem.Item);
                    droppableItems.Remove(droppableItem); //Remove item so we don't pick the item again.
                    itemWasDropped = true;
                    break;
                }
            }

            if (!itemWasDropped)
            {
                var defaultItem = orderedItems.Last();
                droppedItems.Add(defaultItem.Item);
                droppableItems.Remove(defaultItem); //Remove item so we don't pick the item again.
            }
        }

        return droppedItems;
    }

    private List<DroppableItem> CopyDroppableItems()
    {
        var droppableItems = new List<DroppableItem>();
        foreach (var droppableItem in DroppableItems)
        {
            var copy = new DroppableItem
            {
                Item = droppableItem.Item,
                ChanceToDrop = droppableItem.ChanceToDrop
            };
            droppableItems.Add(copy);
        }

        return droppableItems;
    }
}
