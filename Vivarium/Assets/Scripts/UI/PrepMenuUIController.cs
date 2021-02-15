using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public Button PlayButton;
    public Button PreviewMapButton;
    public Button PreviewBackButton;
    public Button PauseButton;
    public Button EndTurnButton;
    public GameObject PlayerInventoryOutline;

    private List<CharacterDetailsProfile> _existingProfiles = new List<CharacterDetailsProfile>();

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

    void OnEnable()
    {
        InventorySlot.OnSlotDrag += OnInvetorySlotDrag;
    }

    void OnDisable()
    {
        InventorySlot.OnSlotDrag -= OnInvetorySlotDrag;
    }

    private void Start()
    {
        PreviewMapButton.onClick.AddListener(() =>
        {
            PrepMenu.SetActive(false);
        });
        PreviewBackButton.onClick.AddListener(() =>
        {
            PrepMenu.SetActive(true);
        });
        PlayButton.onClick.AddListener(() =>
        {
            TurnSystemManager.Instance.PlayerController.EnableCharacters();
            PreviewBackButton.gameObject.SetActive(false);
            PauseButton?.gameObject.SetActive(true);
            EndTurnButton?.gameObject.SetActive(true);
        });
    }

    public void Display()
    {
        TurnSystemManager.Instance.PlayerController.DisableCharacters();
        PreviewBackButton.gameObject.SetActive(true);
        PauseButton?.gameObject.SetActive(false);
        EndTurnButton?.gameObject.SetActive(false);
        PlayerInventoryOutline.SetActive(false);
        CleanupExistingPrepMenu();
        PrepMenu.SetActive(true);
        DisplayCharacters();
        DisplayPlayerInventory();
        ValidateInventories();
    }

    private void CleanupExistingPrepMenu()
    {
        foreach (var profile in _existingProfiles)
        {
            Destroy(profile.gameObject);
        }
        _existingProfiles = new List<CharacterDetailsProfile>();

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
            if (characterController.gameObject.activeSelf)
            {
                var profileObject = Instantiate(CharacterDetailsPrefab, CharactersContainer.transform);
                profileObject.MaxItems = MaxCharacterItems;
                profileObject.DisplayCharacter(characterController);
                profileObject.AddOnDragBeginCallback(OnItemDragStart);
                profileObject.AddOnDragEndCallback(OnItemDragEnd);

                _existingProfiles.Add(profileObject);
            }
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
                inventorySlot.AddOnDragBeginCallback(OnItemDragStart);
                inventorySlot.AddOnDragEndCallback(OnItemDragEnd);
                inventorySlot.HideEquipOverlay();
            }
            else
            {
                inventorySlot.SetItem(null);
            }
        }
    }

    private void OnItemDragStart(InventorySlot inventorySlot)
    {
        var character = inventorySlot.GetCharacter();
        if (string.IsNullOrEmpty(character?.Id))
        {
            InventoryManager.RemovePlayerItem(inventorySlot.GetItem().Item.Id);
        }
        else
        {
            InventoryManager.RemoveCharacterItem(character.Id, inventorySlot.GetItem().Item.Id);
            EquipDefaultItem(character);
        }
    }

    private void OnItemDragEnd(InventorySlot inventorySlot)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(PlayerInventoryContainer.transform as RectTransform, Input.mousePosition))
        {
            InventoryManager.PlacePlayerItem(inventorySlot.GetItem().Item);
            var character = inventorySlot.GetCharacter();
            if (character != null)
            {
                character.Unequip(inventorySlot.GetItem().Item);
                EquipDefaultItem(character);
            }
            Display();
            return;
        }

        foreach (var profile in _existingProfiles)
        {
            if (CharacterHasRoomForItem(inventorySlot.GetItem(), profile.GetCharacter()) &&
                RectTransformUtility.RectangleContainsScreenPoint(profile.transform as RectTransform, Input.mousePosition))
            {
                InventoryManager.PlaceCharacterItem(profile.GetCharacter().Id, inventorySlot.GetItem().Item);
                profile.GetCharacter().Unequip(inventorySlot.GetItem().Item);

                var character = inventorySlot.GetCharacter();
                if (inventorySlot.GetCharacter() != null)
                {
                    character.Unequip(inventorySlot.GetItem().Item);
                    EquipDefaultItem(character);
                }

                EquipDefaultItem(inventorySlot, profile.GetCharacter());
                Display();
                return;
            }
        }

        RevertItemDrag(inventorySlot);
        Display();
    }

    private void RevertItemDrag(InventorySlot inventorySlot)
    {
        var character = inventorySlot.GetCharacter();
        if (string.IsNullOrEmpty(character?.Id))
        {
            InventoryManager.PlacePlayerItem(inventorySlot.GetItem().Item);
        }
        else
        {
            InventoryManager.PlaceCharacterItem(character.Id, inventorySlot.GetItem().Item);
        }
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
                characterController.Equip(item);
            }
        }
    }

    private void EquipDefaultItem(InventorySlot inventorySlot, CharacterController characterController)
    {
        var item = inventorySlot.GetItem().Item;

        if ((characterController.Character.Weapon == null && item.Type == ItemType.Weapon) ||
            (characterController.Character.Shield == null && item.Type == ItemType.Shield))
        {
            characterController.Equip(item);
        }
    }

    private void ValidateInventories()
    {
        PlayButton.interactable = true;

        foreach (var profile in _existingProfiles)
        {
            var characterController = profile.GetCharacter();
            if (characterController.Character.Weapon == null)
            {
                profile.ShowError("Character must have at least one weapon.");
                PlayButton.interactable = false;
            }
        }
    }

    private void OnInvetorySlotDrag(InventorySlot inventorySlot)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(PlayerInventoryContainer.transform as RectTransform, Input.mousePosition))
        {
            PlayerInventoryOutline.SetActive(true);
        }
        else
        {
            PlayerInventoryOutline.SetActive(false);
        }

        foreach (var profileObject in _existingProfiles)
        {
            if (CharacterHasRoomForItem(inventorySlot.GetItem(), profileObject.GetCharacter()) &&
                RectTransformUtility.RectangleContainsScreenPoint(profileObject.transform as RectTransform, Input.mousePosition))
            {
                profileObject.ShowHighlight();
            }
            else
            {
                profileObject.HideHighlight();
            }
        }
    }

    private bool CharacterHasRoomForItem(InventoryItem inventoryItem, CharacterController characterController)
    {
        var itemCount = InventoryManager.GetCharacterItemCount(characterController.Id);
        if (itemCount < MaxCharacterItems)
        {
            return true;
        }

        if (inventoryItem.Item.CanBeStacked)
        {
            var characterInventoryItems = InventoryManager.GetCharacterItems(characterController.Id);
            foreach (var characterItem in characterInventoryItems)
            {
                if (characterItem.Item.Id == inventoryItem.Item.Id)
                {
                    return true;
                }
            }
        }

        return false;
    }
}