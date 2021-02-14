using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

[Serializable]
public class DroppableItem
{
    public Item Item;
    [Range(0, 1f)]
    public float ChanceToDrop;
}

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table", order = 9)]
public class LootTable : ScriptableObject
{
    public List<DroppableItem> DroppableItems;

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
