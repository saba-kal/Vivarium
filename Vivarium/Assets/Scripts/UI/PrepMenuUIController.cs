using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine.SceneManagement;

public class PrepMenuUIController : MonoBehaviour
{
    public static PrepMenuUIController Instance { get; private set; }

    public int MaxPlayerItems = 20;
    public int MaxCharacterItems = 3;
    public GameObject PrepMenu;
    public CharacterDetailsProfile CharacterDetailsPrefab;
    public GameObject CharactersContainer;
    public GameObject PlayerInventoryContainer;
    public InventorySlot InventorySlotPrefab;

    private List<GameObject> _existingProfiles = new List<GameObject>();

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
        Display();
    }

    public void Display()
    {
        CleanupExistingPrepMenu();
        PrepMenu.SetActive(true);
        DisplayCharacters();
        DisplayPlayerInventory();
    }

    private void CleanupExistingPrepMenu()
    {
        foreach (var gameObject in _existingProfiles)
        {
            Destroy(gameObject);
        }

        foreach (Transform child in PlayerInventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void DisplayCharacters()
    {
        var characterControllers = TurnSystemManager.Instance.PlayerController.PlayerCharacters;
        foreach (var characterController in characterControllers)
        {
            var profileObject = Instantiate(CharacterDetailsPrefab, CharactersContainer.transform);
            profileObject.MaxItems = MaxCharacterItems;
            profileObject.DisplayCharacter(characterController);

            _existingProfiles.Add(profileObject.gameObject);
        }
    }

    private void DisplayPlayerInventory()
    {
        var playerItems = InventoryManager.GetPlayerItems();
        for (var i = 0; i < MaxPlayerItems; i++)
        {
            var inventorySlot = Instantiate(InventorySlotPrefab, PlayerInventoryContainer.transform);
            if (i < playerItems.Count)
            {
                inventorySlot.SetItem(playerItems[i]);
            }
            else
            {
                inventorySlot.SetItem(null);
            }
        }
    }
}