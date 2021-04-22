using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

/// <summary>
/// Handles the Inventory UI
/// </summary>
public class InventoryUIController : MonoBehaviour
{
    public static InventoryUIController Instance { get; private set; }

    public delegate void EquipClick(CharacterController character);
    public static event EquipClick OnEquipClick;
    public delegate void ConsumeClick(CharacterController character);
    public static event ConsumeClick OnConsumeClick;

    public InventoryItemsView InventoryView;

    public Button ConsumeButton;
    public Button EquipButton;

    private CharacterController _selectedCharacterController;
    private InventorySlot _selectedItemSlot;
    private List<string> _charactersWithDisabledConsume = new List<string>();
    private List<string> _charactersWithDisabledEquip = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ConsumeButton.interactable = false;
        ConsumeButton.onClick.AddListener(OnConsumeButtonClick);

        EquipButton.interactable = false;
        EquipButton.onClick.AddListener(OnEquipButtonClick);

        InventoryView.EnableSelection();
        InventoryView.SetOnClickCallback(OnInventorySlotClick);
    }

    /// <summary>
    /// Updates the display for the character inventory
    /// </summary>
    public void UpdateDisplay()
    {
        if (_selectedCharacterController != null)
        {
            DisplayCharacterInventory(_selectedCharacterController);
        }
    }

    /// <summary>
    /// Displays the character's inventory
    /// </summary>
    /// <param name="selectedCharacterController">The character controller that holds the inventory</param>
    public void DisplayCharacterInventory(CharacterController selectedCharacterController)
    {
        if (selectedCharacterController == null)
        {
            Debug.LogError("Unable to display inventory for null character controller.");
            return;
        }

        InventoryView.Display(selectedCharacterController);

        _selectedItemSlot = null;

        _selectedCharacterController = selectedCharacterController;
        UpdateButtons();
    }


    /// <summary>
    /// Handles the event when a player clicks on an inventory slot
    /// </summary>
    /// <param name="inventorySlot">The inventory slot being clicked</param>
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

    /// <summary>
    /// Handles the player clicking the consume button
    /// </summary>
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

        OnConsumeClick?.Invoke(_selectedCharacterController);
    }

    /// <summary>
    /// Handles the player clicking on the equip button
    /// </summary>
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

        OnEquipClick?.Invoke(_selectedCharacterController);
    }

    /// <summary>
    /// Updates the accesibility of the buttons
    /// </summary>
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
                    ConsumeButton.interactable = !_charactersWithDisabledConsume.Contains(_selectedCharacterController.Id);
                    EquipButton.interactable = false;
                    break;
                case ItemType.Weapon:
                case ItemType.Shield:
                    ConsumeButton.interactable = false;
                    EquipButton.interactable =
                        !_selectedCharacterController.ItemIsEquipped(_selectedItemSlot.GetItem()) &&
                        !_charactersWithDisabledEquip.Contains(_selectedCharacterController.Id);
                    break;
            }
        }
        else
        {
            ConsumeButton.interactable = false;
            EquipButton.interactable = false;
        }
    }

    /// <summary>
    /// Enables all actions.
    /// </summary>
    public void EnableAllActions()
    {
        _charactersWithDisabledConsume = new List<string>();
        _charactersWithDisabledEquip = new List<string>();
    }

    /// <summary>
    /// Disables the consume action for a given character ID.
    /// </summary>
    /// <param name="characterId">The unique ID of the character.</param>
    public void DisableConsumeForCharacter(string characterId)
    {
        _charactersWithDisabledConsume.Add(characterId);
        ConsumeButton.interactable = false;
    }

    /// <summary>
    /// Disables the equip action for a given character ID.
    /// </summary>
    /// <param name="characterId">The unique ID of the character.</param>
    public void DisableEquipForCharacter(string characterId)
    {
        _charactersWithDisabledEquip.Add(characterId);
        EquipButton.interactable = false;
    }
}
