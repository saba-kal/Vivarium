using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public delegate void Death(CharacterController characterController);
    public static event Death OnDeath;

    public string Id;
    public Character Character;
    public bool IsEnemy;

    private float _currentHealth;
    private float _currentshieldHealth;
    private HealthController _healthController;
    private MoveController _moveController;
    private List<ActionController> _actionControllers;
    private List<ActionViewer> _actionViewers;
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
        _currentshieldHealth = Character.Shield?.Health ?? 0;
        _healthController = GetComponent<HealthController>();
        _healthController?.SetHealthStats(_currentHealth, maxHealth, _currentshieldHealth, _currentshieldHealth);
        _moveController = GetComponent<MoveController>();
        _actionControllers = GetComponents<ActionController>().ToList();
        _actionViewers = GetComponents<ActionViewer>().ToList();
        PlaceSelfInGrid();
    }

    public void Select()
    {
        _isSelected = true;
        UIController.Instance.ShowCharacterInfo(this);
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

    public virtual void MoveToTile(Tile tile, System.Action onMoveComplete = null)
    {
        if (tile == null)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Unable to move to a null tile.");
            return;
        }
        if (_moveController != null)
        {
            _moveController.MoveToTile(GetGridPosition(), tile, onMoveComplete);
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

        if (_healthController.TakeDamage(damage))
        {
            OnDeath(this);
            return true;
        }

        return false;
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
        var gricCellPosition = TileGridController.Instance.GetGrid().ConvertToGridCellPosition(transform.position);
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
        return TileGridController.Instance.GetGrid().GetValue(transform.position);
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

    public void Equip(Item item)
    {
        if ((item.Type != ItemType.Weapon) || (item.Type != ItemType.Shield))
        {
            Debug.LogError($"Character {Character.Name}: cannot equip non-weapon items.");
            return;
        }

        if (InventoryManager.GetCharacterItem(Id, item.Id) == null)
        {
            Debug.LogError($"Character {Character.Name}: cannot equip item that does not exist in character's inventory.");
            return;
        }

        if (item.Type == ItemType.Weapon)
        {
            Character.Weapon = (Weapon)item;
        }
        else if (item.Type == ItemType.Shield)
        {
            Character.Shield = (Shield)item;
        }
        //TODO: switch out weapon model here.
    }

    public List<CharacterController> GetAdjacentCharacters(CharacterSearchType characterSearchType)
    {
        var grid = TileGridController.Instance.GetGrid();
        var adjacentCharacterIds = new List<string>();
        var currentGridPosition = grid.GetValue(transform.position);

        //Right
        if (!string.IsNullOrEmpty(grid.GetValue(currentGridPosition.GridX + 1, currentGridPosition.GridY)?.CharacterControllerId))
        {
            adjacentCharacterIds.Add(grid.GetValue(currentGridPosition.GridX + 1, currentGridPosition.GridY).CharacterControllerId);
        }

        //Left
        if (!string.IsNullOrEmpty(grid.GetValue(currentGridPosition.GridX - 1, currentGridPosition.GridY)?.CharacterControllerId))
        {
            adjacentCharacterIds.Add(grid.GetValue(currentGridPosition.GridX - 1, currentGridPosition.GridY).CharacterControllerId);
        }

        //Up
        if (!string.IsNullOrEmpty(grid.GetValue(currentGridPosition.GridX, currentGridPosition.GridY + 1)?.CharacterControllerId))
        {
            adjacentCharacterIds.Add(grid.GetValue(currentGridPosition.GridX, currentGridPosition.GridY + 1).CharacterControllerId);
        }

        //Down
        if (!string.IsNullOrEmpty(grid.GetValue(currentGridPosition.GridX, currentGridPosition.GridY - 1)?.CharacterControllerId))
        {
            adjacentCharacterIds.Add(grid.GetValue(currentGridPosition.GridX, currentGridPosition.GridY - 1).CharacterControllerId);
        }

        return TurnSystemManager.Instance.GetCharacterWithIds(adjacentCharacterIds, characterSearchType);
    }

    public void DestroyCharacter()
    {
        Debug.Log($"Character {Character.Name} died.");
        var currentGridPosition = TileGridController.Instance.GetGrid().GetValue(transform.position);
        if (currentGridPosition != null)
        {
            currentGridPosition.CharacterControllerId = null;
        }
        else
        {
            Debug.LogError("Unable to remove character ID from grid because current grid cell position is null.");
        }
        Destroy(gameObject, 0.1f);
    }

    public void Consume(Item item)
    {
        if (item.Type != ItemType.Consumable)
        {
            Debug.LogError($"Character {Character.Name}: cannot eat non-consumable items.");
            return;
        }

        if (InventoryManager.GetCharacterItem(Id, item.Id) == null)
        {
            Debug.LogError($"Character {Character.Name}: cannot eat item that does not exist.");
            return;
        }

        var consumable = (Consumable)item;
        switch (consumable.ConsumableType)
        {
            case (ConsumableType.Honey):
                Heal(consumable.value);
                break;
        }
        InventoryManager.RemoveCharacterItem(Id, consumable.Id);
    }

    public void Heal(float healAmount)
    {
        _healthController.Healing(healAmount);
    }

    public void RegenShield(float shieldAmount)
    {
        _healthController.RegenerateShield(shieldAmount);
    }
}
