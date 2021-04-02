using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles the UI logic for the unit inspection dialog.
/// </summary>
public class UnitInspectionController : MonoBehaviour
{
    public static UnitInspectionController Instance { get; private set; }
    public delegate void ActionClick(Action action);
    public static event ActionClick OnActionClick;
    public delegate void MoveClick();
    public static event MoveClick OnMoveClick;
    public delegate void TradeClick();
    public static event TradeClick OnTradeClick;

    public string MoveButtonText = "Move";
    public string TradeButtonText = "Trade";

    public GameObject ActionButtonsContainer;
    public Button ActionButtonPrefab;
    public GameObject SelectedButtonIndicatorPrefab;

    public TextMeshProUGUI UnitNameText;
    public TextMeshProUGUI UnitStatsText;
    public TextMeshProUGUI UnitAbilityText;

    private Button _moveButton;
    private Button _tradeButton;
    private CharacterController _characterController;
    private List<string> _charactersWithDisabledActions = new List<string>();
    private List<string> _charactersWithDisabledMoves = new List<string>();
    private List<string> _charactersWithDisabledTrade = new List<string>();
    private List<Button> _weaponActionButtons = new List<Button>();
    private GameObject _selectedButtonIndicator;

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

    /// <summary>
    /// Displays unit inspection menu for a selected character.
    /// </summary>
    /// <param name="characterController">The character controller for the selected character.</param>
    public void Display(CharacterController characterController)
    {
        _characterController = characterController;
        AddCharacterActions();
        DisplayUnitStats();
    }

    #region Actions

    private void AddCharacterActions()
    {
        ClearCharacterActions();
        AddWeaponActionButtons();
        AddMoveActionButton();
        if (!_characterController.IsEnemy)
        {
            AddTradeActionButton();
        }
    }

