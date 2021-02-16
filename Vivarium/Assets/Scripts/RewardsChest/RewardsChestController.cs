using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RewardsChestController : MonoBehaviour
{
    public static RewardsChestController Instance;

    public GameObject RewardsChestPrefab;

    private Dictionary<(int, int), RewardsChest> _rewardsChests = new Dictionary<(int, int), RewardsChest>();

    private void Awake()
    {
        Instance = this;
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

        var rewardsChestObject = Instantiate(RewardsChestPrefab);
        var rewardsChest = rewardsChestObject.GetComponent<RewardsChest>();
        rewardsChest.Loot = loot;

        _rewardsChests.Add((tile.GridX, tile.GridY), rewardsChest);
    }
}