using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Controller for chest animations, adding a chest to a level, and distributing reward when a player is nearby
/// </summary>
public class RewardsChestController : MonoBehaviour
{
    public static RewardsChestController Instance;

    public GameObject RewardsChestPrefab;

    private Dictionary<(int, int), RewardsChest> _rewardsChests = new Dictionary<(int, int), RewardsChest>();
    private Grid<Tile> _grid;
    private TileGridController _gridController;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CharacterController.OnSelect += ShowGlow;
        CharacterController.OnDeselect += HideGlow;
    }

    private void OnDisable()
    {
        CharacterController.OnSelect -= ShowGlow;
        CharacterController.OnDeselect -= HideGlow;
    }

    private void Start()
    {
        _gridController = TileGridController.Instance;
        if (_gridController == null)
        {
            Debug.LogError("Grid controller is null inside rewards chest controller.");
        }
    }

    private void ShowGlow(CharacterController characterController)
    {
        if (_gridController == null)
        {
            return;
        }

        HideGlow();

        if (characterController == null || characterController.IsEnemy)
        {
            return;
        }

        if (MoveIsSelected(characterController))
        {
            var navigableTiles = characterController.GetAvailableMoves();
            foreach (var tile in navigableTiles.Values)
            {
                if (GetNearbyRewardsChest(tile, out var rewardsChest) &&
                    characterController.IsAbleToMoveToTile(tile) &&
                    InventoryManager.GetCharacterItemCount(characterController.Id) < characterController.Character.MaxItems)
                {
                    rewardsChest.ShowGlow();
                }
            }
        }
    }

    private bool MoveIsSelected(CharacterController characterController)
    {
        return PlayerController.Instance.CharacterMoveIsSelected(out var selectedCharacter) &&
            characterController.Id == selectedCharacter.Id;
    }

    private void HideGlow(CharacterController unused = null)
    {
        foreach (var chest in _rewardsChests.Values)
        {
            chest.HideGlow();
        }
    }

    private bool GetNearbyRewardsChest(Tile tile, out RewardsChest rewardsChest)
    {
        return _rewardsChests.TryGetValue((tile.GridX, tile.GridY), out rewardsChest) ||
            _rewardsChests.TryGetValue((tile.GridX, tile.GridY + 1), out rewardsChest) ||
            _rewardsChests.TryGetValue((tile.GridX + 1, tile.GridY), out rewardsChest) ||
            _rewardsChests.TryGetValue((tile.GridX, tile.GridY - 1), out rewardsChest) ||
            _rewardsChests.TryGetValue((tile.GridX - 1, tile.GridY), out rewardsChest);
    }

    /// <summary>
    /// Adds a chest to a level and checks to see if one exists already
    /// </summary>
    /// <param name="tile">List of the available tiles a chest can spawn on</param>
    /// <param name="loot">Loot table with a list of available rewards from a chest</param>
    public void AddChest(Tile tile, LootTable loot)
    {
        if (RewardsChestPrefab == null)
        {
            Debug.LogError("Unable to create rewards chest because prefab is null");
            return;
        }

        if (_rewardsChests.ContainsKey((tile.GridX, tile.GridY)))
        {
            Debug.LogError($"Rewards chest already exists on tile {tile.GridX}, {tile.GridY}");
        }

        var rewardsChestObject = Instantiate(RewardsChestPrefab, transform);
        rewardsChestObject.transform.position = _grid.GetWorldPositionCentered(tile.GridX, tile.GridY);

        var rewardsChest = rewardsChestObject.GetComponent<RewardsChest>();
        rewardsChest.Loot = loot;

        _rewardsChests.Add((tile.GridX, tile.GridY), rewardsChest);
    }

    /// <summary>
    /// Initializes the grid
    /// </summary>
    /// <param name="grid">The grid of tiles of a level</param>
    public void SetGrid(Grid<Tile> grid)
    {
        _grid = grid;
    }

    /// <summary>
    /// Disables the chest after it's opened and gives the nearest character an item if they can take it
    /// </summary>
    /// <param name="characterController">Controller that keeps track of character specific details</param>
    public void OpenChest(Tile tile, CharacterController characterController)
    {
        if (tile == null || characterController == null ||
            !GetNearbyRewardsChest(tile, out var rewardsChest) ||
            InventoryManager.GetCharacterItemCount(characterController.Id) >= characterController.Character.MaxItems)
        {
            return;
        }

        var item = rewardsChest.Open();
        RemoveChest(rewardsChest);

        InventoryManager.PlaceCharacterItem(characterController.Id, item);
        UIController.Instance.ShowCharacterInfo(characterController);
        UndoMoveController.Instance.DisableUndo();
    }

    private void RemoveChest(RewardsChest rewardsChest)
    {
        var newRewardsChests = new Dictionary<(int, int), RewardsChest>();
        foreach (var chest in _rewardsChests)
        {
            if (chest.Value != rewardsChest)
            {
                newRewardsChests.Add(chest.Key, chest.Value);
            }
        }
        _rewardsChests = newRewardsChests;
    }

}