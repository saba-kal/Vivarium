using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public delegate void ObjectiveCapture();
    public static event ObjectiveCapture OnObjectiveCapture;
    public delegate void AllCharactersDead();
    public static event AllCharactersDead OnAllCharactersDead;
    public delegate void PlayerAttack();
    public static event PlayerAttack OnPlayerAttack;
    public delegate void CharacterSelect(CharacterController character);
    public static event CharacterSelect OnCharacterSelect;

    public bool DisableActionsOnEquip = false;
    public bool DisableActionsOnConsume = true;
    public bool DisableActionsOnTrade = true;
    public List<CharacterController> PlayerCharacters;

    private CharacterController _selectedCharacter;
    private bool _actionIsSelected = false;
    private bool _tradeIsSelected = false;
    private Action _selectedAction;
    private UndoMoveController undomoveController;

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        TileGridController.OnGridCellClick += OnGridCellClick;
        UnitInspectionController.OnActionClick += SelectAction;
        UnitInspectionController.OnMoveClick += SelectMove;
        UnitInspectionController.OnTradeClick += SelectTrade;
        InventoryUIController.OnEquipClick += OnEquip;
        InventoryUIController.OnConsumeClick += OnConsume;
        TradeUIController.OnTradeComplete += OnTrade;
        CharacterController.OnDeath += OnCharacterDeath;
    }

    void OnDisable()
    {
        TileGridController.OnGridCellClick -= OnGridCellClick;
        UnitInspectionController.OnActionClick -= SelectAction;
        UnitInspectionController.OnMoveClick -= SelectMove;
        UnitInspectionController.OnTradeClick -= SelectTrade;
        InventoryUIController.OnEquipClick -= OnEquip;
        InventoryUIController.OnConsumeClick -= OnConsume;
        TradeUIController.OnTradeComplete -= OnTrade;
        CharacterController.OnDeath -= OnCharacterDeath;
    }

    private void OnGridCellClick(Tile selectedTile)
    {

        if (selectedTile == null || CommandController.Instance.CommandsAreExecuting())
        {
            return;
        }

        //CharacterController targetCharacter = TurnSystemManager.Instance.GetCharacterWithIds(selectedTile.CharacterControllerId, CharacterSearchType.Enemy);
        //Action is selected. So this grid cell click is for executing the action.
        if (_actionIsSelected &&
            ActionIsWithinRange(selectedTile) &&
            _selectedCharacter != null &&
            !_selectedCharacter.IsEnemy &&
            !_selectedCharacter.IsDisabled &&
            AoeEnemyDetection(selectedTile))
        {
            PerformAction(selectedTile);
            UnitInspectionController.Instance.DisableWeaponActionsForCharacter(_selectedCharacter.Id);
        }
        //A character is selected. The tile that was clicked is within the character's move range.
        else if (_selectedCharacter != null &&
            _selectedCharacter.IsAbleToMoveToTile(selectedTile) &&
            !_selectedCharacter.IsEnemy &&
            !_selectedCharacter.IsDisabled)
        {
            _selectedCharacter.MoveToTile(selectedTile, () =>
            {
                OnCharacterMoveComplete(selectedTile);
            });
            UnitInspectionController.Instance.DisableMoveForCharacter(_selectedCharacter.Id);
        }
        else if (_tradeIsSelected && _selectedCharacter != null && _selectedCharacter.CanTrade(selectedTile, out var targetCharacter))
        {
            TradeUIController.Instance.Display(_selectedCharacter, targetCharacter);
        }
        //Grid cell click was probably on a character.
        else
        {
            //Do not want to deselect character in this special case
            var invalidWaterTile = false;
            if (_selectedCharacter != null)
            {
                var moveController = _selectedCharacter.GetMoveController();
                var surroundingWaterTiles = moveController.GetWaterTilesInRadius();
                invalidWaterTile = selectedTile.Type == TileType.Water && surroundingWaterTiles.ContainsValue(selectedTile);
            }

            if (!(invalidWaterTile))
            {
                GetSelectedCharacter(selectedTile);
            }
        }
    }

    private bool AoeEnemyDetection(Tile selectedTile)
    {
        if (!_actionIsSelected)
        {
            return false;
        }

        var gridController = TileGridController.Instance;
        var aoe = StatCalculator.CalculateStat(_selectedAction, StatType.AttackAOE);
        var affectedTiles = gridController.GetTilesInRadius(selectedTile.GridX, selectedTile.GridY, 0, aoe);

        foreach (var tile in affectedTiles.Values)
        {
            if (!string.IsNullOrEmpty(tile.CharacterControllerId))
            {
                var characterController = TurnSystemManager.Instance.GetCharacterController(tile.CharacterControllerId);
                if (characterController.IsEnemy)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void GetSelectedCharacter(Tile tile)
    {
        DeselectAction();
        DeselectTrade();

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
                SelectCharacter(character);
                OnCharacterSelect?.Invoke(character);
                return;
            }
        }
    }

    private void SelectMove()
    {
        DeselectAction();
        DeselectTrade();
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

        var attackController = _selectedCharacter.GetActionController(action);
        if (attackController == null)
        {
            Debug.LogError("Unable to display attack because an attack controller could not be found.\n" +
                "Make sure your character has a weapon with attacks, and an attack controller that references it.");
            return;
        }

        DeselectAction();
        DeselectMove();
        DeselectTrade();

        _actionIsSelected = true;
        _selectedAction = action;
        var actionAOE = StatCalculator.CalculateStat(action, StatType.AttackAOE);
        attackController.CalculateAffectedTiles();
        attackViewer.DisplayAction(actionAOE, attackController.GetAffectedTiles());
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

    private void SelectTrade()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogError("Unable to select trade because selected character is null.");
            return;
        }

        DeselectAction();
        DeselectMove();
        DeselectTrade();

        var gridController = TileGridController.Instance;
        var grid = gridController.GetGrid();

        var currentTile = grid.GetValue(_selectedCharacter.transform.position);
        var adjescentTiles = grid.GetAdjacentTiles(currentTile.GridX, currentTile.GridY);

        gridController.HighlightTiles(adjescentTiles, GridHighlightRank.Quinary);

        _tradeIsSelected = true;
    }

    private void DeselectTrade()
    {
        TileGridController.Instance.RemoveHighlights(GridHighlightRank.Quinary);
        _tradeIsSelected = false;
    }

    private void PerformAction(Tile targetTile)
    {
        _selectedCharacter.PerformAction(_selectedAction, targetTile, () =>
        {
            OnPlayerAttack?.Invoke();
        });
        DeselectAction();
        DisableActions(_selectedCharacter.Id, true);
    }

    public void EnableCharacters()
    {
        if (_selectedCharacter)
        {
            DeselectMove();
            UIController.Instance.CharacterInfoPanel.SetActive(false);
        }
        UIController.Instance.EnableAllButtons();
        foreach (var character in PlayerCharacters.ToList())
        {
            character.IsDisabled = false;
            character.SetHasAttacked(false);
            character.SetHasMoved(false);
        }
    }

    public void DisableCharacters()
    {
        foreach (var character in PlayerCharacters.ToList())
        {
            character.IsDisabled = true;
        }
    }

    private bool ActionIsWithinRange(Tile targetTile)
    {
        if (_selectedCharacter == null || _selectedAction == null)
        {
            return false;
        }

        var actionViewer = _selectedCharacter.GetActionViewer(_selectedAction);
        if (actionViewer == null)
        {
            return false;
        }

        return actionViewer.ActionIsWithinRange(targetTile);
    }

    private void OnCharacterMoveComplete(Tile toTile)
    {
        if (toTile.IsObjective)
        {
            OnObjectiveCapture?.Invoke();
        }

        RewardsChestController.Instance.OpenChest(toTile, _selectedCharacter);
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

        int activeCharacters = NumberOfActiveCharacters();

        if (activeCharacters == 0)
        {
            OnAllCharactersDead?.Invoke();
        }
    }

    private int NumberOfActiveCharacters()
    {
        int activeCharacters = 0;
        for (var i = 0; i < PlayerCharacters.Count; i++)
        {
            if (PlayerCharacters[i].gameObject.activeSelf)
            {
                activeCharacters++;
            }
        }
        return activeCharacters;
    }

    public void HealCharacters(float healAmount)
    {
        foreach (var characterController in PlayerCharacters.ToList())
        {
            characterController.Heal(healAmount);
        }
    }

    public void RegenCharacterShields(float regenAmount)
    {
        foreach (var characterController in PlayerCharacters.ToList())
        {
            characterController.RegenShield(regenAmount);
        }
    }

    public void SelectCharacter(CharacterController characterController)
    {
        _selectedCharacter = characterController;
        _selectedCharacter.Select();
        if (_selectedCharacter.IsEnemy)
        {
            _selectedCharacter.ShowMoveRadius();
        }
    }

    public void DeselectCharacter()
    {
        if (_selectedCharacter != null)
        {
            DeselectMove();
            DeselectAction();
            _selectedCharacter.Deselect();
            _selectedCharacter = null;
        }
    }

    public bool CharacterMoveIsSelected(out CharacterController characterController)
    {
        if (_selectedCharacter != null && !_actionIsSelected)
        {
            characterController = _selectedCharacter;
            return true;
        }

        characterController = null;
        return false;
    }

    /// <summary>
    /// Checks if an action has been selected by the player
    /// </summary>
    /// <returns>true if an action is selected, otherwise false</returns>
    public bool GetActionIsSelected()
    {
        return _actionIsSelected;
    }

    #region UI events

    private void OnTrade(CharacterController characterController)
    {
        DeselectTrade();
        DisableActions(characterController.Id, DisableActionsOnTrade);
        if (DisableActionsOnTrade)
        {
            characterController.SetHasAttacked(true);
        }
    }

    private void OnConsume(CharacterController characterController)
    {
        DisableActions(characterController.Id, DisableActionsOnConsume);
        characterController.SetHasAttacked(true);
        if (DisableActionsOnConsume)
        {
            characterController.SetHasAttacked(true);
        }

        DeselectAction();
        characterController.Select();
    }

    private void OnEquip(CharacterController characterController)
    {
        DeselectAction();
        DisableActions(characterController.Id, DisableActionsOnEquip);
        if (DisableActionsOnEquip)
        {
            characterController.SetHasAttacked(true);
        }
    }

    private void DisableActions(string characterId, bool disableActions)
    {
        if (!disableActions)
        {
            return;
        }

        if (DisableActionsOnTrade || DisableActionsOnConsume || DisableActionsOnEquip)
        {
            UnitInspectionController.Instance.DisableWeaponActionsForCharacter(characterId);
        }

        if (DisableActionsOnTrade)
        {
            UnitInspectionController.Instance.DisableTradeActionForCharacter(characterId);
        }

        if (DisableActionsOnConsume)
        {
            InventoryUIController.Instance.DisableConsumeForCharacter(characterId);
        }

        if (DisableActionsOnEquip)
        {
            InventoryUIController.Instance.DisableEquipForCharacter(characterId);
        }
    }

    #endregion
}
