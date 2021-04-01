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
        PlaceCharacterItem(
            characterId,
            new InventoryItem
            {
                Count = 1,
                Item = item
            });
    }

    public static void PlaceCharacterItem(string characterId, InventoryItem inventoryItem, bool ignorePosition = false)
    {
        var characterController = TurnSystemManager.Instance.GetCharacterController(characterId);
        if (characterController == null)
        {
            Debug.LogError("Unable to find character controller to place item.");
            return;
        }

        PlaceCharacterItem(characterController, inventoryItem, ignorePosition);
    }

    public static void PlaceCharacterItem(CharacterController characterController, InventoryItem inventoryItem, bool ignorePosition = false)
    {
        var characterId = characterController.Id;
        var maxItems = characterController?.Character.MaxItems ?? Constants.MAX_CHARACTER_ITEMS;
        var characterItems = GetCharacterItems(characterId);

        if (inventoryItem.InventoryPosition < 0)
        {
            inventoryItem.InventoryPosition = GetFreeInventoryPosition(characterItems, maxItems);
        }

        if (_characterInventories.ContainsKey(characterId))
        {
            if (!ignorePosition && !InventoryPositionIsAvailable(inventoryItem, characterItems, maxItems))
            {
                Debug.LogWarning("This inventory position is unavailable.");
                return;
            }

            if (_characterInventories[characterId].Items.TryGetValue(inventoryItem.Item.Id, out var existingInventoryItems))
            {
                if (existingInventoryItems == null || existingInventoryItems.Count == 0)
                {
                    _characterInventories[characterId].Items[inventoryItem.Item.Id] = new List<InventoryItem> { inventoryItem };
                }
                else if (inventoryItem.Item.CanBeStacked)
                {
                    var existingInventoryItem = existingInventoryItems.FirstOrDefault(itm => inventoryItem.InventoryPosition == itm.InventoryPosition);
                    if (existingInventoryItem != null)
                    {
                        existingInventoryItem.Count++;
                    }
                    else
                    {
                        existingInventoryItems.Add(inventoryItem);
                    }
                }
                else
                {
                    existingInventoryItems.Add(inventoryItem);
                }
            }
            else
            {
                _characterInventories[characterId].Items[inventoryItem.Item.Id] = new List<InventoryItem> { inventoryItem };
            }
        }
        else
        {
            var characterInventory = new CharacterInventory();
            characterInventory.Items.Add(inventoryItem.Item.Id, new List<InventoryItem> { inventoryItem });
            _characterInventories.Add(characterId, characterInventory);
        }

        if (GetCharacterItemCount(characterId) > maxItems)
        {
            Debug.LogWarning("Character has reached maximum items. Cannot place more in inventory.");
            RemoveCharacterItem(characterId, inventoryItem.Item.Id, inventoryItem.InventoryPosition);
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

    public static void RemoveCharacterItem(string characterId, string itemId, int inventoryPosition, int count = 1)
    {
        var inventoryItem = GetCharacterItem(characterId, itemId, inventoryPosition);
        if (inventoryItem == null)
        {
            return;
        }

        if (inventoryItem.Item.CanBeStacked &&
            _characterInventories[characterId].Items[itemId].Count > 0)
        {
            inventoryItem.Count -= count;
            if (inventoryItem.Count <= 0)
            {
                _characterInventories[characterId].Items[itemId].Remove(inventoryItem);
            }
        }
        else if (_characterInventories[characterId].Items[itemId].Count > 0)
        {
            var removed = _characterInventories[characterId].Items[itemId].Remove(inventoryItem);
        }

        if (_characterInventories[characterId].Items[itemId].Count == 0)
        {
            _characterInventories[characterId].Items.Remove(itemId);
        }
    }

    public static InventoryItem GetCharacterItem(string characterId, string itemId, int inventoryPosition)
    {
        if (_characterInventories.ContainsKey(characterId) &&
            _characterInventories[characterId].Items.ContainsKey(itemId))
        {
            return _characterInventories[characterId].Items[itemId].FirstOrDefault(itm => itm.InventoryPosition == inventoryPosition);
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

    public static void SetCharacterInventory(string characterId, CharacterInventory characterInventory)
    {
        _characterInventories[characterId] = characterInventory;
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
        PlacePlayerItem(new InventoryItem
        {
            Count = 1,
            Item = item
        });
    }

    public static void PlacePlayerItem(InventoryItem inventoryItem, bool ignorePosition = false)
    {
        if (inventoryItem.InventoryPosition < 0)
        {
            inventoryItem.InventoryPosition = GetFreeInventoryPosition(GetPlayerItems(), Constants.MAX_PLAYER_ITEMS);
        }

        if (!ignorePosition && !InventoryPositionIsAvailable(inventoryItem, GetPlayerItems(), Constants.MAX_PLAYER_ITEMS))
        {
            Debug.LogWarning("This inventory position is unavailable.");
            return;
        }

        var existingInventoryItem = GetPlayerItem(inventoryItem.Item.Id, inventoryItem.InventoryPosition);
        if (existingInventoryItem == null)
        {
            if (_playerInventory.ContainsKey(inventoryItem.Item.Id))
            {
                _playerInventory[inventoryItem.Item.Id].Add(inventoryItem);
            }
            else
            {
                _playerInventory[inventoryItem.Item.Id] = new List<InventoryItem> { inventoryItem };
            }

            return;
        }

        if (inventoryItem.Item.CanBeStacked && existingInventoryItem.InventoryPosition == inventoryItem.InventoryPosition)
        {
            existingInventoryItem.Count++;
        }
        else
        {
            _playerInventory[inventoryItem.Item.Id].Add(inventoryItem);
        }

        if (GetPlayerItemCount() > Constants.MAX_PLAYER_ITEMS)
        {
            RemovePlayerItem(inventoryItem.Item.Id, inventoryItem.InventoryPosition);
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

    public static void RemovePlayerItem(string itemId, int inventoryPosition, int count = 1)
    {
        var inventoryItem = GetPlayerItem(itemId, inventoryPosition);
        if (inventoryItem == null)
        {
            return;
        }

        if (inventoryItem.Item.CanBeStacked)
        {
            inventoryItem.Count -= count;
            if (inventoryItem.Count <= 0)
            {
                _playerInventory[itemId].Remove(inventoryItem);
            }
        }
        else
        {
            _playerInventory[itemId].Remove(inventoryItem);
        }

        if (_playerInventory[itemId].Count < 1)
        {
            _playerInventory.Remove(itemId);
        }
    }

    public static InventoryItem GetPlayerItem(string itemId, int inventoryPosition)
    {
        if (_playerInventory.ContainsKey(itemId))
        {
            return _playerInventory[itemId].FirstOrDefault(itm => itm.InventoryPosition == inventoryPosition);
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

    public static void ClearInventory()
    {
        _playerInventory = new Dictionary<string, List<InventoryItem>>();
        _characterInventories = new Dictionary<string, CharacterInventory>();
    }

    private static bool InventoryPositionIsAvailable(InventoryItem item, List<InventoryItem> inventoryItems, int maxItems)
    {
        foreach (var existingItem in inventoryItems)
        {
            if (existingItem.InventoryPosition == item.InventoryPosition &&
                (!existingItem.Item.CanBeStacked || existingItem.Item.Id != item.Item.Id))
            {
                var inventoryPosition = GetFreeInventoryPosition(inventoryItems, maxItems);
                if (inventoryPosition > 0)
                {
                    item.InventoryPosition = inventoryPosition;
                    return true;
                }

                return false;
            }
        }

        return inventoryItems.Count <= maxItems && item.InventoryPosition >= 0 && item.InventoryPosition < maxItems;
    }

    private static int GetFreeInventoryPosition(List<InventoryItem> inventoryItems, int maxItems)
    {
        if (inventoryItems.Count >= maxItems)
        {
            return -1;
        }

        var occupiedPositions = new bool[maxItems];
        foreach (var item in inventoryItems)
        {
            if (item.InventoryPosition >= 0 && item.InventoryPosition < maxItems)
            {
                occupiedPositions[item.InventoryPosition] = true;
            }
        }

        for (var i = 0; i < maxItems; i++)
        {
            if (!occupiedPositions[i])
            {
                return i;
            }
        }

        return -1;
    }
}
