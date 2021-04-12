using System;

/// <summary>
/// Type of entity that a tile can spawn.
/// </summary>
[Serializable]
public enum TileSpawnType
{
    Neutral = 0,
    Enemy = 1,
    Player = 2,
    Objective = 3,
    TreasureChest = 4,
    Boss = 5
}
