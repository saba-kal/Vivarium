using UE = UnityEngine;
using System.Collections;
using S = System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Linq;
using TMPro;

/// <summary>
/// Generates a <see cref="Grid"/> of <see cref="Tile"/> based on a given GridGenerationProfile.
/// </summary>
public class GridGenerator
{

    private const int MAX_PATH_CREATION_ITERATIONS = 100;
    private bool _includeBossSpawn;

    /// <summary>
    /// Generates a <see cref="Grid"/> of <see cref="Tile"/> based on a given GridGenerationProfile.
    /// </summary>
    /// <param name="gridProfile"><see cref="GridGenerationProfile"/> containing the information used to generate the grid.</param>
    /// <param name="includeBossSpawn">A bool representing whether or not a boss will be spawned on the grid.</param>
    /// <returns>A randomly generated <see cref="Grid"/>of <see cref="Tile"/> that will be used for a single level.</returns>
    public Grid<Tile> Generate(GridGenerationProfile gridProfile, bool includeBossSpawn)
    {
        _includeBossSpawn = includeBossSpawn;
        UE.Random.InitState(10);
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

        int objectiveVariation = gridProfile.ObjectiveSpawnVariation;
        if (!_includeBossSpawn)
        {
            SetPossibleObjectiveTiles(resultGrid, objectiveVariation);
        }
        SetPossiblePlayerSpawn(resultGrid);
        SetPossibleEnemySpawn(resultGrid);
        if (_includeBossSpawn)
        {
            SetPossibleBossSpawn(resultGrid);
        }
        PlaceObjective(resultGrid);
        CreatePathsToAllGreenTiles(resultGrid);
        for (int i = 0; i < gridProfile.TreasureChests.Count; i++)
        {
            SetTreasureChestSpawns(resultGrid, gridProfile.ChestGenerationSubdivisions);
        }
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

    private void SetPossibleObjectiveTiles(Grid<Tile> grid, int objectiveVariation)
    {
        for (int i = 0; i <= objectiveVariation; i++)
        {
            for (int j = 0; j <= objectiveVariation; j++)
            {
                grid.GetValue(grid.GetGrid().GetLength(0) - i - 1, grid.GetGrid().GetLength(1) - j - 1).SpawnType = TileSpawnType.Objective;
            }
        }
    }

    private void SetPossiblePlayerSpawn(Grid<Tile> grid)
    {
        if (TutorialManager.GetIsTutorial())
        {
            grid.GetValue(2, 2).SpawnType = TileSpawnType.Player;
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    grid.GetValue(i, j).Type = TileType.Grass;
                }
            }
        }
        else
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    grid.GetValue(i, j).SpawnType = TileSpawnType.Player;
                    grid.GetValue(i, j).Type = TileType.Grass;
                }
            }
        }
    }

    private void SetPossibleEnemySpawn(Grid<Tile> grid)
    {
        if (TutorialManager.GetIsTutorial())
        {
            grid.GetValue(4, 4).SpawnType = TileSpawnType.Enemy;
        }
        else
        {
            var maxX = grid.GetGrid().GetLength(0);
            var maxY = grid.GetGrid().GetLength(1);
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    //.25 is the percentage of one axis on the grid that will not allow enemies to spawn
                    //1/4 * 1/4 = 1/16 of the grid that enemies cannot spawn at the start
                    //This is to keep enemies from spawning too close to player characters
                    if (i >= maxX * .25 || j >= maxY * .25)
                    {
                        if (grid.GetValue(i, j).SpawnType != TileSpawnType.Objective)
                        {
                            grid.GetValue(i, j).SpawnType = TileSpawnType.Enemy;
                        }
                    }
                }
            }
        }
    }

    private void SetPossibleBossSpawn(Grid<Tile> grid)
    {
        var width = grid.GetGrid().GetLength(0);
        var height = grid.GetGrid().GetLength(1);
        var topMiddleTile = grid.GetValue(width / 2, height - 1);
        topMiddleTile.Type = TileType.Grass;
        topMiddleTile.SpawnType = TileSpawnType.Boss;

        var adjacentTiles = grid.GetAdjacentTiles(topMiddleTile.GridX, topMiddleTile.GridY, true);
        foreach (var adjacentTile in adjacentTiles)
        {
            adjacentTile.Type = TileType.Grass;
        }
    }

    private void SetTreasureChestSpawns(Grid<Tile> grid, int subdivisions)
    {
        //Calculate the path to the objective.
        var pathToObjective = GetPathToObjective(grid);
        if (pathToObjective == null)
        {
            return;
        }

        //Count how many tiles from the path pass through each subdivision of the grid.
        var subdivisionPathCount = new int[subdivisions, subdivisions];
        int xDivisor = grid.GetGrid().GetLength(0) / subdivisions;
        int yDivisor = grid.GetGrid().GetLength(1) / subdivisions;
        foreach (var tile in pathToObjective)
        {
            var subdivisionX = Mathf.Clamp(tile.GridX / xDivisor, 0, subdivisions - 1);
            var subdivisionY = Mathf.Clamp(tile.GridY / yDivisor, 0, subdivisions - 1);
            subdivisionPathCount[subdivisionX, subdivisionY]++;
        }

        //Get only subdivisions that have 0 path count.
        //Also store the subdivision with the least path tiles in case all subdivisions contain a path.
        var possibleSpawnAreas = new List<(int, int)>();
        var leastCountSubdivision = (1, 1);
        var leastCount = subdivisionPathCount[1, 1];
        for (int x = 0; x < subdivisionPathCount.GetLength(0); x++)
        {
            for (int y = 0; y < subdivisionPathCount.GetLength(1); y++)
            {
                //Skip starting subdivision
                if (x == 0 && y == 0)
                {
                    continue;
                }

                if (subdivisionPathCount[x, y] == 0)
                {
                    possibleSpawnAreas.Add((x, y));
                }
                if (subdivisionPathCount[x, y] < leastCount)
                {
                    leastCountSubdivision = (x, y);
                    leastCount = subdivisionPathCount[x, y];
                }
            }
        }

        //Get a random x and y in a random subdivision.
        int xSpawn;
        int ySpawn;
        int xBase;
        int yBase;
        if (possibleSpawnAreas.Count == 0)
        {
            xBase = leastCountSubdivision.Item1 * xDivisor;
            yBase = leastCountSubdivision.Item2 * yDivisor;
        }
        else
        {
            var spawnAreaIndex = Random.Range(0, possibleSpawnAreas.Count);
            xBase = possibleSpawnAreas[spawnAreaIndex].Item1 * xDivisor;
            yBase = possibleSpawnAreas[spawnAreaIndex].Item2 * yDivisor;
        }
        xSpawn = Random.Range(xBase, xBase + xDivisor);
        ySpawn = Random.Range(yBase, yBase + yDivisor);

        //Place the spawn point.
        grid.GetValue(xSpawn, ySpawn).SpawnType = TileSpawnType.TreasureChest;
        grid.GetValue(xSpawn, ySpawn).Type = TileType.Obstacle;

        //Make surrounding tiles grass type so that the chest does not black any paths.
        var neighboringTiles = new List<Tile>
        {
            grid.GetValue(xSpawn, ySpawn + 1), //North
            grid.GetValue(xSpawn + 1, ySpawn + 1), //Northeast
            grid.GetValue(xSpawn + 1, ySpawn), //East
            grid.GetValue(xSpawn + 1, ySpawn - 1), //Southeast
            grid.GetValue(xSpawn, ySpawn - 1), //South
            grid.GetValue(xSpawn - 1, ySpawn - 1), //Southwest
            grid.GetValue(xSpawn - 1, ySpawn), //West
            grid.GetValue(xSpawn - 1, ySpawn + 1), //Northwest
        };
        foreach (var neighboringTile in neighboringTiles)
        {
            if (neighboringTile != null)
            {
                neighboringTile.Type = TileType.Grass;
            }
        }
    }

    /// <summary>
    /// Finds a path of grass tiles for the player to get to the objective.
    /// </summary>
    /// <param name="grid"><see cref="Grid"/> of <see cref="Tile"/> for the current level.</param>
    /// <returns>List of <see cref="Tile"/> representing the most direct path of grass tiles from the player spawn to the objective.</returns>
    public List<Tile> GetPathToObjective(Grid<Tile> grid)
    {
        var objectiveTile = grid.GetValue(grid.GetGrid().GetLength(0) - 1, grid.GetGrid().GetLength(1) - 1);

        if (_includeBossSpawn)
        {
            for (int i = 0; i < grid.GetGrid().GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetGrid().GetLength(1); j++)
                {
                    if (grid.GetValue(i, j).SpawnType == TileSpawnType.Boss)
                    {
                        objectiveTile = grid.GetValue(i, j);
                    }
                }
            }
        }

        var aStar = new AStar(new List<TileType> { TileType.Grass }, true, grid);
        var path = aStar.Execute(grid.GetValue(0, 0), objectiveTile);
        return path;
    }

    private void PlaceObjective(Grid<Tile> grid)
    {
        var possibleTiles = new Dictionary<(int, int), Tile>();
        for (int i = 0; i < grid.GetGrid().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGrid().GetLength(1); j++)
            {
                if (grid.GetValue(i, j).SpawnType == TileSpawnType.Objective)
                {
                    possibleTiles.Add((i, j), grid.GetValue(i, j));
                }

            }
        }

        if (possibleTiles.Count == 0)
        {
            return;
        }

        var objectiveTile = possibleTiles.ElementAt(Random.Range(0, possibleTiles.Count)).Value;
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
