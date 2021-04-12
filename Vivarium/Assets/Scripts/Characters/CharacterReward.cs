using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Holds data for characters as rewards.
/// </summary>
public static class CharacterReward
{
    /// <summary>
    /// True if the current level gives characters as rewards.
    /// </summary>
    public static bool rewardLevel = false;

    /// <summary>
    /// List of character game objects that can be rewarded to the player.
    /// </summary>
    public static List<GameObject> characterGameObjects = new List<GameObject>();

    /// <summary>
    /// The selected reward character.
    /// </summary>
    public static GameObject selectedCharacter;
}
