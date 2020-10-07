﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public List<CharacterController> PlayerCharacters;

    private Grid<Tile> _grid;
    private CharacterController _selectedCharacter;

    private bool _actionIsSelected = false;
    private Action _selectedAction;
    private float _selectedActionRange;
    private List<CharacterController> _enemies = new List<CharacterController>();

    void OnEnable()
    {
        TileGridController.OnGridCellClick += OnGridCellClick;
        UIController.OnActionClick += SelectAction;
        UIController.OnMoveClick += SelectMove;
    }

    void OnDisable()
    {
        TileGridController.OnGridCellClick -= OnGridCellClick;
        UIController.OnActionClick -= SelectAction;
        UIController.OnMoveClick -= SelectMove;
    }


    // Start is called before the first frame update
    void Start()
    {
        _grid = TileGridController.Instance.GetGrid();
        var enemyObjects = GameObject.FindGameObjectsWithTag(Constants.ENEMY_CHAR_TAG);
        foreach (var enemyObject in enemyObjects)
        {
            var enemy = enemyObject.GetComponent<CharacterController>();
            if (enemy != null)
            {
                _enemies.Add(enemy);
            }
        }
    }

    private void OnGridCellClick(Tile selectedTile)
    {
        if (selectedTile == null || CommandController.Instance.CommandsAreExecuting())
        {
            return;
        }

        //Action is selected. So this grid cell click is for executing the action.
        if (_actionIsSelected && ActionIsWithinRange(selectedTile))
        {
            PerformAction(selectedTile);
            UIController.Instance.DisableActionsForCharacter(_selectedCharacter.Character.Id);
        }
        //A character is selected. The tile that was clicked is within the character's move range.
        else if (_selectedCharacter != null &&
            _selectedCharacter.IsAbleToMoveToTile(selectedTile))
        {
            _selectedCharacter.MoveToTile(selectedTile);
            UIController.Instance.DisableMoveForCharacter(_selectedCharacter.Character.Id);
        }
        //Grid cell click was probably on a character.
        else
        {
            GetSelectedCharacter(selectedTile);
        }
    }

    private void GetSelectedCharacter(Tile tile)
    {
        foreach (var character in PlayerCharacters)
        {
            _grid.GetGridCoordinates(character.transform.position, out var x, out var y);
            if (tile.GridX == x && tile.GridY == y)
            {
                //UIController.Instance
                _selectedCharacter = character;
                _selectedCharacter.Select();
                return;
            }
        }

        DeselectAction();
        _selectedCharacter?.Deselect();
        _selectedCharacter = null;
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
    }

    private void PerformAction(Tile targetTile)
    {
        _selectedCharacter.PerformAction(_selectedAction, targetTile);
        DeselectAction();
    }

    public void EnableCharacters()
    {
        UIController.Instance.EnableAllButtons();
        foreach (var character in PlayerCharacters)
        {
            character.SetHasAttacked(false);
            character.SetHasMoved(false);
        }
    }

    private bool ActionIsWithinRange(Tile targetTile)
    {
        var targetPosition = TileGridController.Instance.GetGrid().GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
        return Vector3.Distance(_selectedCharacter.transform.position, targetPosition) < _selectedActionRange + 0.01f;
    }
}