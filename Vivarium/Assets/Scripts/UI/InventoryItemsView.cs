using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using static InventorySlot;

/// <summary>
/// Handles displaying inventory items for player or character.
/// </summary>
public class InventoryItemsView : MonoBehaviour
{
    public int MaxPlayerItems = 20;
    public GameObject InventoryContainer;
    public InventorySlot InventorySlotPrefab;
    public GameObject SelectedInventorySlotOverlayPrefab;

    private int _equippedWeaponPosition = -1;
    private int _equippedShieldPosition = -1;
    private bool _selectionEnabled = false;
    private InventorySlot _selectedItemSlot;
    private GameObject _selectedItemOverlay;

    private SlotClick _onSlotClick;
    private SlotDragBegin _onSlotDragBegin;
    private SlotDragEnd _onSlotDragEnd;
    private SlotDrop _onSlotDrop;
    private CharacterController _characterController;

    /// <summary>
    /// Displays inventory for character or player. 
    /// </summary>
    /// <param name="characterController">
    /// The character controller for the character to display the inventory for. 
    /// If null, this will display player inventory.
    /// </param>
    public void Display(CharacterController characterController = null)
    {
        _characterController = characterController;

        List<InventoryItem> inventoryItems;
        if (_characterController != null)
        {
            inventoryItems = InventoryManager.GetCharacterItems(characterController.Id);
        }
        else
        {
            inventoryItems = InventoryManager.GetPlayerItems();
        }

        ClearInventory();
        DisplayItems(inventoryItems);
    }

    private void ClearInventory()
    {
        foreach (Transform child in InventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }

        _equippedWeaponPosition = -1;
        _equippedShieldPosition = -1;
    }

    private void DisplayItems(List<InventoryItem> inventoryItems)
    {
        var maxItems = _characterController?.Character.MaxItems ?? MaxPlayerItems;

        var inventorySlots = new List<InventorySlot>();

        for (var i = 0; i < maxItems; i++)
        {
            var inventorySlot = Instantiate(InventorySlotPrefab, InventoryContainer.transform);
            inventorySlot.Index = i;

            inventorySlots.Add(inventorySlot);

            if (i < inventoryItems.Count &&
                inventoryItems[i].InventoryPosition < 0)
            {
                inventoryItems[i].InventoryPosition = i;
            }

            inventorySlot.SetItem(null, _characterController);
            SetInventorySlotCallbacks(inventorySlot);
        }

        for (var i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].InventoryPosition >= maxItems || inventoryItems[i].InventoryPosition < 0)
            {
                Debug.LogError($"Invalid position of {inventoryItems[i].InventoryPosition} detected for inventory item \"{inventoryItems[i].Item.Flavor.Name}\".");
                continue;
            }

            if (ItemIsEquipped(inventoryItems[i]))
            {
                UpdateEquippedItemPositions(inventoryItems[i]);
            }

            var inventorySlot = inventorySlots[inventoryItems[i].InventoryPosition];
            inventorySlot.SetItem(inventoryItems[i], _characterController);

