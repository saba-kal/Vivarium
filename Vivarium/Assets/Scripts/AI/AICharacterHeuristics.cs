using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "New AI Heuristics", menuName = "AI Character Heuristics", order = 10)]
public class AICharacterHeuristics : ScriptableObject
{
    public EnvironmentHeuristics EnvironmentHeuristics;
    public AllyHeuristics AllyHeuristics;
    public OpponentHeuristics OpponentHeuristics;
    public SelfHeuristics SelfHeuristics;
}

[Serializable]
public class EnvironmentHeuristics
{
    [Range(-100, 100)]
    public int ObjectivePoints;
    [Range(-100, 100)]
    public int ObjectiveNearbyTilesPoints;
    [Range(0, 20)]
    public int ObjectivePointsRange;
    [Range(-100, 100)]
    public int ChokePointPoints;
}

[Serializable]
public class AllyHeuristics
{
    [Range(-100, 100)]
    public int AllyProximityPoints;
    [Range(-100, 100)]
    public int AllyAdjacencyPoints;
    [Range(-100, 100)]
    public int AllyAttackCoveragePoints;
}

[Serializable]
public class OpponentHeuristics
{
    [Range(-100, 100)]
    public int OpponentProximityPoints;
    [Range(-100, 100)]
    public int OpponentAdjacencyPoints;
    [Range(-100, 100)]
    public int OpponentAreaOfAttackPoints;
}

[Serializable]
public class SelfHeuristics
{
    [Range(-100, 100)]
    public int TilesCharacterCanAttackPoints;
}