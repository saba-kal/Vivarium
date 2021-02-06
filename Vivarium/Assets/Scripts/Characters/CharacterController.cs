using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;
using System.Threading.Tasks;

public class CharacterController : MonoBehaviour
{
    public delegate void Death(CharacterController characterController);
    public static event Death OnDeath;

    public string Id;
    public Character Character;
    public bool IsEnemy;
    public GameObject Model;

    private float _maxHealth;
    private float _attackDamage;
    private float _maxShield;
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
        _maxHealth = StatCalculator.CalculateStat(Character, StatType.Health);
        _maxShield = Character.Shield?.Health ?? 0;
        _healthController = GetComponent<HealthController>();
        _healthController?.SetHealthStats(_maxHealth, _maxHealth, _maxShield, _maxShield);
        _moveController = GetComponent<MoveController>();
        _actionControllers = GetComponents<ActionController>().ToList();
        _actionViewers = GetComponents<ActionViewer>().ToList();
        _attackDamage = Character.AttackDamage;
        PlaceSelfInGrid();
    }

    public void Select()
    {
        _isSelected = true;
        if (!_hasMoved)
        {
            ShowMoveRadius();
        }
        UIController.Instance.ShowCharacterInfo(this);
    }

    public void Deselect()
    {
        _isSelected = false;
        UIController.Instance.HideCharacterInfo();
        HideMoveRadius();
        TileGridController.Instance.RemoveHighlights(GridHighlightRank.Secondary);
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

    // potential
    public virtual void MoveToTile(Tile tile, System.Action onMoveComplete = null, bool skipMovement = false)
    {
        if (tile == null)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Unable to move to a null tile.");
            return;
        }
        if (_moveController != null)
        {
            _moveController.MoveToTile(GetGridPosition(), tile, onMoveComplete, skipMovement);
            _hasMoved = true;
            Deselect();
        }
        else
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Cannot not move because character is missing a move controller.");
        }
    }

    public void MoveAlongPath(List<Tile> path, System.Action onMoveComplete = null, bool skipMovement = false)
    {
        if (path == null || path.Count == 0)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Unable to move because the path list is empty.");
            return;
        }

        if (_moveController != null)
        {
            _moveController.MoveAlongPath(path, onMoveComplete, skipMovement);
            UnityEngine.Debug.Log("Moving");
            if(path.Count != 1)
            {
                UnityEngine.Debug.Log("Path not 1 tile");
                _hasMoved = true;
            }
            
            Deselect();
        }
        else
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Cannot not move because character is missing a move controller.");
        }
    }

    public void PerformAction(Action attack, Tile targetTile, System.Action onActionComplete = null)
    {
        var actionController = GetActionController(attack);
        actionController.Execute(targetTile, onActionComplete);
        _hasAttacked = true;
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
            SoundManager.GetInstance()?.Play(Constants.DEATH_SOUND);
            return true;
        }

        SoundManager.GetInstance()?.Play(Constants.DAMAGE_TAKEN_SOUND);
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

    public ActionController GetActionController(Action action)
    {
        foreach (var actionController in _actionControllers)
        {
            if (actionController.ActionReference.Id == action.Id)
            {
                return actionController;
            }
        }

        ActionFactory.Create(gameObject, action, out var newActionController, out var newActionViewer);
        _actionControllers.Add(newActionController);
        _actionViewers.Add(newActionViewer);
        Debug.LogWarning($"Character \"{gameObject.name}\": Could not find attack controller that matches the attack, so one was made for it.");

        return newActionController;
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

        ActionFactory.Create(gameObject, action, out var newActionController, out var newActionViewer);
        _actionControllers.Add(newActionController);
        _actionViewers.Add(newActionViewer);
        Debug.LogWarning($"Character \"{gameObject.name}\": Could not find attack viewer that matches the attack, so one was made for it.");

        return newActionViewer;
    }

    public void Equip(Item item)
    {
        if ((item.Type != ItemType.Weapon) && (item.Type != ItemType.Shield))
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
            _healthController?.UpgradMaxShield(Character.Shield.Health);
        }
        //TODO: switch out weapon model here.
    }

    public void Unequip(Item item)
    {
        if (item.Type == ItemType.Weapon && item.Id == Character.Weapon?.Id)
        {
            Character.Weapon = null;
        }
        else if (item.Type == ItemType.Shield && item.Id == Character.Shield?.Id)
        {
            Character.Shield = null;
            _healthController?.RemoveShield();
        }
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
        DetachCamera();

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

    public void DetachCamera()
    {
        for (int x = 0; x < this.transform.childCount; x++)
        {
            if (this.transform.GetChild(x).tag == "MasterCamera")
            {
                this.transform.GetChild(x).transform.GetComponent<CameraFollower>().ResetCamera();
            }
        }
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
            case (ConsumableType.AtkBuff):
                AtkBuff(consumable.value);
                break;
            case (ConsumableType.MovBuff):
                MovBuff(consumable.value);
                break;
        }
        InventoryManager.RemoveCharacterItem(Id, consumable.Id);
    }

    public void Heal(float healAmount)
    {
        _healthController.Healing(healAmount);
    }

    public void AtkBuff(float attackAmount)
    {
        Character.AttackDamage += attackAmount;
    }

    public void MovBuff(float moveAmount)
    {
        Character.MoveRange += moveAmount;
    }
    public void RegenShield(float shieldAmount)
    {
        _healthController.RegenerateShield(shieldAmount);
    }

    public HealthController GetHealthController()
    {
        return _healthController;
    }

    public float GetAttackDamage()
    {
        return _attackDamage;
    }

    public bool ItemIsEquipped(Item item)
    {
        return (item.Type == ItemType.Shield || item.Type == ItemType.Weapon) &&
            (Character.Weapon?.Id == item.Id || Character.Shield?.Id == item.Id);
    }
}
