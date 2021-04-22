using UnityEngine;
using UnityEditor;

/// <summary>
/// Handles logic behind executing weapon attacks.
/// </summary>
public interface IActionController
{
    /// <summary>
    /// Calculates the tiles that the attack can affect.
    /// </summary>
    void CalculateAffectedTiles();
}