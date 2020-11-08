using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Grid Profile", menuName = "Grid Profile", order = 1)]
public class GridGenerationProfile : ScriptableObject
{
    public List<TileDisplayInfo> TileInfos;

    public int MinGridWidth;
    public int MaxGridWidth;

    public int MinGridHeight;
    public int MaxGridHeight;

    public float GridCellSize;
    public Vector3 GridOrigin;

    public int MinWallLength;
    public int MaxWallLength;

    public int ObjectiveSpawnVariation;

    [Range(0f, 1f)]
    public float ObstacleSpawnChance = 0.05f;
    [Range(0f, 1f)]
    public float WaterSpawnChance = 0.05f;
    [Range(0f, 1f)]
    public float WaterGrouping = 0.05f;
}
