using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.UI;

/// <summary>
/// Base class that handles UI, used to control the UI scenes of the game.
/// </summary>
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
    /// <summary>
    /// Opens UI that displays character information during a level. Usually accessed by clicking onto a character.
    /// </summary>
    /// /// <param name="characterController">The character controller of the character who's information is shown.</param>
    public void ShowCharacterInfo(CharacterController characterController)
    {
        HideCharacterInfo();

        CharacterInfoPanel.SetActive(true);
        InventoryUIController.DisplayCharacterInventory(characterController);
        //InventoryUIController.SetActionButtonsDisabled(_charactersWithDisabledActions.Contains(characterController.Id));
        UnitInspectionController.Display(characterController);
    }
    /// <summary>
    /// Hides the character information UI panel. User does this by clicking off of a character or performing an action.
    /// </summary>
    public void HideCharacterInfo()
    {
        CharacterInfoPanel.SetActive(false);
    }
    /// <summary>
    /// Enables every button for a character on their Info Panel. Done for level startup.
    /// </summary>
    public void EnableAllButtons()
    {
        UnitInspectionController.EnableAllActions();
        InventoryUIController.EnableAllActions();
    }
    /// <summary>
    /// Grabs the appropriate text for either a game win or loss and displays it to the player. Also activates the GameOverScreen where a new game can be started.
    /// </summary>
    /// /// <param name="gameoverText">The appopriate text needed to be displayed to the UI text.</param>
    public void GameOver(string gameoverText)
    {
        GameOverText.text = gameoverText;
        GameOverScreen.SetActive(true);
    }
}