            UpdateInventorySlotOverlays(inventoryItems[i], inventorySlot);
        }
    }

    private void SetInventorySlotCallbacks(InventorySlot inventorySlot)
    {
        inventorySlot.SetOnClickCallback((slot) =>
        {
            _onSlotClick?.Invoke(slot);
            if (!_selectionEnabled)
            {
                return;
            }
            _selectedItemSlot = inventorySlot;
            if (_selectedItemOverlay == null)
            {
                _selectedItemOverlay = Instantiate(SelectedInventorySlotOverlayPrefab, inventorySlot.transform);
            }
            else
            {
                _selectedItemOverlay.transform.SetParent(inventorySlot.transform, false);
            }
        });

        inventorySlot.SetOnDragBeginCallback((slot) =>
        {
            _onSlotDragBegin?.Invoke(slot);
        });

        inventorySlot.SetOnDragEndCallback((slot) =>
        {
            _onSlotDragEnd?.Invoke(slot);
        });

        inventorySlot.SetOnDropCallback((dropSlot, droppingSlot) =>
        {
            TrasferItem(dropSlot, droppingSlot);
            Display(_characterController);
            _onSlotDrop?.Invoke(dropSlot, droppingSlot);
        });
    }

    private void TrasferItem(InventorySlot trasferToSlot, InventorySlot transferFromSlot)
    {
        var itemToStackOn = trasferToSlot.GetItem()?.Item;
        var inventoryItem = transferFromSlot.GetItem();

        if (itemToStackOn != null &&
            !itemToStackOn.CanBeStacked &&
            itemToStackOn.Id != inventoryItem?.Item.Id)
        {
            //Don't place item on top of another item if it can't be stacked.
            return;
        }

        if (inventoryItem?.Item == null)
        {
            //If the item is null, this event should not have been called in the first place. Something went wrong.
            Debug.LogError("Cannot transfer null item.");
            return;
        }

        if (inventoryItem.Item.CanBeStacked)
        {
            //Only move one item at a time instead of the entire stack.
            inventoryItem = new InventoryItem
            {
                Item = inventoryItem.Item,
                Count = 1,
                InventoryPosition = trasferToSlot.Index
            };
        }

        var fromCharacter = transferFromSlot.GetCharacter();
        if (string.IsNullOrEmpty(fromCharacter?.Id)) //Transferring from player inventory
        {
            InventoryManager.RemovePlayerItem(inventoryItem.Item.Id, transferFromSlot.Index);
        }
        else //Transferring from character inventory
        {
            InventoryManager.RemoveCharacterItem(fromCharacter.Id, inventoryItem.Item.Id, transferFromSlot.Index);
            fromCharacter.Unequip(inventoryItem.Item);
            EquipDefaultItem(fromCharacter);
        }

        var toCharacter = trasferToSlot.GetCharacter();
        if (string.IsNullOrEmpty(toCharacter?.Id)) //Transferring to player inventory
        {
            InventoryManager.PlacePlayerItem(inventoryItem);
        }
        else //Transferring to Character inventory
        {
            InventoryManager.PlaceCharacterItem(toCharacter.Id, inventoryItem);
            EquipDefaultItem(toCharacter);
        }

        inventoryItem.InventoryPosition = trasferToSlot.Index;
    }

    private void EquipDefaultItem(CharacterController characterController)
    {
        var inventoryItems = InventoryManager.GetCharacterItems(characterController.Id);
        foreach (var inventoryItem in inventoryItems)
        {
            var item = inventoryItem.Item;
            if ((characterController.Character.Weapon == null && item.Type == ItemType.Weapon) ||
                (characterController.Character.Shield == null && item.Type == ItemType.Shield))
            {
                characterController.Equip(inventoryItem);
                UpdateEquippedItemPositions(inventoryItem);
            }
        }
    }

    private void UpdateEquippedItemPositions(InventoryItem inventoryItem)
    {
        if (inventoryItem.Item.Type == ItemType.Weapon)
        {
            _equippedWeaponPosition = inventoryItem.InventoryPosition;
        }
        else if (inventoryItem.Item.Type == ItemType.Shield)
        {
            _equippedShieldPosition = inventoryItem.InventoryPosition;
        }
    }

    private void UpdateInventorySlotOverlays(
        InventoryItem inventoryItem,
        InventorySlot inventorySlot)
    {
        if (ItemIsEquipped(inventoryItem))
        {
            inventorySlot.DisplayEquipOverlay();
        }
        else
        {
            inventorySlot.HideEquipOverlay();
        }
    }

    private bool ItemIsEquipped(InventoryItem inventoryItem)
    {
        var itemIsEquipped = _characterController?.ItemIsEquipped(inventoryItem) ?? false;
        if (!itemIsEquipped)
        {
            return false;
        }

        if (inventoryItem.Item.Type == ItemType.Weapon)
        {
            return inventoryItem.InventoryPosition == _equippedWeaponPosition;
        }
        else if (inventoryItem.Item.Type == ItemType.Shield)
        {
            return inventoryItem.InventoryPosition == _equippedShieldPosition;
        }

        return false;
    }


    /// <summary>
    /// Sets callback for when an inventory slot is clicked.
    /// </summary>
    /// <param name="onClick">The callback to set.</param>
    public void SetOnClickCallback(SlotClick onClick)
    {
        _onSlotClick = onClick;
    }

    /// <summary>
    /// Sets callback for when inventory slot dragging begins.
    /// </summary>
    /// <param name="dragBegin">The callback to set.</param>
    public void SetOnDragBeginCallback(SlotDragBegin dragBegin)
    {
        _onSlotDragBegin = dragBegin;
    }

    /// <summary>
    /// Sets callback for when inventory slot dragging ends.
    /// </summary>
    /// <param name="dragEnd">The callback to set.</param>
    public void SetOnDragEndCallback(SlotDragEnd dragEnd)
    {
        _onSlotDragEnd = dragEnd;
    }

    /// <summary>
    /// Sets callback for when an inventory slot drops on another inventory slot.
    /// </summary>
    /// <param name="slotDrop">The callback to set.</param>
    public void SetOnDropCallback(SlotDrop slotDrop)
    {
        _onSlotDrop = slotDrop;
    }

    /// <summary>
    /// Enables ability to select inventory slots.
    /// </summary>
    public void EnableSelection()
    {
        _selectionEnabled = true;
    }

    /// <summary>
    /// Disables ability to select inventory slots.
    /// </summary>
    public void DisableSelection()
    {
        _selectionEnabled = false;
    }
}
