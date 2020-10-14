using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public static class InventoryManager
{
    private static Dictionary<string, Item> _playerInventory = new Dictionary<string, Item>();
    private static Dictionary<string, CharacterInventory> _characterInventories = new Dictionary<string, CharacterInventory>();

    public static void PlaceCharacterItem(string characterId, Item item)
    {
        if (_characterInventories.ContainsKey(characterId))
        {
            if (_characterInventories[characterId].Items.ContainsKey(item.Id))
            {
                _characterInventories[characterId].Items[item.Id] = item;
            }
            else
            {
                _characterInventories[characterId].Items.Add(item.Id, item);
            }
        }
        else
        {
            var characterInventory = new CharacterInventory();
            characterInventory.Items.Add(item.Id, item);
            _characterInventories.Add(characterId, characterInventory);
        }
    }

    public static void RemoveCharacterItem(string characterId, string itemId)
    {
        if (_characterInventories.ContainsKey(characterId) &&
            _characterInventories[characterId].Items.ContainsKey(itemId))
        {
            _characterInventories[characterId].Items.Remove(itemId);
        }
    }

    public static Item GetCharacterItem(string characterId, string itemId)
    {
        if (_characterInventories.ContainsKey(characterId) &&
            _characterInventories[characterId].Items.ContainsKey(itemId))
        {
            return _characterInventories[characterId].Items[itemId];
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
            _playerInventory[item.Id] = item;
        }
        else
        {
            _playerInventory.Add(item.Id, item);
        }
    }

    public static void RemovePlayerItem(string itemId)
    {
        if (_playerInventory.ContainsKey(itemId))
        {
            _playerInventory.Remove(itemId);
        }
    }

    public static Item GetPlayerItem(string itemId)
    {
        if (_playerInventory.ContainsKey(itemId))
        {
            return _playerInventory[itemId];
        }

        return null;
    }

    public static Dictionary<string, Item> GetPlayerInventory()
    {
        return _playerInventory;
    }
}
