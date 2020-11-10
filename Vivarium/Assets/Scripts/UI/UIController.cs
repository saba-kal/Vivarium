using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Assets.Scripts.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    public delegate void ActionClick(Action action);
    public static event ActionClick OnActionClick;
    public delegate void MoveClick();
    public static event MoveClick OnMoveClick;
    public delegate void EndTurnClick();
    public static event EndTurnClick OnEndTurnClick;

    public GameObject CharacterInfoPanel;
    public GameObject ActionButtonsContainer;
    public GameObject SelectedButtonIndicator;
    public GameObject GameOverScreen;
    public TextMeshProUGUI GameOverText;
    public InventoryUIController InventoryUIController;
    public RewardsUIController RewardsUIController;
    public Button ActionButtonPrefab;
    public Button MoveButton;
    public Button EndTurnButton;
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
        MoveButton.onClick.AddListener(() =>
        {
            OnMoveClick?.Invoke();
            SelectedButtonIndicator.transform.SetParent(MoveButton.transform, false);
        });
        EndTurnButton.onClick.AddListener(() => OnEndTurnClick?.Invoke());
    }

    private void Update()
    {
        if (CommandController.Instance?.CommandsAreExecuting() == true)
        {
            EndTurnButton.interactable = false;
        }
        else
        {
            EndTurnButton.interactable = true;
        }
    }

    public void ShowCharacterInfo(CharacterController characterController)
    {
        HideCharacterInfo();
        _selectedCharacter = characterController;

        CharacterInfoPanel.SetActive(true);
        MoveButton.interactable = !_charactersWithDisabledMoves.Contains(characterController.Id);

        DisplayActions(characterController);
        InventoryUIController.DisplayCharacterInventory(characterController);
        InventoryUIController.SetActionButtonsDisabled(_charactersWithDisabledActions.Contains(characterController.Id));
    }

    private void DisplayActions(CharacterController characterController)
    {
        MoveButton.interactable = !_charactersWithDisabledMoves.Contains(characterController.Id);

        var yOffset = 0f;
        foreach (var action in characterController?.Character?.Weapon?.Actions ?? new List<Action>())
        {
            var actionButton = Instantiate(ActionButtonPrefab);
            actionButton.transform.SetParent(ActionButtonsContainer.transform, false);
            actionButton.transform.Translate(new Vector3(0, yOffset));
            _existingActionButtons.Add(actionButton);

            if (_charactersWithDisabledActions.Contains(characterController.Id))
            {
                actionButton.interactable = false;
            }
            else
            {
                actionButton.onClick.AddListener(() =>
                {
                    OnActionClick?.Invoke(action);
                    SelectedButtonIndicator.transform.SetParent(actionButton.transform, false);
                });
            }
            var buttonText = actionButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = action.Name;
            yOffset -= SpaceBetweenButtons;
        }
    }

    public void HideCharacterInfo()
    {
        _selectedCharacter = null;

        SelectedButtonIndicator.transform.SetParent(MoveButton.transform, false);
        foreach (var button in _existingActionButtons)
        {
            Destroy(button.gameObject);
        }
        _existingActionButtons = new List<Button>();
        CharacterInfoPanel.SetActive(false);
    }

    public void DisableActionsForCharacter()
    {
        if (_selectedCharacter != null)
        {
            DisableActionsForCharacter(_selectedCharacter.Id);
        }
    }

    public void DisableActionsForCharacter(string characterId)
    {
        _charactersWithDisabledActions.Add(characterId);
        foreach (var button in _existingActionButtons)
        {
            button.interactable = false;
        }
        InventoryUIController.SetActionButtonsDisabled(true);
    }

    public void DisableMoveForCharacter(string characterId)
    {
        MoveButton.interactable = false;
        _charactersWithDisabledMoves.Add(characterId);
    }

    public void EnableAllButtons()
    {
        _charactersWithDisabledActions = new List<string>();
        _charactersWithDisabledMoves = new List<string>();
        InventoryUIController.SetActionButtonsDisabled(false);
    }

    public void GameOver(string gameoverText)
    {
        GameOverText.text = gameoverText;
        GameOverScreen.SetActive(true);
        Instance = null;
    }
}
