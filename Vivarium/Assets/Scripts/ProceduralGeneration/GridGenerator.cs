using UnityEngine;
using System.Collections;

public class GridGenerator
{
    /// <summary>
    /// Generates a grid.
    /// </summary>
    public Grid<Tile> Generate(GridGenerationProfile gridProfile)
    {
        var width = Random.Range(gridProfile.MinGridWidth, gridProfile.MaxGridWidth);
        var height = Random.Range(gridProfile.MinGridHeight, gridProfile.MaxGridHeight);
        var cellSize = gridProfile.GridCellSize;
        var origin = gridProfile.GridOrigin;

        return new Grid<Tile>(
            width,
            height,
            cellSize,
            origin,
            (int x, int y, Grid<Tile> grid) =>
            {
                var tile = new Tile(x, y, grid);
                var randomValue = Random.Range(0f, 1f);
                if (randomValue < gridProfile.ObstacleSpawnChance)
                {
                    tile.Type = TileType.Obstacle;
                }
                else if (randomValue < gridProfile.ObstacleSpawnChance + gridProfile.WaterSpawnChance)
                {
                    tile.Type = TileType.Water;
                }
                else
                {
                    tile.Type = TileType.Grass;
                }
                return tile;
            });
    }
}
