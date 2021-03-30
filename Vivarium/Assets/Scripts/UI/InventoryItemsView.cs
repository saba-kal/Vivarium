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
    public GameObject InventoryContainer;
    public InventorySlot InventorySlotPrefab;
    public GameObject SelectedInventorySlotOverlayPrefab;

    private bool _selectionEnabled = false;

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
    }

    private void DisplayItems(List<InventoryItem> inventoryItems)
    {
        var maxItems = _characterController?.Character.MaxItems ?? Constants.MAX_PLAYER_ITEMS;

        var inventorySlots = new List<InventorySlot>();

        for (var i = 0; i < maxItems; i++)
        {
            var inventorySlot = Instantiate(InventorySlotPrefab, InventoryContainer.transform);
            inventorySlot.Index = i;

            var isEnemy = _characterController?.IsEnemy ?? false;
            inventorySlot.SetHighlightEnabled(!isEnemy);

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

    private void TrasferItem(InventorySlot transferToSlot, InventorySlot transferFromSlot)
    {
        var itemTransferHandler = new ItemTransferHandler(transferToSlot, transferFromSlot);
        itemTransferHandler.ExecuteTransfer();
    }

    private void UpdateInventorySlotOverlays(
        InventoryItem inventoryItem,
        InventorySlot inventorySlot)
    {
        if (_characterController?.ItemIsEquipped(inventoryItem) ?? false)
        {
            inventorySlot.DisplayEquipOverlay();
        }
        else
        {
            inventorySlot.HideEquipOverlay();
        }
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
