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

        for (int i = 0; i < numberOfItems; i++)
        {
            var randValue = UnityEngine.Random.Range(0f, 1f);
            var orderedItems = DroppableItems.OrderBy(x => x.ChanceToDrop);
            var itemWasDropped = false;

            foreach (var droppableItem in orderedItems)
            {
                if (randValue < droppableItem.ChanceToDrop)
                {
                        droppedItems.Add(droppableItem.Item);
                        itemWasDropped = true;
                        break;
                }
            }

            if (!itemWasDropped)
            {
                droppedItems.Add(orderedItems.Last().Item);
            }
        }        
        return droppedItems;
    }
}
