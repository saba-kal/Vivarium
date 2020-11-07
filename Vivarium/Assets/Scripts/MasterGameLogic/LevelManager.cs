using System.Collections;
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
        PlayerController.OnAllCharactersDead += GameOver;
    }

    void OnDisable()
    {
        PlayerController.OnObjectiveCapture -= CompleteLevel;
        PlayerController.OnAllCharactersDead -= GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        PlayerData.CurrentLevelIndex = 0;
        _levels = new List<Level>();
        //TODO: implement loading here.
    }

    private void CompleteLevel()
    {
        PlayerData.CurrentLevelIndex++;
        if (PlayerData.CurrentLevelIndex >= LevelGenerationProfiles.Count)
        {
            Debug.Log("You beat the game.");
            UIController.Instance.GameOver("YOU WIN");
            PlayerData.CurrentLevelIndex = 0;
        }
        else
        {
            Debug.Log("Level complete. Generating next level...");
            LevelGenerator.LevelProfile = LevelGenerationProfiles[PlayerData.CurrentLevelIndex];
            LevelGenerator.GenerateLevel();
            LevelGenerator.PlayerController.EnableCharacters();
            LevelGenerator.PlayerController.HealCharacters(LevelGenerator.LevelProfile.OnLevelStartHeal);
            LevelGenerator.PlayerController.RegenCharacterShields(LevelGenerator.LevelProfile.OnLevelStartShieldRegen);
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        UIController.Instance.GameOver("GAME OVER");
        PlayerData.CurrentLevelIndex = 0;
    }
}
