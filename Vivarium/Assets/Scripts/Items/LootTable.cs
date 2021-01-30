using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table", order = 9)]
    public class LootTable : ScriptableObject
    {
        public List<DroppableItems> DroppableItems;      
    }

[Serializable]
public class DroppableItems
{
    public Item Item;
    [Range(0, 1f)]
    public float ChanceToDrop;
}