using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles logic for how a <see cref="Character"/> interacts with the game.
/// </summary>
public class CharacterController : MonoBehaviour
{
    public delegate void Death(CharacterController characterController);
    public static event Death OnDeath;
    public delegate void Move(CharacterController characterController, Vector3 oldPosition);
    public static event Move OnMove;
    public delegate void SelectEvent(CharacterController characterController);
    public static event SelectEvent OnSelect;
    public delegate void DeselectEvent(CharacterController characterController);
    public static event DeselectEvent OnDeselect;
    public delegate void DamageTaken(CharacterController characterController);
    public static event DamageTaken OnDamageTaken;

    public string Id;
    public Character Character;
    public bool IsEnemy;
    public bool IsDisabled = false;
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
    private float _savedMoveRange;
    private GameObject _meleeWeapon;
    private GameObject _rangedWeapon;
    private int _equippedWeaponPosition = -1;
    private int _equippedShieldPosition = -1;

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
        _savedMoveRange = Character.MoveRange;
        _meleeWeapon = Utils.FindObjectWithTag(gameObject, Constants.MELEE_WEAPON_TAG);
        _rangedWeapon = Utils.FindObjectWithTag(gameObject, Constants.RANGED_WEAPON_TAG);
        SwitchWeaponModel();
        PlaceSelfInGrid();
        Debug.Log(gameObject.name);
        Debug.Log(gameObject.transform.childCount);
        DisplayEquipment(Character.Weapon);
        Debug.Log("INITIAL WEAPON: " + Character.Weapon);
        //var weaponSpawn = gameObject.GetComponentInChildren<ShowWeaponScript>();
        //if (weaponSpawn != null)
        //{
        //    weaponSpawn.SpawnRandomWeapon();
        //}

    }

    /// <summary>
    /// Sets this character as the selected one.
    /// </summary>
    public void Select()
    {
        if(!GetGridPosition().IsObjective || IsEnemy)
        {
            _isSelected = true;
            if (!_hasMoved)
            {
                ShowMoveRadius();
            }
            UIController.Instance.ShowCharacterInfo(this);
            OnSelect?.Invoke(this);

            Dictionary<(int, int), Tile> tempDict = new Dictionary<(int, int), Tile>();
            tempDict.Add((0, 0), GetGridPosition());
            TileGridController.Instance.HighlightTiles(tempDict, GridHighlightRank.Secondary);
        }
    }

    /// <summary>
    /// De-selects the character.
    /// </summary>
    public void Deselect()
    {
        _isSelected = false;
        UIController.Instance.HideCharacterInfo();
        HideMoveRadius();
        TileGridController.Instance.RemoveHighlights(GridHighlightRank.Secondary);
        OnDeselect?.Invoke(this);
    }

    /// <summary>
    /// Gets whether or not this character is able to move.
    /// </summary>
    /// <returns>Whether or not this character is able to move.</returns>
    public bool IsAbleToMove()
    {
        return !_hasMoved;
    }

    /// <summary>
    /// Gets whether or not this character is able to move to a specific <see cref="Tile"/>.
    /// </summary>
    /// <param name="tile">The tile to move to.</param>
    /// <returns></returns>
    public virtual bool IsAbleToMoveToTile(Tile tile)
    {
        return
            tile != null &&
            !_hasMoved && _isSelected &&
            _moveController.IsAbleToMoveToTile(tile) &&
            string.IsNullOrEmpty(tile.CharacterControllerId);
    }

    /// <summary>
    /// Gets whether or not this character can attack.
    /// </summary>
    /// <returns>Whether or not this character can attack.</returns>
    public bool IsAbleToAttack()
    {
        return !_hasAttacked;
    }

    /// <summary>
    /// Moves the character to a specific tile.
    /// </summary>
    /// <param name="tile">The tile to move to.</param>
    /// <param name="onMoveComplete">Callback for when the movement is complete.</param>
    /// <param name="skipMovement">Boolean flag for skipping the move animation.</param>
    public virtual void MoveToTile(Tile tile, System.Action onMoveComplete = null, bool skipMovement = false)
    {
        var oldPosition = transform.position;
        if (tile == null)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Unable to move to a null tile.");
            return;
        }

        if (!FollowsTutorialRestrictions(tile))
        {
            Debug.Log("Attempted to move to a tile other than the one instructed");
            return;
        }

        if (_moveController != null)
        {
            _moveController.MoveToTile(GetGridPosition(), tile, () =>
            {
                onMoveComplete();
                Select();
            }, skipMovement);
            _hasMoved = true;

            Deselect();
            OnMove?.Invoke(this, oldPosition);
            HideMoveRadius();
        }
        else
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Cannot not move because character is missing a move controller.");
        }
    }

    private bool FollowsTutorialRestrictions(Tile tile)
    {
        if (TutorialManager.GetIsTutorial())
        {
            var grid = TileGridController.Instance.GetGrid();
            Tile enemyTile = LevelManager.Instance.LevelGenerator.GetPossibleEnemySpawnTiles().Values.ToList()[0];
            Tile requiredTile = grid.GetValue(enemyTile.GridX, enemyTile.GridY - 1);
            if (TutorialManager.Instance.GetCurrentIndex() == 6)
            {
                if (requiredTile == tile)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (TutorialManager.Instance.GetCurrentIndex() < 6)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Moves character along a tile path.
    /// </summary>
    /// <param name="path">The path to move along.</param>
    /// <param name="onMoveComplete">Callback for when the movement is complete.</param>
    /// <param name="skipMovement">Boolean flag for skipping the move animation.</param>
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
            if (path.Count != 1)
            {
                _hasMoved = true;
            }

            Deselect();
            OnMove?.Invoke(this, Vector3.zero);
        }
        else
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Cannot not move because character is missing a move controller.");
        }
    }

    /// <summary>
    /// Executes a given <see cref="Action"/>.
    /// </summary>
    /// <param name="attack">The <see cref="Action"> to execute.</param>
    /// <param name="targetTile">The target tile to execute the action on.</param>
    /// <param name="onActionComplete">Callback for when action execution is complete.</param>
    public void PerformAction(Action attack, Tile targetTile, System.Action onActionComplete = null)
    {

        var actionController = GetActionController(attack);
        actionController.Execute(targetTile, onActionComplete);
        _hasAttacked = true;
    }

    /// <summary>
    /// Character takes damage.
    /// </summary>
    /// <param name="damage">The damage amount.</param>
    /// <param name="instantKill">If true, character will be instantly killed regardless of damage amount.</param>
    /// <returns>Whether or not the character was killed.</returns>
    public bool TakeDamage(float damage, bool instantKill = false)
    {
        if (_healthController == null)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Cannot take damage because character is missing a health controller.");
            return false;
        }

        OnDamageTaken?.Invoke(this);

        if (instantKill || _healthController.TakeDamage(damage))
        {
            OnDeath(this);
            SoundManager.GetInstance()?.Play(Constants.DEATH_SOUND);
            return true;
        }

        SoundManager.GetInstance()?.Play(Constants.DAMAGE_TAKEN_SOUND);
        return false;
    }

    /// <summary>
    /// Sets whether or not this character has previously moved.
    /// </summary>
    /// <param name="hasMoved">Whether or not the character has moved.</param>
    public void SetHasMoved(bool hasMoved)
    {
        _hasMoved = hasMoved;
    }

    /// <summary>
    /// Sets whether or not this character has previously attacked.
    /// </summary>
    /// <param name="hasMoved">Whether or not the character has attacked.</param>
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

    /// <summary>
    /// Hides the character's move range on the grid.
    /// </summary>
    public void HideMoveRadius()
    {
        _moveController.HideMoveRadius();
    }

    /// <summary>
    /// Shows the character's move range on the grid.
    /// </summary>
    public void ShowMoveRadius()
    {
        _moveController.ShowMoveRadius();
    }

    /// <summary>
    /// Calculates all the tiles that this character can move to.
    /// </summary>
    /// <returns>Position-to-tile dictionary of all the tiles the character can move to.</returns>
    public Dictionary<(int, int), Tile> CalculateAvailableMoves()
    {
        return _moveController.CalculateAvailableMoves();
    }

    /// <summary>
    /// Gets all the tiles that this character can move to.
    /// </summary>
    /// <returns>Position-to-tile dictionary of all the tiles the character can move to.</returns>
    public Dictionary<(int, int), Tile> GetAvailableMoves()
    {
        return _moveController.GetAvailableMoves();
    }

    /// <summary>
    /// Gets the action controller for a specific action.
    /// </summary>
    /// <param name="action">The action to get the controller for.</param>
    /// <returns>An <see cref="ActionController"/>.</returns>
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

    /// <summary>
    /// Gets the tile that the character is sitting on.
    /// </summary>
    /// <returns>The <see cref="Tile"/> the character is on.</returns>
    public Tile GetGridPosition()
    {
        return TileGridController.Instance.GetGrid().GetValue(transform.position);
    }

    /// <summary>
    /// Gets the action viewer for a specific action.
    /// </summary>
    /// <param name="action">The action to get the viewer for.</param>
    /// <returns>An <see cref="ActionViewer"/>.</returns>
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


    /// <summary>
    /// Equips an item.
    /// </summary>
    /// <param name="inventoryItem">The item to equip.</param>
    public void Equip(InventoryItem inventoryItem)
    {
        var item = inventoryItem.Item;

        if ((item.Type != ItemType.Weapon) && (item.Type != ItemType.Shield))
        {
            Debug.LogError($"Character {Character.Flavor.Name}: cannot equip non-weapon items.");
            return;
        }

        if (InventoryManager.GetCharacterItem(Id, item.Id, inventoryItem.InventoryPosition) == null)
        {
            Debug.LogError($"Character {Character.Flavor.Name}: cannot equip item that does not exist in character's inventory.");
            return;
        }

        if (item.Type == ItemType.Weapon)
        {
            Character.Weapon = (Weapon)item;
            Debug.Log("MY WEAPON: " + Character.Weapon);
            DisplayEquipment(Character.Weapon);
            //SwitchWeaponModel();
            _equippedWeaponPosition = inventoryItem.InventoryPosition;
        }
        else if (item.Type == ItemType.Shield)
        {
            Character.Shield = (Shield)item;
            _healthController?.UpgradMaxShield(Character.Shield.Health);
            _equippedShieldPosition = inventoryItem.InventoryPosition;
        }
    }

    /// <summary>
    /// Displays the equipment of the character
    /// </summary>
    /// <param name="weapon">the weapon to display</param>
    public void DisplayEquipment(Weapon weapon)
    {
        //FindChildGameObjectsWithTag();
        var weaponSpawn = gameObject.GetComponentInChildren<ShowWeaponScript>();
        if (weaponSpawn != null)
        {
            weaponSpawn.SetWeapon(weapon.WeaponModel, Character);
        }
    }

    /// <summary>
    /// Displays the equipment of the character
    /// </summary>
    public void DisplayEquipment()
    {
        //FindChildGameObjectsWithTag();
        var weaponSpawn = gameObject.GetComponentInChildren<ShowWeaponScript>();
        if (weaponSpawn != null)
        {
            weaponSpawn.SetWeapon(Character.Weapon.WeaponModel, Character);
        }
    }



    /// <summary>
    /// Changes the weapon model to match the equipped item.
    /// </summary>
    public void SwitchWeaponModel()
    {
        var weapon = Character.Weapon;
        if (weapon?.Actions == null || weapon.Actions.Count == 0)
        {
            return;
        }

        if (_meleeWeapon == null || _rangedWeapon == null)
        {
            Debug.LogWarning("Unable to change weapon model because one was not found with the right tag.");
            return;
        }

        var rangedWeaponIsEquipped = weapon.Actions.Any(a => a.AnimType == AnimationType.ranged_attack);
        _meleeWeapon.SetActive(!rangedWeaponIsEquipped);
        _rangedWeapon.SetActive(rangedWeaponIsEquipped);
    }

    /// <summary>
    /// Unequips all items that the character may have equipped.
    /// </summary>
    public void UnequipAllItems()
    {
        Character.Weapon = null;
        _equippedWeaponPosition = -1;

        Character.Shield = null;
        _healthController?.RemoveShield();
        _equippedShieldPosition = -1;
    }

    /// <summary>
    /// Uniquips a specific item.
    /// </summary>
    /// <param name="item">The item to unequip.</param>
    public void Unequip(Item item)
    {
        if (item.Type == ItemType.Weapon && item.Id == Character.Weapon?.Id)
        {
            Character.Weapon = null;
            _equippedWeaponPosition = -1;
        }
        else if (item.Type == ItemType.Shield && item.Id == Character.Shield?.Id)
        {
            Character.Shield = null;
            _healthController?.RemoveShield();
            _equippedShieldPosition = -1;
        }
    }

    /// <summary>
    /// Gets characters that are adjacent to this character.
    /// </summary>
    /// <param name="characterSearchType">The type of characters to get.</param>
    /// <returns>List of characters adjacent to this character.</returns>
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

    /// <summary>
    /// Destroys this character.
    /// </summary>
    public void DestroyCharacter()
    {
        DetachCamera();

        Debug.Log($"Character {Character.Flavor.Name} died.");
        var currentGridPosition = TileGridController.Instance.GetGrid().GetValue(transform.position);
        if (currentGridPosition != null)
        {
            currentGridPosition.CharacterControllerId = null;
        }
        else
        {
            Debug.LogError("Unable to remove character ID from grid because current grid cell position is null.");
        }
        PerformDeathAnimation();
        DestroyCharacter(4f);
    }

    /// <summary>
    /// Destroys character after a certain amount of time.
    /// </summary>
    /// <param name="time"></param>
    public void DestroyCharacter(float time)
    {
        var profileCamera = GetComponentInChildren<UICameraController>();
        if (profileCamera != null)
        {
            profileCamera.transform.SetParent(null, false);
        }

        Destroy(gameObject, time);
    }

    /// <summary>
    /// Detaches camera from the character.
    /// </summary>
    public void DetachCamera()
    {
        for (int x = 0; x < this.transform.childCount; x++)
        {
            if (this.transform.GetChild(x).tag == "MasterCamera")
            {
                var cameraScript = transform.GetChild(x).transform.GetComponent<MasterCameraScript>();
                if (IsEnemy)
                {
                    cameraScript.RemoveFocus();
                }
                else
                {
                    cameraScript.ResetCamera();
                }
            }
        }
    }

    /// <summary>
    /// Consumes an item.
    /// </summary>
    /// <param name="inventoryItem">The item to consume.</param>
    public void Consume(InventoryItem inventoryItem)
    {
        var item = inventoryItem.Item;

        if (item.Type != ItemType.Consumable)
        {
            Debug.LogError($"Character {Character.Flavor.Name}: cannot eat non-consumable items.");
            return;
        }

        if (InventoryManager.GetCharacterItem(Id, item.Id, inventoryItem.InventoryPosition) == null)
        {
            Debug.LogError($"Character {Character.Flavor.Name}: cannot eat item that does not exist.");
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
        InventoryManager.RemoveCharacterItem(Id, consumable.Id, inventoryItem.InventoryPosition);
        if (consumable.ParticleEffect != null)
        {
            var particleEffect = Instantiate(consumable.ParticleEffect, transform.transform);
            particleEffect.transform.position = transform.position;
            Destroy(particleEffect, 5f);
        }
    }

    /// <summary>
    /// Heals the character.
    /// </summary>
    /// <param name="healAmount">The amount to character.</param>
    public void Heal(float healAmount)
    {
        if (_healthController != null)
        {
            _healthController.Healing(healAmount);
        }
    }

    /// <summary>
    /// Increases the character's attack.
    /// </summary>
    /// <param name="attackAmount">The attack amount.</param>
    public void AtkBuff(float attackAmount)
    {
        Character.AttackDamage += attackAmount;
    }

    /// <summary>
    /// Increases the character's move range.
    /// </summary>
    /// <param name="attackAmount">The move range amount.</param>
    public void MovBuff(float moveAmount)
    {
        Character.MoveRange += moveAmount;
    }

    /// <summary>
    /// Regenerates the character's shield.
    /// </summary>
    /// <param name="shieldAmount">Amount of shield to regenerate.</param>
    public void RegenShield(float shieldAmount)
    {
        if (_healthController != null)
        {
            _healthController.RegenerateShield(shieldAmount);
        }
    }

    /// <summary>
    /// Gets the character's <see cref="HealthController"/>.
    /// </summary>
    /// <returns>The character's <see cref="HealthController"/>.</returns>
    public HealthController GetHealthController()
    {
        return _healthController;
    }

    /// <summary>
    /// Gets how much damage the character can do.
    /// </summary>
    /// <returns>How much damage the character can do.</returns>
    public float GetAttackDamage()
    {
        return _attackDamage;
    }

    /// <summary>
    /// Gets whether or not an item is equipped by this character.
    /// </summary>
    /// <param name="inventoryItem">The item to check if its equipped.</param>
    /// <returns>Whether or not an item is equipped.</returns>
    public bool ItemIsEquipped(InventoryItem inventoryItem)
    {
        var item = inventoryItem.Item;

        if (inventoryItem.Item.Type == ItemType.Weapon)
        {
            return Character.Weapon?.Id == item.Id && inventoryItem.InventoryPosition == _equippedWeaponPosition;
        }
        else if (inventoryItem.Item.Type == ItemType.Shield)
        {
            return Character.Shield?.Id == item.Id && inventoryItem.InventoryPosition == _equippedShieldPosition;
        }

        return false;
    }


    /// <summary>
    /// Stuns a character by reducing move range to 0.
    /// </summary>
    /// <param name="newMoveRange">Optional parameter to set the movement range to something else.</param>
    public void IsStunned(float newMoveRange = 0)
    {
        _savedMoveRange = Character.MoveRange;
        Character.MoveRange = newMoveRange;
    }

    /// <summary>
    /// Removes stun effect on character by restoring movement range.
    /// </summary>
    public void Destun()
    {
        Character.MoveRange = _savedMoveRange;
    }

    private void PerformDeathAnimation()
    {
        var animationTypeName = System.Enum.GetName(typeof(AnimationType), AnimationType.death);
        Animator myAnimator = gameObject.GetComponentInChildren<Animator>();
        myAnimator.SetTrigger(animationTypeName);
    }

    /// <summary>
    /// Gets the movement controller component.
    /// </summary>
    /// <returns><see cref="MoveController"></returns>
    public MoveController GetMoveController()
    {
        return _moveController;
    }

    /// <summary>
    /// Determines whether or not this character can trade with another character.
    /// </summary>
    /// <param name="targetTile">The tile on which the character wants to trade.</param>
    /// <param name="targetCharacter">The other character this character can trade with.</param>
    /// <returns>Whether or not this character is able to trade.</returns>
    public bool CanTrade(Tile targetTile, out CharacterController targetCharacter)
    {
        targetCharacter = null;
        if (string.IsNullOrWhiteSpace(targetTile.CharacterControllerId))
        {
            return false;
        }

        var currentTile = GetGridPosition();
        var adjacentTiles = TileGridController.Instance.GetGrid().GetAdjacentTiles(currentTile.GridX, currentTile.GridY);

        foreach (var tile in adjacentTiles)
        {
            if (tile.GridX == targetTile.GridX && tile.GridY == targetTile.GridY)
            {
                targetCharacter = TurnSystemManager.Instance.GetCharacterController(targetTile.CharacterControllerId);
                return targetCharacter.IsEnemy == IsEnemy;
            }
        }
        return false;
    }

    /// <summary>
    /// Sets the inventory position of an equipped weapon.
    /// </summary>
    /// <param name="itemPosition">The position in the character's inventory.</param>
    public void SetEquippedWeaponPosition(int itemPosition)
    {
        _equippedWeaponPosition = itemPosition;
    }

    /// <summary>
    /// Sets the inventory position of an equipped shield.
    /// </summary>
    /// <param name="itemPosition">The position in the character's inventory.</param>
    public void SetEquippedShieldPosition(int itemPosition)
    {
        _equippedShieldPosition = itemPosition;
    }

    /// <summary>
    /// Checks whether this character is currently selected by the user
    /// </summary>
    /// <returns>true if the character is selected, otherwise false</returns>
    public bool getIsSelected()
    {
        return _isSelected;
    }
}
