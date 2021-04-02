using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    public delegate void EndTurnClick();
    public static event EndTurnClick OnEndTurnClick;
    public delegate void UndoClick();
    public static event UndoClick OnUndoClick;

    public GameObject CharacterInfoPanel;
    public GameObject GameOverScreen;
    public TextMeshProUGUI GameOverText;
    public InventoryUIController InventoryUIController;
    public RewardsUIController RewardsUIController;
    public UnitInspectionController UnitInspectionController;
    public Button EndTurnButton;
    public Button UndoButton;
    public float SpaceBetweenButtons = 2f;

    private CharacterController _selectedCharacter;
    private List<Button> _existingActionButtons = new List<Button>();
    private List<string> _charactersWithDisabledActions = new List<string>();
    private List<string> _charactersWithDisabledMoves = new List<string>();


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

    // Use this for initialization
    void Start()
    {
        CharacterInfoPanel.SetActive(false);
        EndTurnButton.onClick.AddListener(() => OnEndTurnClick?.Invoke());
        UndoButton.onClick.AddListener(() => OnUndoClick?.Invoke());
    }

    private void Update()
    {
        if (CommandController.Instance?.CommandsAreExecuting() == true ||
            TurnSystemManager.Instance?.IsPlayersTurn() == false)
        {
            EndTurnButton.interactable = false;
            UndoButton.interactable = false;
        }
        else
        {
            EndTurnButton.interactable = true;
            UndoButton.interactable = UndoMoveController.Instance.IsUndoTrue;
        }
    }

    public void ShowCharacterInfo(CharacterController characterController)
    {
        HideCharacterInfo();

        CharacterInfoPanel.SetActive(true);
        InventoryUIController.DisplayCharacterInventory(characterController);
        //InventoryUIController.SetActionButtonsDisabled(_charactersWithDisabledActions.Contains(characterController.Id));
        UnitInspectionController.Display(characterController);
    }

    public void HideCharacterInfo()
    {
        CharacterInfoPanel.SetActive(false);
    }

    public void EnableAllButtons()
    {
        UnitInspectionController.EnableAllActions();
        InventoryUIController.EnableAllActions();
    }

    public void GameOver(string gameoverText)
    {
        GameOverText.text = gameoverText;
        GameOverScreen.SetActive(true);
    }
}
