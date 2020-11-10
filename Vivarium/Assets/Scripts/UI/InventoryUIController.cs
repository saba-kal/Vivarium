using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class InventoryUIController : MonoBehaviour
{
    public GameObject InventoryUI;
    public Button ConsumeButton;
    public Button EquipButton;
    public GameObject SelectedInventorySlotOverlay;
    public bool DisableActionsOnConsume = true;
    public bool DisableActionsOnEquip = true;

    private CharacterController _selectedCharacterController;
    private InventorySlot[] _inventorySlots;
    private Item _selectedItem;
    private bool _isDisabled;

    void OnEnable()
    {
        InventorySlot.OnSlotClick += OnInventorySlotClick;
    }

    void OnDisable()
    {
        InventorySlot.OnSlotClick -= OnInventorySlotClick;
    }

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
    }

    public void DisplayCharacterInventory(CharacterController selectedCharacterController)
    {
        if (selectedCharacterController == null)
        {
            Debug.LogError("Unable to display inventory for null character controller.");
            return;
        }

        _selectedItem = null;
        SelectedInventorySlotOverlay.SetActive(false);

        foreach (var inventorySlot in _inventorySlots)
        {
            inventorySlot.Clear();
        }

        _selectedCharacterController = selectedCharacterController;
        UpdateInventoryDisplay();
    }

    private void OnInventorySlotClick(Item item, InventorySlot inventorySlot)
    {
        if (item == null)
        {
            Debug.LogError("Unable to display info for null item.");
            return;
        }

        _selectedItem = item;
        UpdateButtons();

        SelectedInventorySlotOverlay.transform.SetParent(inventorySlot.transform);
        SelectedInventorySlotOverlay.transform.position = inventorySlot.transform.position;
        SelectedInventorySlotOverlay.SetActive(true);
    }

    private void OnConsumeButtonClick()
    {
        if (_selectedItem == null)
        {
            Debug.LogError("Cannot consume null item.");
            return;
        }

        if (_selectedItem.Type != ItemType.Consumable)
        {
            Debug.LogError($"Cannot consume {_selectedItem.Name} because it is not a consumable type.");
            return;
        }

        _selectedCharacterController.Consume(_selectedItem);
        if (InventoryManager.GetCharacterItem(_selectedCharacterController.Id, _selectedItem.Id) == null)
        {
            SelectedInventorySlotOverlay.SetActive(false);
            ConsumeButton.interactable = false;
        }
        UpdateInventoryDisplay();

        if (DisableActionsOnConsume)
        {
            UIController.Instance.DisableActionsForCharacter();
        }
    }

    private void OnEquipButtonClick()
    {
        if (_selectedItem == null)
        {
            Debug.LogError("Cannot equip null item.");
            return;
        }

        if (_selectedItem.Type != ItemType.Weapon && _selectedItem.Type != ItemType.Shield)
        {
            Debug.LogError($"Cannot equip {_selectedItem.Name} because it is not a weapon or shield type.");
            return;
        }

        _selectedCharacterController.Equip(_selectedItem);
        SelectedInventorySlotOverlay.SetActive(false);
        EquipButton.interactable = false;
        UpdateInventoryDisplay();
        //Refresh character UI to show new abilities from the weapon.
        UIController.Instance.ShowCharacterInfo(_selectedCharacterController);

        if (DisableActionsOnEquip)
        {
            UIController.Instance.DisableActionsForCharacter();
        }
    }

    private void UpdateInventoryDisplay()
    {
        var characterInventory = InventoryManager.GetCharacterItems(_selectedCharacterController.Id);
        for (var i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < characterInventory.Count)
            {
                _inventorySlots[i].SetItem(characterInventory[i], _selectedCharacterController);
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
        if (!_isDisabled && _selectedItem != null)
        {
            switch (_selectedItem.Type)
            {
                case ItemType.Consumable:
                    ConsumeButton.interactable = true;
                    EquipButton.interactable = false;
                    break;
                case ItemType.Weapon:
                case ItemType.Shield:
                    ConsumeButton.interactable = false;
                    EquipButton.interactable = !ItemIsEquipped(_selectedItem);
                    break;
            }
        }
        else
        {
            ConsumeButton.interactable = false;
            EquipButton.interactable = false;
        }
    }

    private bool ItemIsEquipped(Item item)
    {
        return (item.Type == ItemType.Shield || item.Type == ItemType.Weapon) &&
            (_selectedCharacterController.Character.Weapon.Id == item.Id || _selectedCharacterController.Character.Shield.Id == item.Id);
    }
}
