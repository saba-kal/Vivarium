using System.Collections.Generic;

public class CharacterInventory
{
    public Dictionary<string, InventoryItem> Items { get; set; }

    public CharacterInventory()
    {
        Items = new Dictionary<string, InventoryItem>();
    }
}
