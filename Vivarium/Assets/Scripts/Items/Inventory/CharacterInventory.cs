using System.Collections.Generic;

/// <summary>
/// Represents inventory data for character inventories
/// </summary>
public class CharacterInventory
{
    /// <summary>
    /// Property used to store items in a characters inventory
    /// </summary>
    public Dictionary<string, List<InventoryItem>> Items { get; set; }

    /// <summary>
    /// A dictionary representing the items in a characters inventory
    /// </summary>
    public CharacterInventory()
    {
        Items = new Dictionary<string, List<InventoryItem>>();
    }
}
