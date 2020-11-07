using UE = UnityEngine;
using System.Collections;
using S = System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Linq;

public class GridGenerator
{

    private const int MAX_PATH_CREATION_ITERATIONS = 100;

    public Grid<Tile> Generate(GridGenerationProfile gridProfile)
    {
        var width = UE.Random.Range(gridProfile.MinGridWidth, gridProfile.MaxGridWidth);
        var height = UE.Random.Range(gridProfile.MinGridHeight, gridProfile.MaxGridHeight);
        var cellSize = gridProfile.GridCellSize;
        var origin = gridProfile.GridOrigin;

        var resultGrid = new Grid<Tile>(
            width,
            height,
            cellSize,
            origin,
            (int x, int y, Grid<Tile> grid) => InitializeTile(x, y, grid, gridProfile));

        PlaceObjective(resultGrid);
        CreatePathsToAllGreenTiles(resultGrid);

        return resultGrid;
    }

    private Tile InitializeTile(int x, int y, Grid<Tile> grid, GridGenerationProfile gridProfile)
    {
        var tile = new Tile(x, y, grid);
        var randomValue = UE.Random.Range(0f, 1f);
        var newWaterSpawnChance = gridProfile.WaterSpawnChance;

        int adjacentWater = NextToWater(tile, grid);
        if (adjacentWater != 0)
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
        catch (S.NullReferenceException) { }

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
                            count++;
                        }
                    }
                    catch (S.NullReferenceException) { }
                }
            }
        }
        return count;

    }

    //Generates a wall starting at the coordinates given, going in a random direction
    private void GenerateWall(int x, int y, Grid<Tile> grid, GridGenerationProfile gridProfile)
    {
        int length = UE.Random.Range(gridProfile.MinWallLength, gridProfile.MaxWallLength + 1);
        int dir = UE.Random.Range(0, 4);

        for (int i = 0; i <= length; i++)
        {
            switch (dir)
            {
                case 0:
                    x += 1;
                    break;
                case 1:
                    x -= 1;
                    break;
                case 2:
                    y += 1;
                    break;
                case 3:
                    y -= 1;
                    break;
            }

            var tile = new Tile(x, y, grid);
            tile.Type = TileType.Obstacle;
            grid.SetValue(x, y, tile);
        }
    }

    private void PlaceObjective(Grid<Tile> grid)
    {
        //TODO: figure out better objective placement than a completely random position.
        var xPosition = Random.Range(0, grid.GetGrid().GetLength(0));
        var yPosition = Random.Range(0, grid.GetGrid().GetLength(1));

        var objectiveTile = grid.GetValue(xPosition, yPosition);
        objectiveTile.IsObjective = true;
        objectiveTile.Type = TileType.Grass;
    }

    private void CreatePathsToAllGreenTiles(Grid<Tile> grid, int iteration = 0)
    {
        //WARNING: DO NOT ADD LOGS WITHOUT PERFORMANCE CONSIDERATION
        //This function can get called a lot. If there are a lot of logs, it can freeze the game.

        iteration++;

        var allGreenTiles = GetAllGreenTiles(grid);
        var randomGreenTile = allGreenTiles.Values.ToList()[Random.Range(0, allGreenTiles.Count)];

        //Try to visit every single grass tile using breadth first search.
        var breadthsFirstSearch = new BreadthFirstSearch(grid, true);
        breadthsFirstSearch.Execute(randomGreenTile, int.MaxValue, new List<TileType> { TileType.Grass });
        var visitedTiles = breadthsFirstSearch.GetVisitedTiles();
        visitedTiles[(randomGreenTile.GridX, randomGreenTile.GridY)] = randomGreenTile;

        //The visited tiles equal the total number of green tiles. So, we don't need to do further work.
        if (allGreenTiles.Count == visitedTiles.Count)
        {
            return;
        }

        //Precautionary cap to recursive call count.
        if (iteration > MAX_PATH_CREATION_ITERATIONS)
        {
            UE.Debug.LogWarning("Max number of iterations have been reached while trying to generate level.");
            return;
        }

        //Create a single path between an unvisited and visited tile.
        CreatePathToUnreachableGreenTiles(allGreenTiles, visitedTiles, grid);

        //Recursively call the function to see if all green tiles can be visited. If not, create another path.
        CreatePathsToAllGreenTiles(grid, iteration);
    }

    private Dictionary<(int, int), Tile> GetAllGreenTiles(Grid<Tile> grid)
    {
        var allGreenTiles = new Dictionary<(int, int), Tile>();
        for (var x = 0; x < grid.GetGrid().GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetGrid().GetLength(1); y++)
            {
                if (grid.GetGrid()[x, y].Type == TileType.Grass)
                {
                    allGreenTiles.Add((x, y), grid.GetGrid()[x, y]);
                }
            }
        }
        return allGreenTiles;
    }

    private void CreatePathToUnreachableGreenTiles(
        Dictionary<(int, int), Tile> allGreenTiles,
        Dictionary<(int, int), Tile> visitedTiles,
        Grid<Tile> grid)
    {
        //Get all unvisited tiles (tiles that cannot be reached).
        var unvisitedTiles = new Dictionary<(int, int), Tile>();
        foreach (var tile in allGreenTiles)
        {
            if (!visitedTiles.ContainsKey((tile.Value.GridX, tile.Value.GridY)))
            {
                unvisitedTiles[(tile.Value.GridX, tile.Value.GridY)] = tile.Value;
            }
        }

        //Find the two closest tiles between the two groups of disconnected green tiles.
        Tile closestVisitedTile = null;
        Tile closestUnvisitedTile = null;
        var shortestSqrDistance = float.MaxValue;
        foreach (var visitedTile in visitedTiles.Values)
        {
            foreach (var unvisitedTile in unvisitedTiles.Values)
            {
                var offset = new UE.Vector2(visitedTile.GridX - unvisitedTile.GridX, visitedTile.GridY - unvisitedTile.GridY);

                //Using square magnitude because calculating a square root is expensive.
                if (offset.sqrMagnitude < shortestSqrDistance)
                {
                    closestVisitedTile = visitedTile;
                    closestUnvisitedTile = unvisitedTile;
                    shortestSqrDistance = offset.sqrMagnitude;
                }
            }
        }

        //Walk in a straight line between the two closest tiles and convert all stepped-on tiles to grass type.
        if (closestVisitedTile != null && closestUnvisitedTile != null)
        {
            var currentX = closestVisitedTile.GridX;
            var currentY = closestVisitedTile.GridY;
            var targetX = closestUnvisitedTile.GridX;
            var targetY = closestUnvisitedTile.GridY;

            while (currentX != targetX || currentY != targetY)
            {
                if (Mathf.Abs(currentX - targetX) > Mathf.Abs(currentY - targetY))
                {
                    currentX -= Mathf.Clamp(currentX - targetX, -1, 1);
                }
                else
                {
                    currentY -= Mathf.Clamp(currentY - targetY, -1, 1);
                }
                grid.GetValue(currentX, currentY).Type = TileType.Grass;
            }
        }
    }
}
