using System.Collections.Generic;

public class CharacterInventory
{
    public Dictionary<string, List<InventoryItem>> Items { get; set; }

    public CharacterInventory()
    {
        Items = new Dictionary<string, List<InventoryItem>>();
    }
}
