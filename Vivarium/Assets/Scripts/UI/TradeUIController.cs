using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// UI controller that handles trading between two characters.
/// </summary>
public class TradeUIController : MonoBehaviour
{
    public static TradeUIController Instance { get; private set; }

    public delegate void TradeComplete(CharacterController character);
    public static event TradeComplete OnTradeComplete;

    public bool DisableActionsOnTrade = true;
    public GameObject TradeUIGameObject;
    public CharacterDetailsProfile CharacterDetailsPrefab;
    public GameObject CharacterContainer1;
    public GameObject CharacterContainer2;
    public Button CancelButton;
    public Button ConfirmButton;

    private CharacterController _character1;
    private CharacterController _character2;
    private CharacterInventory _originalInventory1;
    private CharacterInventory _originalInventory2;
    private List<InventoryItem> _originalEquippedItems1;
    private List<InventoryItem> _originalEquippedItems2;

    private List<CharacterDetailsProfile> _characterProfiles;

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
        CancelButton.onClick.AddListener(RevertTrade);
        ConfirmButton.onClick.AddListener(() =>
        {
            InventoryUIController.Instance.UpdateDisplay();
            if (DisableActionsOnTrade)
            {
                UnitInspectionController.Instance.DisableWeaponActionsForCharacter();
            }
            OnTradeComplete?.Invoke(_character1);
        });
    }

    /// <summary>
    /// Displays item trade menu between two characters.
    /// </summary>
    /// <param name="character1">The first character controller that's trading.</param>
    /// <param name="character2">The second character controller that's trading.</param>
    public void Display(CharacterController character1, CharacterController character2)
    {
        _character1 = character1;
        _character2 = character2;

        TradeUIGameObject.SetActive(true);

        _characterProfiles = new List<CharacterDetailsProfile>();
        DisplayCharacterProfile(_character1, CharacterContainer1);
        DisplayCharacterProfile(_character2, CharacterContainer2);

        _originalEquippedItems1 = new List<InventoryItem>();
        _originalEquippedItems2 = new List<InventoryItem>();
        _originalInventory1 = CopyInventory(_character1, _originalEquippedItems1);
        _originalInventory2 = CopyInventory(_character2, _originalEquippedItems2);
    }

    private void DisplayCharacterProfile(CharacterController character, GameObject container)
    {
        //Clear container.
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }

        var profileObject = Instantiate(CharacterDetailsPrefab, container.transform);
        profileObject.DisplayCharacter(character);
        profileObject.SetOnDropCallback((dropSlot, droppingSlot) =>
        {
            foreach (var profile in _characterProfiles)
            {
                profile.UpdateDisplay();
            }

            ValidateInventories();
        });

        _characterProfiles.Add(profileObject);
    }

    private CharacterInventory CopyInventory(CharacterController character, List<InventoryItem> originalEquippedItems)
    {
        var characterInventory = InventoryManager.GetCharacterInventory(character.Id);
        var inventoryCopy = new CharacterInventory();

        foreach (var itemsKeyVal in characterInventory.Items)
        {
            var inventoryItems = new List<InventoryItem>();
            foreach (var inventoryItem in itemsKeyVal.Value)
            {
                var itemCopy = InventoryItem.Copy(inventoryItem);
                inventoryItems.Add(itemCopy);
                if (character.ItemIsEquipped(itemCopy))
                {
                    originalEquippedItems.Add(itemCopy);
                }
            }

            inventoryCopy.Items.Add(itemsKeyVal.Key, inventoryItems);
        }

        return inventoryCopy;
    }

    private void RevertTrade()
    {
        InventoryManager.SetCharacterInventory(_character1.Id, _originalInventory1);
        InventoryManager.SetCharacterInventory(_character2.Id, _originalInventory2);

        EquipOriginalItems(_character1, _originalEquippedItems1);
        EquipOriginalItems(_character2, _originalEquippedItems2);

        InventoryUIController.Instance.UpdateDisplay();
    }

    private void EquipOriginalItems(CharacterController characterController, List<InventoryItem> originalEquippedItems)
    {
        characterController.UnequipAllItems();

        foreach (var item in originalEquippedItems)
        {
            characterController.Equip(item);
        }
    }

    private void ValidateInventories()
    {
        ConfirmButton.interactable = true;

        foreach (var profile in _characterProfiles)
        {
            var characterController = profile.GetCharacter();
            if (characterController.Character.Weapon == null)
            {
                profile.ShowError("Character must have at least one weapon.");
                ConfirmButton.interactable = false;
            }
        }
    }
}
