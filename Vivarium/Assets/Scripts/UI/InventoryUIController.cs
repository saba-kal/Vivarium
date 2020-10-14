using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUIController : MonoBehaviour
{
    //The properties below are for demoing the inventory API. They can be removed once actual UI is implemented.
    #region Inventory API demo properties
    public Item TestItem;
    public Button PlaceCharacterItemButton;
    public Button RemoveCharacterItemButton;
    public Button GetCharacterItemButton;
    public Button GetCharacterInventoryButton;
    public Button GetAllCharacterInventoriesButton;
    public Button PlacePlayerItemButton;
    public Button RemovePlayerItemButton;
    public Button GetPlayerItemButton;
    public Button GetPlayerInventoryButton;
    public Button GetNearbyCharactersButton;
    public Button EquipButton;
    #endregion

    private CharacterController _selectedCharacterController;

    private void Start()
    {
        SetupDemoButtonClickEvents();
    }

    public void DisplayCharacterInventory(CharacterController characterController)
    {
        _selectedCharacterController = characterController;
        //TODO: Implement inventory display logic here.
    }

    //Note: the region below is just logic for demoing the inventory API. Please remove the code below once actual UI is implemented.
    #region Inventory API demo logic

    private void SetupDemoButtonClickEvents()
    {
        PlaceCharacterItemButton.onClick.AddListener(() =>
        {
            InventoryManager.PlaceCharacterItem(_selectedCharacterController.Id, TestItem);
            LogCharacterInventory();
        });
        RemoveCharacterItemButton.onClick.AddListener(() =>
        {
            InventoryManager.RemoveCharacterItem(_selectedCharacterController.Id, TestItem.Id);
            LogCharacterInventory();
        });
        GetCharacterItemButton.onClick.AddListener(() =>
        {
            var item = InventoryManager.GetCharacterItem(_selectedCharacterController.Id, TestItem.Id);
            Debug.Log($"Retrieved the following item: {item.Name}");
        });
        GetAllCharacterInventoriesButton.onClick.AddListener(() =>
        {
            foreach (var inventory in InventoryManager.GetAllCharacterInventories().Values)
            {
                LogCharacterInventory(inventory);
            }
        });
        PlacePlayerItemButton.onClick.AddListener(() =>
        {
            InventoryManager.PlacePlayerItem(TestItem);
            LogPlayerInventory();
        });
        RemovePlayerItemButton.onClick.AddListener(() =>
        {
            InventoryManager.RemovePlayerItem(TestItem.Id);
            LogPlayerInventory();
        });
        GetPlayerItemButton.onClick.AddListener(() =>
        {
            var item = InventoryManager.GetPlayerItem(TestItem.Id);
            Debug.Log($"Retrieved the following item: {item.Name}");
        });
        GetPlayerInventoryButton.onClick.AddListener(() =>
        {
            LogPlayerInventory();
        });
        GetNearbyCharactersButton.onClick.AddListener(() =>
        {
            LogCharacters(_selectedCharacterController.GetAdjacentCharacters(CharacterSearchType.Player));
        });
        EquipButton.onClick.AddListener(() =>
        {
            if (_selectedCharacterController != null)
            {
                _selectedCharacterController.Equip(TestItem);

                //Refresh character UI to show new abilities from the weapon.
                UIController.Instance.ShowCharacterInfo(_selectedCharacterController);
            }
        });
    }

    private void LogCharacterInventory()
    {
        LogCharacterInventory(_selectedCharacterController);
    }

    private void LogCharacterInventory(CharacterController characterController)
    {
        if (characterController == null)
        {
            return;
        }

        var logMessage = $"Inventory for character \"{characterController.Character.Name}\": ";
        foreach (var item in InventoryManager.GetCharacterInventory(characterController.Id).Items.Values)
        {
            logMessage += $"{item.Name}, ";
        }

        Debug.Log(logMessage);
    }

    private void LogCharacterInventory(CharacterInventory characterInventory)
    {
        if (characterInventory == null)
        {
            return;
        }

        var logMessage = $"Character Inventory: ";
        foreach (var item in characterInventory.Items.Values)
        {
            logMessage += $"{item.Name}, ";
        }

        Debug.Log(logMessage);
    }

    private void LogPlayerInventory()
    {
        var logMessage = "Player inventory: ";
        foreach (var item in InventoryManager.GetPlayerInventory().Values)
        {
            logMessage += $"{item.Name}, ";
        }

        Debug.Log(logMessage);
    }

    private void LogCharacters(List<CharacterController> characterControllers)
    {
        var logMessage = "Nearby characters: ";
        foreach (var characterController in characterControllers)
        {
            logMessage += $"{characterController.Character.Name}, ";
        }

        Debug.Log(logMessage);
    }

    #endregion
}
