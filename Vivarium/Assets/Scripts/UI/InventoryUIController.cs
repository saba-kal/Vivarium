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

    public GameObject InventoryUI;
    public Button ConsumeButton;
    public Button EquipButton;
    public GameObject SelectedInventorySlotOverlay;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemStats;
    public bool DisableActionsOnConsume = true;
    public bool DisableActionsOnEquip = true;

    private CharacterController _selectedCharacterController;
    private InventorySlot[] _inventorySlots;
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

        if (SelectedInventorySlotOverlay == null)
        {
            Debug.LogError("The inventory slot selection overlay is null.");
        }
        SelectedInventorySlotOverlay?.SetActive(false);

        _inventorySlots = InventoryUI.GetComponentsInChildren<InventorySlot>();
        if (_inventorySlots == null || _inventorySlots.Length == 0)
        {
            Debug.LogError("There are no inventory slots that can display items");
        }
        for (var i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i].Index = i;
            _inventorySlots[i].AddOnClickCallback(OnInventorySlotClick);
        }
    }

    public void DisplayCharacterInventory(CharacterController selectedCharacterController)
    {
        if (selectedCharacterController == null)
        {
            Debug.LogError("Unable to display inventory for null character controller.");
            return;
        }

        if (selectedCharacterController.Character.MaxItems <= 3)
        {
            _inventorySlots[3].gameObject.SetActive(false);
            _inventorySlots[4].gameObject.SetActive(false);
        }
        else
        {
            _inventorySlots[3].gameObject.SetActive(true);
            _inventorySlots[4].gameObject.SetActive(true);
        }

        _equippedWeaponIndex = -1;
        _equippedShieldIndex = -1;
        _selectedItemSlot = null;
        ItemDescription.text = "";
        SelectedInventorySlotOverlay.SetActive(false);

        foreach (var inventorySlot in _inventorySlots)
        {
            inventorySlot.Clear();
        }

        _selectedCharacterController = selectedCharacterController;
        UpdateInventoryDisplay();
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

        SelectedInventorySlotOverlay.transform.SetParent(inventorySlot.transform);
        SelectedInventorySlotOverlay.transform.position = inventorySlot.transform.position;
        SelectedInventorySlotOverlay.SetActive(true);
        ItemDescription.text = _selectedItemSlot.GetItem().Item.Name;
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
            Debug.LogError($"Cannot consume {_selectedItemSlot.GetItem().Item.Name} because it is not a consumable type.");
            return;
        }

        _selectedCharacterController.Consume(_selectedItemSlot.GetItem().Item);
        if (InventoryManager.GetCharacterItem(_selectedCharacterController.Id, _selectedItemSlot.GetItem().Item.Id) == null)
        {
            SelectedInventorySlotOverlay.SetActive(false);
            ConsumeButton.interactable = false;
        }
        UpdateInventoryDisplay();

        if (DisableActionsOnConsume)
        {
            UIController.Instance.DisableActionsForCharacter();
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
            Debug.LogError($"Cannot equip {_selectedItemSlot.GetItem().Item.Name} because it is not a weapon or shield type.");
            return;
        }

        _selectedCharacterController.Equip(_selectedItemSlot.GetItem().Item);
        SelectedInventorySlotOverlay.SetActive(false);
        EquipButton.interactable = false;
        UpdateInventoryDisplay();
        //Refresh character UI to show new abilities from the weapon.
        UIController.Instance.ShowCharacterInfo(_selectedCharacterController);

        if (DisableActionsOnEquip)
        {
            UIController.Instance.DisableActionsForCharacter();
        }

        OnEquipClick?.Invoke();
    }

    private void UpdateInventoryDisplay()
    {
        var characterInventory = InventoryManager.GetCharacterItems(_selectedCharacterController.Id);
        for (var i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < characterInventory.Count)
            {
                _inventorySlots[i].SetItem(characterInventory[i], _selectedCharacterController);
                if (_selectedCharacterController.ItemIsEquipped(characterInventory[i].Item))
                {
                    if (characterInventory[i].Item.Type == ItemType.Weapon && _equippedWeaponIndex < 0)
                    {
                        _equippedWeaponIndex = i;
                        _inventorySlots[i].DisplayEquipOverlay();
                    }
                    else if (characterInventory[i].Item.Type == ItemType.Shield && _equippedShieldIndex < 0)
                    {
                        _equippedShieldIndex = i;
                        _inventorySlots[i].DisplayEquipOverlay();
                    }
                }
                else
                {
                    _inventorySlots[i].HideEquipOverlay();
                }
            }
            else
            {
                _inventorySlots[i].Clear();
            }
        }
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
                    EquipButton.interactable = !ItemIsEquipped(_selectedItemSlot.GetItem().Item, _selectedItemSlot.Index) && !(_isDisabled && DisableActionsOnEquip);
                    break;
            }
        }
        else
        {
            ConsumeButton.interactable = false;
            EquipButton.interactable = false;
        }
    }

    private bool ItemIsEquipped(Item item, int itemIndex)
    {
        if (_selectedCharacterController == null)
        {
            return false;
        }

        var itemIsEquiped = _selectedCharacterController.ItemIsEquipped(item);

        if (itemIsEquiped && item.Type == ItemType.Weapon && _equippedWeaponIndex != itemIndex)
        {
            return false;
        }
        else if (itemIsEquiped && item.Type == ItemType.Shield && _equippedShieldIndex != itemIndex)
        {
            return false;
        }

        return itemIsEquiped;
    }
}
