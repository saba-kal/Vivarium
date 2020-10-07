using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public string Id;
    public Character Character;
    public bool IsEnemy;

    private float _currentHealth;
    private HealthController _healthController;
    private MoveController _moveController;
    private List<ActionController> _actionControllers;
    private List<ActionViewer> _actionViewers;
    protected Grid<Tile> _grid;
    private bool _isSelected = false;
    private bool _hasMoved = false;
    private bool _hasAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        VirtualStart();
    }

    protected virtual void VirtualStart()
    {
        var maxHealth = StatCalculator.CalculateStat(Character, StatType.Health);
        _currentHealth = maxHealth;
        _healthController = GetComponent<HealthController>();
        _healthController?.SetHealthStats(_currentHealth, maxHealth);
        _moveController = GetComponent<MoveController>();
        _actionControllers = GetComponents<ActionController>().ToList();
        _actionViewers = GetComponents<ActionViewer>().ToList();
        _grid = TileGridController.Instance.GetGrid();
        PlaceSelfInGrid();
    }

    public void Select()
    {
        _isSelected = true;

        if (!_hasMoved)
        {
            ShowMoveRadius();
        }
        UIController.Instance.ShowCharacterInfo(Character);
    }

    public void Deselect()
    {
        _isSelected = false;
        UIController.Instance.HideCharacterInfo();
        HideMoveRadius();
    }

    public bool IsAbleToMove()
    {
        return !_hasMoved;
    }

    public virtual bool IsAbleToMoveToTile(Tile tile)
    {
        return
            tile != null &&
            !_hasMoved && _isSelected &&
            _moveController.IsAbleToMoveToTile(tile) &&
            string.IsNullOrEmpty(tile.CharacterControllerId);
    }

    public bool IsAbleToAttack()
    {
        return !_hasAttacked;
    }

    public virtual void MoveToTile(Tile tile)
    {
        if (tile == null)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Unable to move to a null tile.");
            return;
        }
        if (_moveController != null)
        {
            _moveController.MoveToTile(GetGridPosition(), tile);
            _hasMoved = true;
            Deselect();
        }
        else
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Cannot not move because character is missing a move controller.");
        }
    }

    public void PerformAction(Action attack, Tile targetTile)
    {
        var actionController = GetActionController(attack);
        if (actionController != null)
        {
            actionController.Execute(targetTile);
            _hasAttacked = true;
        }
        else
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Could not find attack controller that matches the attack.");
        }
    }

    public bool TakeDamage(float damage)
    {
        if (_healthController == null)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Cannot take damage because character is missing a health controller.");
            return false;
        }
        return _healthController.TakeDamage(damage);
    }

    public void SetHasMoved(bool hasMoved)
    {
        _hasMoved = hasMoved;
    }

    public void SetHasAttacked(bool hasAttacked)
    {
        _hasAttacked = hasAttacked;
    }

    protected virtual void PlaceSelfInGrid()
    {
        var gricCellPosition = _grid.ConvertToGridCellPosition(transform.position);
        transform.position = gricCellPosition;
        GetGridPosition().CharacterControllerId = Id;
    }

    public void HideMoveRadius()
    {
        _moveController.HideMoveRadius();
    }

    public void ShowMoveRadius()
    {
        _moveController.ShowMoveRadius();
    }

    public Dictionary<(int, int), Tile> CalculateAvailableMoves()
    {
        return _moveController.CalculateAvailableMoves();
    }

    private ActionController GetActionController(Action action)
    {
        foreach (var actionController in _actionControllers)
        {
            if (actionController.ActionReference.Id == action.Id)
            {
                return actionController;
            }
        }

        return null;
    }

    private Tile GetGridPosition()
    {
        return _grid.GetValue(transform.position);
    }

    public ActionViewer GetActionViewer(Action action)
    {
        foreach (var actionViewer in _actionViewers)
        {
            if (actionViewer.ActionReference.Id == action.Id)
            {
                return actionViewer;
            }
        }

        return null;
    }
}
