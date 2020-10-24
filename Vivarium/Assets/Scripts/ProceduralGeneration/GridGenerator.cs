using UE = UnityEngine;
using System.Collections;
using S = System;
using System.Diagnostics;

public class GridGenerator
{

    public Grid<Tile> Generate(GridGenerationProfile gridProfile)
    {
        var width = UE.Random.Range(gridProfile.MinGridWidth, gridProfile.MaxGridWidth);
        var height = UE.Random.Range(gridProfile.MinGridHeight, gridProfile.MaxGridHeight);
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
                var randomValue = UE.Random.Range(0f, 1f);
                var newWaterSpawnChance = gridProfile.WaterSpawnChance;

                int adjacentWater = NextToWater(tile, grid);
                if(adjacentWater != 0)
                {
                    newWaterSpawnChance += gridProfile.WaterGrouping * adjacentWater;
                }

                //This is to check if the tile has already been generated previously i.e. part of a wall.
                //Without this check, the type of the tile will be determined again, overwriting the previous type
                try
                {
                    tile.Type = grid.GetValue(x, y).Type;
                    return tile;
                }
                catch (S.NullReferenceException e) { }

                if (randomValue < gridProfile.ObstacleSpawnChance)
                {
                    tile.Type = TileType.Obstacle;
                    GenerateWall(x, y, grid, gridProfile);
                }
                else if (randomValue < gridProfile.ObstacleSpawnChance + newWaterSpawnChance)
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

    //Returns how many tiles adjacent to the one given are water tiles
    private int NextToWater(Tile tile, Grid<Tile> grid)
    {
        int x = tile.GridX;
        int y = tile.GridY;
        int count = 0;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 ^ j == 0)
                {
                    try
                    {
                        if (grid.GetValue(x + i, y + j).Type == TileType.Water)
                        {
                            UE.Debug.Log("Test");
                            count++;
                        }
                    }
                    catch (S.NullReferenceException e) { }
                }
            }
        }
        return count;

    }

    //Generates a wall starting at the coordinates given, going in a random direction
    private void GenerateWall(int x, int y, Grid<Tile> grid, GridGenerationProfile gridProfile)
    {
        int length = UE.Random.Range(gridProfile.MinWallLength, gridProfile.MaxWallLength+1);
        int dir = UE.Random.Range(0, 4);

        var tile = new Tile(x + 1, y, grid);
        tile.Type = TileType.Obstacle;

        for (int i = 0; i <= length; i++)
        {
            switch(dir)
            {
                case 0:
                    grid.SetValue(x+i, y, tile);
                    break;
                case 1:
                    grid.SetValue(x - i, y, tile);
                    break;
                case 2:
                    grid.SetValue(x, y+i, tile);
                    break;
                case 3:
                    grid.SetValue(x, y-i, tile);
                    break;
            }
        }
    }
}
