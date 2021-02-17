using UnityEngine;
using System.Collections;

public class TreasureChestGenerator
{

    public void GenerateTreasureChests(RewardsChestController rewardsChestController, LevelGenerationProfile levelProfile)
    {
        var grid = TileGridController.Instance?.GetGrid()?.GetGrid();
        if (grid == null)
        {
            Debug.LogError("Unable to generate chests because grid is null.");
            return;
        }

        //var upperLeftQuadrant = grid[grid]
        foreach (var lootTable in levelProfile.TreasureChests)
        {
            rewardsChestController.AddChest(null, lootTable);
        }
    }
}
