using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A profile used to generate a random grid of tiles.
/// </summary>
[CreateAssetMenu(fileName = "New Grid Profile", menuName = "Grid Profile", order = 1)]
public class GridGenerationProfile : ScriptableObject
{
    /// <summary>
    /// <see cref="TileDisplayInfo"/> contains the tiles used when generating the grid, and definitions of those tiles.
    /// </summary>
    public List<TileDisplayInfo> TileInfos;

    /// <summary>
    /// The minimum number of tiles making the width of the grid.
    /// </summary>
    public int MinGridWidth;

    /// <summary>
    /// The maximum number of tiles making the width of the grid.
    /// </summary>
    public int MaxGridWidth;

    /// <summary>
    /// The minimum number of tiles making the height of the grid.
    /// </summary>
    public int MinGridHeight;

    /// <summary>
    /// The maximum number of tiles making the height of the grid.
    /// </summary>
    public int MaxGridHeight;

    /// <summary>
    /// The size of each tile of the grid.
    /// </summary>
    public float GridCellSize;

    /// <summary>
    /// The position of tile 0,0 on the grid.
    /// </summary>
    public Vector3 GridOrigin;

    /// <summary>
    /// The minimum length of walls generated when placing obstacles on the grid.
    /// </summary>
    public int MinWallLength;

    /// <summary>
    /// The maximum length of walls generated when placing obstacles on the grid.
    /// </summary>
    public int MaxWallLength;

    /// <summary>
    /// The number of tiles that the objective can spawn away from the upper right corner of the grid (assuming the origin is in the lower left).
    /// </summary>
    public int ObjectiveSpawnVariation;

    /// <summary>
    /// The chance that an obstacle will spawn on any given tile.
    /// </summary>
    [Range(0f, 1f)]
    public float ObstacleSpawnChance = 0.05f;

    /// <summary>
    /// The base chance that any given tile will be a water tile.
    /// </summary>
    [Range(0f, 1f)]
    public float WaterSpawnChance = 0.05f;

    /// <summary>
    /// The likelihood that water will spawn near other water tiles.
    /// </summary>
    [Range(0f, 1f)]
    public float WaterGrouping = 0.05f;

    /// <summary>
    /// <see cref="LootTable"/> of items that can be found in chests on the grid.
    /// </summary>
    [HideInInspector]
    public List<LootTable> TreasureChests { get; set; }

    /// <summary>
    /// The number of subdivisions the grid is broken into when generating the location of chests.
    /// </summary>
    [HideInInspector]
    public int ChestGenerationSubdivisions { get; set; }
}
