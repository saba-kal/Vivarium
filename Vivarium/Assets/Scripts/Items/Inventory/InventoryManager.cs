using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public static class InventoryManager
{
    private static Dictionary<string, InventoryItem> _playerInventory = new Dictionary<string, InventoryItem>();
    private static Dictionary<string, CharacterInventory> _characterInventories = new Dictionary<string, CharacterInventory>();

    public static void PlaceCharacterItem(string characterId, Item item)
    {
        if (_characterInventories.ContainsKey(characterId))
        {
            if (_characterInventories[characterId].Items.ContainsKey(item.Id))
            {
                _characterInventories[characterId].Items[item.Id].Count++;
            }
            else
            {
                _characterInventories[characterId].Items.Add(item.Id, new InventoryItem { Count = 1, Item = item });
            }
        }
        else
        {
            var characterInventory = new CharacterInventory();
            characterInventory.Items.Add(item.Id, new InventoryItem { Count = 1, Item = item });
            _characterInventories.Add(characterId, characterInventory);
        }
    }

    public static void RemoveCharacterItem(string characterId, string itemId)
    {
        if (_characterInventories.ContainsKey(characterId) &&
            _characterInventories[characterId].Items.ContainsKey(itemId))
        {
            if (_characterInventories[characterId].Items[itemId].Count <= 1)
            {
                _characterInventories[characterId].Items.Remove(itemId);
            }
            else
            {
                _characterInventories[characterId].Items[itemId].Count--;
            }
        }
    }

    public static Item GetCharacterItem(string characterId, string itemId)
    {
        if (_characterInventories.ContainsKey(characterId) &&
            _characterInventories[characterId].Items.ContainsKey(itemId))
        {
            return _characterInventories[characterId].Items[itemId].Item;
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

    public static Dictionary<string, CharacterInventory> GetAllCharacterInventories()
    {
        return _characterInventories;
    }

    public static void PlacePlayerItem(Item item)
    {
        if (_playerInventory.ContainsKey(item.Id))
        {
            _playerInventory[item.Id].Count++;
        }
        else
        {
            _playerInventory.Add(item.Id, new InventoryItem { Count = 1, Item = item });
        }
    }

    public static void RemovePlayerItem(string itemId)
    {
        if (_playerInventory.ContainsKey(itemId))
        {
            if (_playerInventory[itemId].Count <= 1)
            {
                _playerInventory.Remove(itemId);
            }
            else
            {
                _playerInventory[itemId].Count--;
            }
        }
    }

    public static Item GetPlayerItem(string itemId)
    {
        if (_playerInventory.ContainsKey(itemId))
        {
            return _playerInventory[itemId].Item;
        }

        return null;
    }

    public static Dictionary<string, InventoryItem> GetPlayerInventory()
    {
        return _playerInventory;
    }
}
