using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Contains settings for debugging AI.
/// </summary>
[Serializable]
public class AISettings
{
    /// <summary>
    /// Show/hide point values on titles.
    /// </summary>
    public bool ShowPreview;

    /// <summary>
    /// The prefab used to show the tile point value.
    /// </summary>
    public TextLabel PreviewLabelPrefab;
}
