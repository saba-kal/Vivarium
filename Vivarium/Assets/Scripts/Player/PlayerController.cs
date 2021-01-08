using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void ObjectiveCapture();
    public static event ObjectiveCapture OnObjectiveCapture;
    public delegate void AllCharactersDead();
    public static event AllCharactersDead OnAllCharactersDead;

    public List<CharacterController> PlayerCharacters;
    private CharacterController _selectedCharacter;

    private bool _actionIsSelected = false;
    private Action _selectedAction;
    private float _selectedActionRange;

    void OnEnable()
    {
        TileGridController.OnGridCellClick += OnGridCellClick;
        UIController.OnActionClick += SelectAction;
        UIController.OnMoveClick += SelectMove;
        UIController.OnShowMapClick += DisableCharacters;
        PrepMenuUIController.OnBackClick += EnableCharacters;
        InventoryUIController.OnEquipClick += DeselectAction;
        CharacterController.OnDeath += OnCharacterDeath;
    }

    void OnDisable()
    {
        TileGridController.OnGridCellClick -= OnGridCellClick;
        UIController.OnActionClick -= SelectAction;
        UIController.OnMoveClick -= SelectMove;
        UIController.OnShowMapClick -= DisableCharacters;
        PrepMenuUIController.OnBackClick -= EnableCharacters;
        InventoryUIController.OnEquipClick -= DeselectAction;
        CharacterController.OnDeath -= OnCharacterDeath;
    }

    private void OnGridCellClick(Tile selectedTile)
    {

        if (selectedTile == null || CommandController.Instance.CommandsAreExecuting())
        {
            return;
        }

        //Action is selected. So this grid cell click is for executing the action.
        if (_actionIsSelected && ActionIsWithinRange(selectedTile) && !_selectedCharacter.IsEnemy)
        {
            PerformAction(selectedTile);
            UIController.Instance.DisableActionsForCharacter(_selectedCharacter.Id);
        }
        //A character is selected. The tile that was clicked is within the character's move range.
        else if (_selectedCharacter != null &&
            _selectedCharacter.IsAbleToMoveToTile(selectedTile) && _selectedCharacter.IsEnemy == false)
        {
            _selectedCharacter.MoveToTile(selectedTile, () =>
            {
                OnCharacterMoveComplete(selectedTile);
            });
            UIController.Instance.DisableMoveForCharacter(_selectedCharacter.Id);
        }
        //Grid cell click was probably on a character.
        else
        {
            GetSelectedCharacter(selectedTile);
        }
    }

    private void GetSelectedCharacter(Tile tile)
    {
        DeselectAction();
        _selectedCharacter?.Deselect();
        _selectedCharacter = null;

        var grid = TileGridController.Instance.GetGrid();
        foreach (var character in PlayerCharacters.Concat(TurnSystemManager.Instance.AIManager.AICharacters))
        {
            //Character was most likely deleted.
            if (character == null)
            {
                continue;
            }

            grid.GetGridCoordinates(character.transform.position, out var x, out var y);
            if (tile.GridX == x && tile.GridY == y)
            {
                _selectedCharacter = character;
                _selectedCharacter.Select();
                if (_selectedCharacter.IsEnemy)
                {
                    _selectedCharacter.ShowMoveRadius();
                }
                return;
            }
        }
    }

    private void SelectMove()
    {
        DeselectAction();
        _selectedCharacter?.ShowMoveRadius();
    }

    private void DeselectMove()
    {
        _selectedCharacter?.HideMoveRadius();
    }

    private void SelectAction(Action action)
    {
        var attackViewer = _selectedCharacter.GetActionViewer(action);
        if (attackViewer == null)
        {
            Debug.LogError("Unable to display attack because an attack viewer could not be found.\n" +
                "Make sure your character has a weapon with attacks, and an attack viewer that references it.");
            return;
        }

        DeselectAction();
        DeselectMove();

        _actionIsSelected = true;
        _selectedAction = action;
        var actionAOE = StatCalculator.CalculateStat(action, StatType.AttackAOE);
        _selectedActionRange = StatCalculator.CalculateStat(action, StatType.AttackRange);
        attackViewer.DisplayAction(actionAOE, _selectedActionRange);
        UIController.Instance.DisplayActionStats(_selectedAction);

        Debug.Log($"Attack '{action.Name}' has been selected.");
    }

    private void DeselectAction()
    {
        if (!_actionIsSelected)
        {
            return;
        }

        var actionViewer = _selectedCharacter.GetActionViewer(_selectedAction);
        if (actionViewer == null)
        {
            Debug.LogError("Unable hide attack because an attack viewer could not be found.");
            return;
        }

        actionViewer.HideAction();
        _actionIsSelected = false;
        _selectedAction = null;
        UIController.Instance.ClearActionStats();
    }

    private void PerformAction(Tile targetTile)
    {
        _selectedCharacter.PerformAction(_selectedAction, targetTile);
        DeselectAction();
    }

    public void EnableCharacters()
    {
        if (_selectedCharacter)
        {
            DeselectMove();
            UIController.Instance.CharacterInfoPanel.SetActive(false);
        }
        UIController.Instance.EnableAllButtons();
        foreach (var character in PlayerCharacters)
        {
            character.IsEnemy = false;
            character.SetHasAttacked(false);
            character.SetHasMoved(false);
        }
    }

    private void DisableCharacters()
    {
        foreach (var character in PlayerCharacters)
        {
            character.IsEnemy = true;
        }
    }

    private bool ActionIsWithinRange(Tile targetTile)
    {
        var targetPosition = TileGridController.Instance.GetGrid().GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
        return Vector3.Distance(_selectedCharacter.transform.position, targetPosition) < _selectedActionRange + 0.01f;
    }

    private void OnCharacterMoveComplete(Tile toTile)
    {
        if (toTile.IsObjective)
        {
            OnObjectiveCapture?.Invoke();
        }
    }

    private void OnCharacterDeath(CharacterController deadCharacterController)
    {
        for (var i = 0; i < PlayerCharacters.Count; i++)
        {
            if (PlayerCharacters[i].Id == deadCharacterController.Id)
            {
                PlayerCharacters.RemoveAt(i);
            }
        }

        deadCharacterController.DestroyCharacter();

        if (PlayerCharacters.Count == 0)
        {
            OnAllCharactersDead?.Invoke();
        }
    }

    public void HealCharacters(float healAmount)
    {
        foreach (var characterController in PlayerCharacters)
        {
            characterController.Heal(healAmount);
        }
    }

    public void RegenCharacterShields(float regenAmount)
    {
        foreach (var characterController in PlayerCharacters)
        {
            characterController.RegenShield(regenAmount);
        }
    }
}
