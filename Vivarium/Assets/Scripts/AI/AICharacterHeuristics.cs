using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(fileName = "New AI Heuristics", menuName = "AI Character Heuristics", order = 10)]
public class AICharacterHeuristics : ScriptableObject
{
    public ObjectiveHeuristics ObjectiveHeuristics;
    public AllyHeuristics AllyHeuristics;
    public OpponentHeuristics OpponentHeuristics;
}

[Serializable]
public class ObjectiveHeuristics
{
    [Range(-100, 100)]
    public int ObjectivePoints;
    public int ObjectiveGuardRadius;
    [Range(0f, 1f)]
    public float DistanceMultiplier;
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