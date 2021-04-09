using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Handles player progression between levels.
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelGenerator LevelGenerator;
    public List<LevelGenerationProfile> LevelGenerationProfiles;

    private List<Level> _levels;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        PlayerController.OnObjectiveCapture += CompleteLevel;
        PlayerController.OnAllCharactersDead += GameOver;
        CharacterController.OnDeath += OnCharacterDeath;
    }

    void OnDisable()
    {
        PlayerController.OnObjectiveCapture -= CompleteLevel;
        PlayerController.OnAllCharactersDead -= GameOver;
        CharacterController.OnDeath -= OnCharacterDeath;
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

    /// <summary>
    /// Completes the current level and generates the next level.
    /// </summary>
    public void CompleteLevel()
    {
        if (PlayerData.CurrentLevelIndex + 1 >= LevelGenerationProfiles.Count)
        {
            Debug.Log("You beat the game.");
            UIController.Instance.GameOver("YOU WIN");
        }
        else
        {
            PlayerData.CurrentLevelIndex++;
            UIController.Instance.RewardsUIController.ShowRewardsScreen(() =>
            {
                Debug.Log("Level complete. Generating next level...");
                StartLevel(PlayerData.CurrentLevelIndex);
                LevelGenerator.GenerateLevel();
                PrepMenuUIController.Instance.Display();
            }, LevelGenerator.LevelProfile.PossilbleRewards);
        }
    }

    /// <summary>
    /// Generates and starts a specific level.
    /// </summary>
    /// <param name="level">The level number to start.</param>
    public void StartLevel(int level)
    {
        PlayerData.CurrentLevelIndex = level;
        LevelGenerator.LevelProfile = LevelGenerationProfiles[level];
        LevelGenerator.GenerateLevel();
        LevelGenerator.PlayerController.EnableCharacters();
        LevelGenerator.PlayerController.HealCharacters(LevelGenerator.LevelProfile.OnLevelStartHeal);
        LevelGenerator.PlayerController.RegenCharacterShields(LevelGenerator.LevelProfile.OnLevelStartShieldRegen);
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        UIController.Instance.GameOver("GAME OVER");
        PlayerData.CurrentLevelIndex = 0;
    }

    private void OnCharacterDeath(CharacterController characterController)
    {
        //TODO: Create a more generic attribute that tells us whether an enemy has the level key.
        if (characterController.Character.Type == CharacterType.QueenBee)
        {
            CompleteLevel();
        }
    }
}
