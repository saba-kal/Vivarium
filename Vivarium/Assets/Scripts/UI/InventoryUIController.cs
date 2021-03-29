using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    public delegate void EquipClick();
    public static event EquipClick OnEquipClick;
    public delegate void ConsumeClick();
    public static event ConsumeClick OnConsumeClick;

    public InventoryItemsView InventoryView;

    public Button ConsumeButton;
    public Button EquipButton;
    public bool DisableActionsOnConsume = true;
    public bool DisableActionsOnEquip = true;

    private CharacterController _selectedCharacterController;
    private InventorySlot _selectedItemSlot;
    private bool _isDisabled;
    private int _equippedWeaponIndex = -1;
    private int _equippedShieldIndex = -1;

    private void Start()
    {
        ConsumeButton.interactable = false;
        ConsumeButton.onClick.AddListener(OnConsumeButtonClick);

        EquipButton.interactable = false;
        EquipButton.onClick.AddListener(OnEquipButtonClick);

        InventoryView.EnableSelection();
        InventoryView.SetOnClickCallback(OnInventorySlotClick);
    }

    public void DisplayCharacterInventory(CharacterController selectedCharacterController)
    {
        if (selectedCharacterController == null)
        {
            Debug.LogError("Unable to display inventory for null character controller.");
            return;
        }

        InventoryView.Display(selectedCharacterController);

        _equippedWeaponIndex = -1;
        _equippedShieldIndex = -1;
        _selectedItemSlot = null;

        _selectedCharacterController = selectedCharacterController;
        UpdateButtons();
    }

    private void OnInventorySlotClick(InventorySlot inventorySlot)
    {
        _selectedItemSlot = inventorySlot;
        if (_selectedItemSlot?.GetItem()?.Item == null)
        {
            Debug.LogError("Unable to display info for null item.");
            return;
        }

        UpdateButtons();
    }

    private void OnConsumeButtonClick()
    {
        if (_selectedItemSlot == null)
        {
            Debug.LogError("Cannot consume null item.");
            return;
        }

        if (_selectedItemSlot.GetItem().Item.Type != ItemType.Consumable)
        {
            Debug.LogError($"Cannot consume {_selectedItemSlot.GetItem().Item.Flavor.Name} because it is not a consumable type.");
            return;
        }

        _selectedCharacterController.Consume(_selectedItemSlot.GetItem());
        if (InventoryManager.GetCharacterItem(
            _selectedCharacterController.Id,
            _selectedItemSlot.GetItem().Item.Id,
            _selectedItemSlot.GetItem().InventoryPosition) == null)
        {
            ConsumeButton.interactable = false;
        }
        InventoryView.Display(_selectedCharacterController);

        if (DisableActionsOnConsume)
        {
            UnitInspectionController.Instance.DisableWeaponActionsForCharacter();
        }

        OnConsumeClick?.Invoke();
    }

    private void OnEquipButtonClick()
    {
        if (_selectedItemSlot.GetItem().Item == null)
        {
            Debug.LogError("Cannot equip null item.");
            return;
        }

        if (_selectedItemSlot.GetItem().Item.Type != ItemType.Weapon && _selectedItemSlot.GetItem().Item.Type != ItemType.Shield)
        {
            Debug.LogError($"Cannot equip {_selectedItemSlot.GetItem().Item.Flavor.Name} because it is not a weapon or shield type.");
            return;
        }

        _selectedCharacterController.Equip(_selectedItemSlot.GetItem());
        EquipButton.interactable = false;

        InventoryView.Display(_selectedCharacterController);

        //Refresh character UI to show new abilities from the weapon.
        UIController.Instance.ShowCharacterInfo(_selectedCharacterController);

        if (DisableActionsOnEquip)
        {
            UnitInspectionController.Instance.DisableWeaponActionsForCharacter();
        }

        OnEquipClick?.Invoke();
    }

    public void SetActionButtonsDisabled(bool isDisabled)
    {
        _isDisabled = isDisabled;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (_selectedCharacterController != null && _selectedCharacterController.IsEnemy)
        {
            ConsumeButton.gameObject.SetActive(false);
            EquipButton.gameObject.SetActive(false);
            return;
        }
        else
        {
            ConsumeButton.gameObject.SetActive(true);
            EquipButton.gameObject.SetActive(true);
        }

        if (_selectedItemSlot?.GetItem()?.Item != null)
        {
            switch (_selectedItemSlot.GetItem().Item.Type)
            {
                case ItemType.Consumable:
                    ConsumeButton.interactable = !(_isDisabled && DisableActionsOnConsume);
                    EquipButton.interactable = false;
                    break;
                case ItemType.Weapon:
                case ItemType.Shield:
                    ConsumeButton.interactable = false;
                    EquipButton.interactable = !_selectedCharacterController.ItemIsEquipped(_selectedItemSlot.GetItem()) && !(_isDisabled && DisableActionsOnEquip);
                    break;
            }
        }
        else
        {
            ConsumeButton.interactable = false;
            EquipButton.interactable = false;
        }
    }
}
