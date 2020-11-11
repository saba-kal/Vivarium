using UnityEngine;
using System.Collections;

public class ResetPlayerLevel : MonoBehaviour
{
    public void ResetLevel(int levelIndex)
    {
        PlayerData.CurrentLevelIndex = levelIndex;
    }
}
