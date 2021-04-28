using UnityEngine;
using System.Collections;

/// <summary>
/// Scriptable object for holding various visual data for grid features.
/// </summary>
[CreateAssetMenu(fileName = "New Grid Visual Settings", menuName = "Visual Settings", order = 11)]
public class GridVisualSettings : ScriptableObject
{
    /// <summary>
    /// Prefab for the primary color grid cell highlight.
    /// </summary>
    public GameObject PrimaryHighlightPrefab;

    /// <summary>
    /// Prefab for the secondary color grid cell highlight.
    /// </summary>
    public GameObject SecondaryHighlightPrefab;

    /// <summary>
    /// Prefab for the tertiary color grid cell highlight.
    /// </summary>
    public GameObject TertiaryHighlightPrefab;

    /// <summary>
    /// Prefab for the quaternary color grid cell highlight.
    /// </summary>
    public GameObject QuaternaryHighlightPrefab;

    /// <summary>
    /// Prefab for the quinary color grid cell highlight.
    /// </summary>
    public GameObject QuinaryHighlightPrefab;

    /// <summary>
    /// Prefab that represents the level objective.
    /// </summary>
    public GameObject LevelObjectivePrefab;

    /// <summary>
    /// The material for the grid mesh.
    /// </summary>
    public Material GridMaterial;

    public GameObject DebugGroundTile;
    public GameObject DebugWaterTile;
    public GameObject DebugWallTile;
}
