using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;

/// <summary>
/// Handles transferring items between inventories and inventory slot positions.
/// </summary>
public class ItemTransferHandler
{
    private InventorySlot _transferToSlot;
    private InventorySlot _transferFromSlot;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="transferToSlot">Inventory slot to transfer the item to.</param>
    /// <param name="transferFromSlot">Inventory slot from which the item is coming from.</param>
    public ItemTransferHandler(InventorySlot transferToSlot, InventorySlot transferFromSlot)
    {
        _transferToSlot = transferToSlot;
        _transferFromSlot = transferFromSlot;
    }

    /// <summary>
    /// Executes the transfer according to the to/from slots that were passed in the constructor.
    /// </summary>
    public void ExecuteTransfer()
    {
        if (_transferFromSlot.GetItem() == null)
        {
            //Unable to transfer anything from an empty inventory slot.
            return;
        }

        if (_transferToSlot.GetItem() == null && !_transferFromSlot.GetItem().Item.CanBeStacked)
        {
            MoveUnstackableItemToEmptySlot();
        }
        else if (_transferToSlot.GetItem() == null && _transferFromSlot.GetItem().Item.CanBeStacked)
        {
            MoveStackableItemToEmptySlot();
        }
        else if (_transferToSlot.GetItem() != null &&
            !_transferFromSlot.GetItem().Item.CanBeStacked &&
            !_transferToSlot.GetItem().Item.CanBeStacked)
        {
            SwapItems();
        }
        else if (_transferToSlot.GetItem() != null && ItemsCanStack())
        {
            StackItemOnTopOfAnotherItem();
        }
        else if (_transferToSlot.GetItem() != null)
        {
            SwapItems();
        }
    }

    private void MoveUnstackableItemToEmptySlot()
    {
        var character = _transferFromSlot.GetCharacter();

        if (_transferFromSlot.GetCharacter() == _transferToSlot.GetCharacter())
        {
            var itemCopy = InventoryItem.Copy(_transferFromSlot.GetItem());

            //Move is within character's/player's inventory.
            _transferFromSlot.GetItem().InventoryPosition = _transferToSlot.Index;

            //Update equipped index.
            if (character != null && character.ItemIsEquipped(itemCopy))
            {
                character.Equip(_transferFromSlot.GetItem());
            }

            return;
        }

        //Need to transfer item to another inventory.

        var itemToTransfer = _transferFromSlot.GetItem();
        if (character != null && character.ItemIsEquipped(itemToTransfer))
        {
            //Equipped weapon moved to another inventory. Need to get a new default equip.
            character.Unequip(itemToTransfer.Item);
            EquipDefaultItem(character, itemToTransfer.Item.Type, itemToTransfer.InventoryPosition);
        }

        RemoveItemFromInventory(_transferFromSlot.GetCharacter(), itemToTransfer, 1);

        itemToTransfer.Count = 1;
        itemToTransfer.InventoryPosition = _transferToSlot.Index;
        PlaceItemInInventory(_transferToSlot.GetCharacter(), itemToTransfer);
        EquipDefaultItem(_transferToSlot.GetCharacter(), itemToTransfer.Item.Type, -1);
    }

    private void MoveStackableItemToEmptySlot()
    {
        var itemToTransfer = _transferFromSlot.GetItem();
        if (itemToTransfer.Count == 1)
        {
            //Since there is only 1 stack, we can treat this as an unstackable item.
            MoveUnstackableItemToEmptySlot();
        }

        var itemCopy = InventoryItem.Copy(itemToTransfer);
        RemoveItemFromInventory(_transferFromSlot.GetCharacter(), itemToTransfer, 1);

        itemCopy.Count = 1;
        itemCopy.InventoryPosition = _transferToSlot.Index;
        PlaceItemInInventory(_transferToSlot.GetCharacter(), itemCopy);
    }

