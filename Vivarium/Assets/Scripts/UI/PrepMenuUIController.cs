using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Handles UI functionality for the preparation menu screen.
/// </summary>
public class PrepMenuUIController : MonoBehaviour
{
    public static PrepMenuUIController Instance { get; private set; }

    public GameObject PrepMenu;
    public CharacterDetailsProfile CharacterDetailsPrefab;
    public InventoryItemsView PlayerInventoryView;
    public GameObject CharactersContainer;
    public Button PlayButton;
    public Button PreviewMapButton;
    public Button PreviewBackButton;
    public Button UndoMoveButton;
    public Button EndTurnButton;
    public GameObject PlayerButtons;

    private List<CharacterDetailsProfile> _existingProfiles = new List<CharacterDetailsProfile>();
    private MasterCameraScript _masterCamera;

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
        PlayerInventoryView.SetOnDropCallback(OnItemDrop);

        PreviewMapButton.onClick.AddListener(() =>
        {
            PrepMenu.SetActive(false);
            PreviewBackButton.gameObject.SetActive(true);
            _masterCamera?.unlockCamera();
        });
        PreviewBackButton.onClick.AddListener(() =>
        {
            PrepMenu.SetActive(true);
            PreviewBackButton.gameObject.SetActive(false);
            _masterCamera?.lockCamera();
        });
        PlayButton.onClick.AddListener(() =>
        {
            TurnSystemManager.Instance.PlayerController.EnableCharacters();
            PreviewBackButton.gameObject.SetActive(false);
            UndoMoveButton?.gameObject.SetActive(true);
            EndTurnButton?.gameObject.SetActive(true);
            _masterCamera?.unlockCamera();
            PlayerButtons.SetActive(true);
            UndoMoveController.Instance.IsUndoTrue = false;
        });


        var mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
        _masterCamera = mainCamera.GetComponent<MasterCameraScript>();
    }

    /// <summary>
    /// Displays the prep menu screen.
    /// </summary>
    public void Display()
    {
        TurnSystemManager.Instance.PlayerController.DisableCharacters();
        PreviewBackButton.gameObject.SetActive(false);
        UndoMoveButton?.gameObject.SetActive(false);
        EndTurnButton?.gameObject.SetActive(false);
        CleanupExistingPrepMenu();
        PrepMenu.SetActive(true);
        DisplayCharacters();
        PlayerInventoryView.Display();
        ValidateInventories();
        _masterCamera?.lockCamera();
    }

    private void CleanupExistingPrepMenu()
    {
        foreach (var profile in _existingProfiles)
        {
            Destroy(profile.gameObject);
        }
        _existingProfiles = new List<CharacterDetailsProfile>();
    }

    private void DisplayCharacters()
    {
        var characterControllers = TurnSystemManager.Instance.PlayerController.PlayerCharacters;
        foreach (var characterController in characterControllers)
        {
            if (characterController.gameObject.activeSelf)
            {
                var profileObject = Instantiate(CharacterDetailsPrefab, CharactersContainer.transform);
                profileObject.DisplayCharacter(characterController);
                profileObject.SetOnDropCallback(OnItemDrop);

                _existingProfiles.Add(profileObject);
            }
        }
    }

    private void OnItemDrop(InventorySlot dropSlot, InventorySlot droppedSlot)
    {
        Display();
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
        if (itemCount < characterController.Character.MaxItems)
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