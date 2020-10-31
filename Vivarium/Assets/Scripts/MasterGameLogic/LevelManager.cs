﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public List<LevelGenerationProfile> LevelGenerationProfiles;

    private List<Level> _levels;

    void OnEnable()
    {
        PlayerController.OnObjectiveCapture += CompleteLevel;
    }

    void OnDisable()
    {
        PlayerController.OnObjectiveCapture -= CompleteLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        StartGame();
    }

    private void LoadGame()
    {
        _levels = new List<Level>();
        //TODO: implement loading here.
    }

    private void StartGame()
    {
        if (LevelGenerationProfiles.Count <= PlayerData.CurrentLevelIndex)
        {
            Debug.LogError("");
        }
    }

    private void CompleteLevel()
    {
        PlayerData.CurrentLevelIndex++;
        if (PlayerData.CurrentLevelIndex >= LevelGenerationProfiles.Count)
        {
            Debug.Log("You beat the game.");
        }
        else
        {
            Debug.Log("Level complete. Generating next level...");
            LevelGenerator.LevelProfile = LevelGenerationProfiles[PlayerData.CurrentLevelIndex];
            LevelGenerator.GenerateLevel();
            LevelGenerator.PlayerController.EnableCharacters();
        }
    }
}