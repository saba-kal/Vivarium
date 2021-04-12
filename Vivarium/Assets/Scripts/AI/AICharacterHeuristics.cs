using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Scriptable Object for storing AI heuristic data/
/// </summary>
[CreateAssetMenu(fileName = "New AI Heuristics", menuName = "AI Character Heuristics", order = 10)]
public class AICharacterHeuristics : ScriptableObject
{
    /// <summary>
    /// Heuristics related to how AI interacts with the environment.
    /// </summary>
    public EnvironmentHeuristics EnvironmentHeuristics;

    /// <summary>
    /// Heuristics related to how AI interacts with other AI characters.
    /// </summary>
    public AllyHeuristics AllyHeuristics;

    /// <summary>
    /// Heuristics related to how AI interacts with player characters.
    /// </summary>
    public OpponentHeuristics OpponentHeuristics;

    /// <summary>
    /// Heuristics related to how AI values their abilities.
    /// </summary>
    public SelfHeuristics SelfHeuristics;
}

/// <summary>
/// Heuristics related to how AI interacts with the environment.
/// </summary>
[Serializable]
public class EnvironmentHeuristics
{
    /// <summary>
    /// How much the AI values the objective tile.
    /// </summary>
    [Range(-100, 100)]
    public int ObjectivePoints;

    /// <summary>
    /// How much the AI values tiles nearby to the objective.
    /// </summary>
    [Range(-100, 100)]
    public int ObjectiveNearbyTilesPoints;

    /// <summary>
    /// The range of tiles around the objective that the AI values.
    /// </summary>
    [Range(0, 20)]
    public int ObjectivePointsRange;

    /// <summary>
    /// How much the AI values tiles that are considered choke points.
    /// </summary>
    [Range(-100, 100)]
    public int ChokePointPoints;
}

/// <summary>
/// Heuristics related to how AI interacts with other AI characters.
/// </summary>
[Serializable]
public class AllyHeuristics
{
    /// <summary>
    /// How much the AI values tiles that other AI characters can move to.
    /// </summary>
    [Range(-100, 100)]
    public int AllyProximityPoints;

    /// <summary>
    /// How much the AI values tiles that other AI characters are adjacent to.
    /// </summary>
    [Range(-100, 100)]
    public int AllyAdjacencyPoints;

    /// <summary>
    /// How much the AI values tiles that other AI characters can attack.
    /// </summary>
    [Range(-100, 100)]
    public int AllyAttackCoveragePoints;
}

/// <summary>
/// Heuristics related to how AI interacts with player characters.
/// </summary>
[Serializable]
public class OpponentHeuristics
{
    /// <summary>
    /// How much the AI values tiles that player characters can move to.
    /// </summary>
    [Range(-100, 100)]
    public int OpponentProximityPoints;

    /// <summary>
    /// How much the AI values tiles that player characters are adjacent to.
    /// </summary>
    [Range(-100, 100)]
    public int OpponentAdjacencyPoints;

    /// <summary>
    /// How much the AI values tiles that player characters can attack.
    /// </summary>
    [Range(-100, 100)]
    public int OpponentAreaOfAttackPoints;
}

/// <summary>
/// Heuristics related to how AI values their abilities.
/// </summary>
[Serializable]
public class SelfHeuristics
{
    /// <summary>
    /// How much the AI values tiles that they can attack.
    /// </summary>
    [Range(-100, 100)]
    public int TilesCharacterCanAttackPoints;
}