using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public List<LevelGenerationProfile> LevelGenerationProfiles;

    private List<Level> _levels;

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        StartGame();
    }

    private void LoadGame()
    {
        _levels = new List<Level>();
    }

    private void StartGame()
    {
        if (LevelGenerationProfiles.Count <= PlayerData.CurrentLevelIndex)
        {
            Debug.LogError("");
        }
    }
}
