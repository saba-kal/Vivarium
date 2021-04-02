using UnityEngine;
using System.Collections;

/// <summary>
/// Scriptable object for holding various visual data for grid features.
/// </summary>
[CreateAssetMenu(fileName = "New Grid Visual Settings", menuName = "Visual Settings", order = 11)]
public class GridVisualSettings : ScriptableObject
{
    public GameObject PrimaryHighlightPrefab;
    public GameObject SecondaryHighlightPrefab;
    public GameObject TertiaryHighlightPrefab;
    public GameObject QuaternaryHighlightPrefab;
    public GameObject QuinaryHighlightPrefab;
    public GameObject LevelObjectivePrefab;
}
