using System.Collections.Generic;

public class CharacterInventory
{
    public Dictionary<string, Item> Items { get; set; }

    public CharacterInventory()
    {
        Items = new Dictionary<string, Item>();
    }
}
