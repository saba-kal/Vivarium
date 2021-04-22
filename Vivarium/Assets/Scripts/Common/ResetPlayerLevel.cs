using UnityEngine;
using System.Collections;

public class ResetPlayerLevel : MonoBehaviour
{
    /// <summary>
    /// Resets the level
    /// </summary>
    /// <param name="levelIndex">An int corresponding to a level</param>
    public void ResetLevel(int levelIndex)
    {
        PlayerData.CurrentLevelIndex = levelIndex;
    }
}
