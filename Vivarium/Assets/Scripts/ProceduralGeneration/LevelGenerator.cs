using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelGenerationProfile LevelProfile;
    public string LevelData;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        GenerateGrid();
        GenerateCharacters();
        GenerateGameMaster();
    }

    private void GenerateGrid()
    {
        var grid = new GameObject("Grid");

        var tileGridController = grid.AddComponent<TileGridController>();
        tileGridController.GridWidth = Random.Range(LevelProfile.MinGridWidth, LevelProfile.MaxGridWidth);
        tileGridController.GridHeight = Random.Range(LevelProfile.MinGridHeight, LevelProfile.MaxGridHeight);
        tileGridController.GridCellSize = LevelProfile.GridCellSize;
        tileGridController.GridOrigin = LevelProfile.GridOrigin;
        tileGridController.GenerateGridData();


        var tileGridView = grid.AddComponent<TileGridView>();
        tileGridView.GridController = tileGridController;
        tileGridView.TileInfos = LevelProfile.TileInfos;
        tileGridView.CreateGridMesh();
    }

    private void GenerateCharacters()
    {
        var enemyGenerationOutOfRange =
            LevelProfile.MinEnemyCharacters < 0 ||
            LevelProfile.MaxEnemyCharacters >= LevelProfile.PossibleEnemyCharacters.Count;
        var playerGenerationOutOfRange =
            LevelProfile.MinPlayerCharacters < 0 ||
            LevelProfile.MaxPlayerCharacters >= LevelProfile.PossiblePlayerCharacters.Count;

        if (enemyGenerationOutOfRange || playerGenerationOutOfRange)
        {
            var characterType = enemyGenerationOutOfRange ? "enemy" : "player";
            Debug.LogError($"The minimum and maximum number of {characterType} characters it out of range of the possible characters.");
            return;
        }

        var numberOfEnemyCharacters = Random.Range(LevelProfile.MinEnemyCharacters, LevelProfile.MaxEnemyCharacters);
        var enemyProfiles = LevelProfile.PossibleEnemyCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfEnemyCharacters);

        foreach (var enemyProfile in enemyProfiles)
        {
            GenerateEnemyCharacter(enemyProfile);
        }

        var numberOfPlayerCharacters = Random.Range(LevelProfile.MinPlayerCharacters, LevelProfile.MaxPlayerCharacters);
        var playerProfiles = LevelProfile.PossiblePlayerCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfPlayerCharacters);

        foreach (var enemyProfile in enemyProfiles)
        {
            GeneratePlayerCharacter(enemyProfile);
        }
    }

    private void GenerateEnemyCharacter(CharacterGenerationProfile characterProfile)
    {
        //TODO: implement this method.
    }

    private void GeneratePlayerCharacter(CharacterGenerationProfile characterProfile)
    {
        //TODO: implement this method.
    }

    private void GenerateGameMaster()
    {
        var gameMaster = new GameObject("GameMaster");
    }
}
