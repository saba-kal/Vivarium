using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public static class InventoryManager
{
    private static Dictionary<string, List<InventoryItem>> _playerInventory = new Dictionary<string, List<InventoryItem>>();
    private static Dictionary<string, CharacterInventory> _characterInventories = new Dictionary<string, CharacterInventory>();

    public static void PlaceCharacterItem(string characterId, Item item)
    {
        if (_characterInventories.ContainsKey(characterId))
        {
            if (_characterInventories[characterId].Items.ContainsKey(item.Id))
            {
                if (_characterInventories[characterId].Items[item.Id] == null ||
                    _characterInventories[characterId].Items[item.Id].Count == 0)
                {
                    _characterInventories[characterId].Items[item.Id] = new List<InventoryItem> { new InventoryItem { Count = 1, Item = item } };
                }
                else if (item.CanBeStacked)
                {
                    _characterInventories[characterId].Items[item.Id][0].Count++;
                }
                else
                {
                    _characterInventories[characterId].Items[item.Id].Add(new InventoryItem { Count = 1, Item = item });
                }
            }
            else
            {
                _characterInventories[characterId].Items[item.Id] = new List<InventoryItem> { new InventoryItem { Count = 1, Item = item } };
            }
        }
        else
        {
            var characterInventory = new CharacterInventory();
            characterInventory.Items.Add(item.Id, new List<InventoryItem> { new InventoryItem { Count = 1, Item = item } });
            _characterInventories.Add(characterId, characterInventory);
        }


        if (GetCharacterItemCount(characterId) > Constants.MAX_CHARACTER_ITEMS)
        {
            Debug.LogWarning("Character has reached maximum items. Cannot place more in inventory.");
            RemoveCharacterItem(characterId, item.Id);
            return;
        }
    }

    public static int GetCharacterItemCount(string characterId)
    {
        var characterInventory = GetCharacterInventory(characterId);
        if (characterInventory == null)
        {
            return 0;
        }

        return GetCharacterItems(characterId).Count;
    }

    public static void RemoveCharacterItem(string characterId, string itemId)
    {
        var inventoryItem = GetCharacterItem(characterId, itemId);
        if (inventoryItem == null)
        {
            return;
        }

        if (inventoryItem.Item.CanBeStacked &&
            _characterInventories[characterId].Items[itemId].Count >= 1)
        {
            _characterInventories[characterId].Items[itemId][0].Count--;
            if (_characterInventories[characterId].Items[itemId][0].Count == 0)
            {
                _characterInventories[characterId].Items.Remove(itemId);
            }
        }
        else if (_characterInventories[characterId].Items[itemId].Count > 1)
        {
            _characterInventories[characterId].Items[itemId].RemoveAt(0);
        }
        else
        {
            _characterInventories[characterId].Items.Remove(itemId);
        }
    }

    public static InventoryItem GetCharacterItem(string characterId, string itemId)
    {
        if (_characterInventories.ContainsKey(characterId) &&
            _characterInventories[characterId].Items.ContainsKey(itemId))
        {
            return _characterInventories[characterId].Items[itemId].FirstOrDefault();
        }

        return null;
    }

    public static CharacterInventory GetCharacterInventory(string characterId)
    {
        if (_characterInventories.ContainsKey(characterId))
        {
            return _characterInventories[characterId];
        }

        return null;
    }

    public static List<InventoryItem> GetCharacterItems(string characterId)
    {
        var characterItems = new List<InventoryItem>();

        var characterInventory = GetCharacterInventory(characterId);
        if (characterInventory == null)
        {
            return characterItems;
        }

        foreach (var inventoryItems in characterInventory.Items.Values)
        {
            foreach (var inventoryItem in inventoryItems)
            {
                characterItems.Add(inventoryItem);
            }
        }

        return characterItems;
    }

    public static Dictionary<string, CharacterInventory> GetAllCharacterInventories()
    {
        return _characterInventories;
    }

    public static void PlacePlayerItem(Item item)
    {
        var inventoryItem = GetPlayerItem(item.Id);
        if (inventoryItem == null)
        {
            _playerInventory[item.Id] = new List<InventoryItem> { new InventoryItem { Count = 1, Item = item } };
            return;
        }

        if (inventoryItem.Item.CanBeStacked)
        {
            inventoryItem.Count++;
        }
        else
        {
            _playerInventory[item.Id].Add(new InventoryItem { Count = 1, Item = item });
        }

        if (GetPlayerItemCount() > Constants.MAX_PLAYER_ITEMS)
        {
            RemovePlayerItem(item.Id);
            Debug.LogWarning("The player has reached maximum items. Cannot place more in inventory.");
            return;
        }
    }

    public static int GetPlayerItemCount()
    {
        var count = 0;
        foreach (var inventoryItems in _playerInventory.Values)
        {
            foreach (var inventoryItem in inventoryItems)
            {
                count++;
            }
        }

        return count;
    }

    public static void RemovePlayerItem(string itemId)
    {
        var inventoryItem = GetPlayerItem(itemId);
        if (inventoryItem == null)
        {
            return;
        }

        if (inventoryItem.Item.CanBeStacked)
        {
            inventoryItem.Count--;
            if (inventoryItem.Count <= 0)
            {
                _playerInventory.Remove(itemId);
            }
        }
        else
        {
            if (_playerInventory[itemId].Count <= 1)
            {
                _playerInventory.Remove(itemId);
            }
            else
            {
                _playerInventory[itemId].RemoveAt(0);
            }
        }
    }

    public static InventoryItem GetPlayerItem(string itemId)
    {
        if (_playerInventory.ContainsKey(itemId))
        {
            return _playerInventory[itemId].FirstOrDefault();
        }

        return null;
    }

    public static Dictionary<string, List<InventoryItem>> GetPlayerInventory()
    {
        return _playerInventory;
    }

    public static List<InventoryItem> GetPlayerItems()
    {
        var playerItems = new List<InventoryItem>();

        var playerInventory = GetPlayerInventory();
        if (playerInventory == null)
        {
            return playerItems;
        }

        foreach (var inventoryItems in playerInventory.Values)
        {
            foreach (var inventoryItem in inventoryItems)
            {
                playerItems.Add(inventoryItem);
            }
        }

        return playerItems;
    }
}
