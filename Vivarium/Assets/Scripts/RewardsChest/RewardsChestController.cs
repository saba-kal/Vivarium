using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

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

    private void Start()
    {
        _gridController = TileGridController.Instance;
        if (_gridController == null)
        {
            Debug.LogError("Grid controller is null inside rewards chest controller.");
        }
    }

    private void Update()
    {
        if (_gridController == null)
        {
            return;
        }

        var mouseHoverTile = _gridController.GetMouseHoverTile();
        if (mouseHoverTile.SpawnType == TileSpawnType.TreasureChest)
        {
            Debug.Log("Hovering over mouse hover tile");
        }
    }

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

    public void SetGrid(Grid<Tile> grid)
    {
        _grid = grid;
    }
}