    private void SwapItems()
    {
        var fromCharacter = _transferFromSlot.GetCharacter();
        var toCharacter = _transferToSlot.GetCharacter();

        if (_transferFromSlot.GetCharacter() == _transferToSlot.GetCharacter())
        {
            var fromItemCopy = InventoryItem.Copy(_transferFromSlot.GetItem());
            var toItemCopy = InventoryItem.Copy(_transferToSlot.GetItem());

            //Move is within character's/player's inventory.
            _transferFromSlot.GetItem().InventoryPosition = _transferToSlot.Index;
            _transferToSlot.GetItem().InventoryPosition = _transferFromSlot.Index;

            //Update equipped index.
            if (fromCharacter != null && fromCharacter.ItemIsEquipped(fromItemCopy))
            {
                fromCharacter.Equip(_transferFromSlot.GetItem());
            }
            if (toCharacter != null && toCharacter.ItemIsEquipped(toItemCopy))
            {
                toCharacter.Equip(_transferToSlot.GetItem());
            }

            return;
        }

        var inventoryItem1 = InventoryItem.Copy(_transferFromSlot.GetItem());
        var inventoryItem2 = InventoryItem.Copy(_transferToSlot.GetItem());

        RemoveItemFromInventory(fromCharacter, inventoryItem1, inventoryItem1.Count);
        if (fromCharacter != null && fromCharacter.ItemIsEquipped(inventoryItem1))
        {
            fromCharacter.Unequip(inventoryItem1.Item);
        }

        RemoveItemFromInventory(toCharacter, inventoryItem2, inventoryItem2.Count);
        if (toCharacter != null && toCharacter.ItemIsEquipped(inventoryItem2))
        {
            toCharacter.Unequip(inventoryItem2.Item);
        }

        inventoryItem1.InventoryPosition = _transferToSlot.Index;
        PlaceItemInInventory(_transferToSlot.GetCharacter(), inventoryItem1);

        inventoryItem2.InventoryPosition = _transferFromSlot.Index;
        PlaceItemInInventory(_transferFromSlot.GetCharacter(), inventoryItem2);

        EquipDefaultItem(fromCharacter, ItemType.Weapon, -1);
        EquipDefaultItem(fromCharacter, ItemType.Shield, -1);
        EquipDefaultItem(toCharacter, ItemType.Weapon, -1);
        EquipDefaultItem(toCharacter, ItemType.Shield, -1);
    }

    private void StackItemOnTopOfAnotherItem()
    {
        var inventoryItem1Copy = InventoryItem.Copy(_transferFromSlot.GetItem());
        RemoveItemFromInventory(_transferFromSlot.GetCharacter(), inventoryItem1Copy, 1);

        inventoryItem1Copy.Count = 1;
        inventoryItem1Copy.InventoryPosition = _transferToSlot.Index;
        PlaceItemInInventory(_transferToSlot.GetCharacter(), inventoryItem1Copy);
    }

    private void RemoveItemFromInventory(CharacterController character, InventoryItem inventoryItem, int count = 1)
    {
        if (inventoryItem == null)
        {
            return;
        }

        //Transferring from player inventory
        if (string.IsNullOrEmpty(character?.Id))
        {
            InventoryManager.RemovePlayerItem(inventoryItem.Item.Id, inventoryItem.InventoryPosition, count);
        }
        //Transferring from character inventory
        else
        {
            InventoryManager.RemoveCharacterItem(character.Id, inventoryItem.Item.Id, inventoryItem.InventoryPosition, count);
        }
    }

    private void PlaceItemInInventory(CharacterController character, InventoryItem inventoryItem)
    {
        if (inventoryItem == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(character?.Id))
        {
            //Transferring to player inventory
            InventoryManager.PlacePlayerItem(inventoryItem, true);
        }
        else
        {
            //Transferring to Character inventory
            InventoryManager.PlaceCharacterItem(character.Id, inventoryItem, true);
        }
    }

    private bool ItemsCanStack()
    {
        var inventoryItem1 = _transferFromSlot.GetItem();
        var inventoryItem2 = _transferToSlot.GetItem();

        if (inventoryItem1 == null || inventoryItem2 == null)
        {
            return true;
        }

        return inventoryItem1.Item.CanBeStacked && inventoryItem2.Item.CanBeStacked &&
            inventoryItem1.Item.Id == inventoryItem2.Item.Id;
    }

    private void EquipDefaultItem(
        CharacterController characterController,
        ItemType itemType,
        int excludedPosition)
    {
        if (characterController == null)
        {
            return;
        }

        if (itemType != ItemType.Shield && itemType != ItemType.Weapon)
        {
            //Cannot equip non-weapon and shield items.
            return;
        }

        var inventoryItems = InventoryManager.GetCharacterItems(characterController.Id);

        var minimumPosition = int.MaxValue;
        InventoryItem ItemToEquip = null;

        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.InventoryPosition == excludedPosition)
            {
                continue;
            }

            if (inventoryItem.InventoryPosition < minimumPosition &&
                inventoryItem.Item.Type == itemType)
            {
                minimumPosition = inventoryItem.InventoryPosition;
                ItemToEquip = inventoryItem;
            }
        }

        if (ItemToEquip != null)
        {
            characterController.Equip(ItemToEquip);
        }
    }
}