    private void ClearCharacterActions()
    {
        _weaponActionButtons = new List<Button>();
        foreach (Transform child in ActionButtonsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        if (_selectedButtonIndicator != null)
        {
            Destroy(_selectedButtonIndicator);
        }

        _selectedButtonIndicator = null;
    }

    private void AddMoveActionButton()
    {
        _moveButton = Instantiate(ActionButtonPrefab);
        _moveButton.transform.SetParent(ActionButtonsContainer.transform, false);
        _moveButton.interactable = !_charactersWithDisabledMoves.Contains(_characterController.Id);
        _moveButton.onClick.AddListener(() =>
        {
            OnMoveClick?.Invoke();
            UpdateSelectedActionIndicator(_moveButton);
        });

        var buttonText = _moveButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = MoveButtonText;

        UpdateSelectedActionIndicator(_moveButton);
    }

    private void AddWeaponActionButtons()
    {
        foreach (var action in _characterController?.Character?.Weapon?.Actions ?? new List<Action>())
        {
            var actionButton = Instantiate(ActionButtonPrefab);
            actionButton.transform.SetParent(ActionButtonsContainer.transform, false);

            if (_charactersWithDisabledActions.Contains(_characterController.Id))
            {
                actionButton.interactable = false;
            }
            else if (action.ControllerType == ActionControllerType.Skewer && !_characterController.IsAbleToMove() &&
                !_characterController.IsEnemy)
            {
                actionButton.interactable = false;
            }
            else
            {
                actionButton.onClick.AddListener(() =>
                {
                    OnActionClick?.Invoke(action);
                    UpdateSelectedActionIndicator(actionButton);
                });
            }
            var buttonText = actionButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = action.Flavor.Name;

            var tooltip = actionButton.GetComponent<Tooltip>();
            if (tooltip != null)
            {
                tooltip.SetTooltipData(action);
            }

            _weaponActionButtons.Add(actionButton);
        }
    }

    private void AddTradeActionButton()
    {
        _tradeButton = Instantiate(ActionButtonPrefab);
        _tradeButton.transform.SetParent(ActionButtonsContainer.transform, false);
        _tradeButton.interactable = !_charactersWithDisabledTrade.Contains(_characterController.Id);
        _tradeButton.onClick.AddListener(() =>
        {
            OnTradeClick?.Invoke();
            UpdateSelectedActionIndicator(_tradeButton);
        });

        var buttonText = _tradeButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = TradeButtonText;
    }

    private void UpdateSelectedActionIndicator(Button selectedAction)
    {
        if (_selectedButtonIndicator == null)
        {
            _selectedButtonIndicator = Instantiate(SelectedButtonIndicatorPrefab);
        }

        _selectedButtonIndicator.transform.SetParent(selectedAction.transform, false);
    }

    /// <summary>
    /// Disables the move action for a character.
    /// </summary>
    /// <param name="characterId"></param>
    public void DisableMoveForCharacter(string characterId)
    {
        if (_moveButton != null)
        {
            _moveButton.interactable = false;
        }
        _charactersWithDisabledMoves.Add(characterId);
    }

    /// <summary>
    /// Enables all actions.
    /// </summary>
    public void EnableAllActions()
    {
        _charactersWithDisabledActions = new List<string>();
        _charactersWithDisabledMoves = new List<string>();
        _charactersWithDisabledTrade = new List<string>();
    }

    /// <summary>
    /// Disables weapon actions for the selected character.
    /// </summary>
    public void DisableWeaponActionsForCharacter()
    {
        if (_characterController != null)
        {
            DisableWeaponActionsForCharacter(_characterController.Id);
        }
    }

    /// <summary>
    /// Disables weapon actions for a given character ID.
    /// </summary>
    /// <param name="characterId">The unique ID of the character.</param>
    public void DisableWeaponActionsForCharacter(string characterId)
    {
        _charactersWithDisabledActions.Add(characterId);
        foreach (var button in _weaponActionButtons)
        {
            button.interactable = false;
        }
    }

    /// <summary>
    /// Disables the trade action for a given character ID.
    /// </summary>
    /// <param name="characterId">The unique ID of the character.</param>
    public void DisableTradeActionForCharacter(string characterId)
    {
        _charactersWithDisabledTrade.Add(characterId);
        _tradeButton.interactable = false;
    }

    #endregion

    #region Stats

    private void DisplayUnitStats()
    {
        UnitNameText.text = _characterController.Character.Flavor.Name;

        UnitStatsText.text = $"{BuildStatRangeText(StatType.Damage)}\n";
        UnitStatsText.text += $"{BuildStatRangeText(StatType.MoveRadius)}\n";
        UnitStatsText.text += $"{BuildHealthText()}\n";
        UnitStatsText.text += $"{BuildShieldText()}";

        UnitAbilityText.text = $"Ability: {_characterController.Character.Flavor.Description}";
    }

    private string BuildStatRangeText(StatType statType)
    {
        var statText = "";
        switch (statType)
        {
            case StatType.Damage:
                statText = "Attack: ";
                break;
            case StatType.MoveRadius:
                statText = "Range: ";
                break;
            default:
                return $"UNABLE TO BUILD RANGE TEXT FOR STAT {statType}";
        }

        var minDamage = StatCalculator.CalculateStat(_characterController.Character, statType, StatCalculationType.Min);
        var maxDamage = StatCalculator.CalculateStat(_characterController.Character, statType, StatCalculationType.Max);
        if (Mathf.Approximately(minDamage, maxDamage))
        {
            statText += $"{minDamage:n0}";
        }
        else
        {
            statText += $"{minDamage:n0}-{maxDamage:n0}";
        }

        return statText;
    }

    private string BuildHealthText()
    {
        var currentHealth = _characterController.GetHealthController().GetCurrentHealth();
        var maxHealth = StatCalculator.CalculateStat(_characterController.Character, StatType.Health);

        var healthText = $"Health: {currentHealth:n0}/{maxHealth:n0}";

        return healthText;
    }

    private string BuildShieldText()
    {
        if (_characterController.Character.Shield == null)
        {
            return "";
        }

        var currentShield = _characterController.GetHealthController().GetCurrentShield();
        var maxShield = _characterController.Character.Shield.Health;

        var healthText = $"Health: {currentShield:n0}/{maxShield:n0}";

        return healthText;
    }

    #endregion
}
