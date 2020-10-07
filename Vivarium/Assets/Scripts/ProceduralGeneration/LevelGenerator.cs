using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelGenerationProfile LevelProfile;
    public string LevelData;

    private const int MAX_SPAWN_ITERATIONS = 1000;

    private GameObject _levelContainer;
    private EnemyAIManager _enemyAIManager;
    private PlayerController _playerController;
    private Grid<Tile> _grid;

    private void Start()
    {
        //GenerateLevel();
    }

    public void GenerateLevel()
    {
        DestroyExistingLevel();

        _levelContainer = new GameObject("Level");
        _levelContainer.tag = Constants.LEVEL_CONTAINER_TAG;

        GenerateGrid();
        GenerateCharacters();
        GenerateGameMaster();
    }

    public void DestroyExistingLevel()
    {
        var levelContainer = GameObject.FindGameObjectWithTag(Constants.LEVEL_CONTAINER_TAG);
        if (levelContainer != null)
        {
            DestroyImmediate(levelContainer);
        }
    }

    private void GenerateGrid()
    {
        var grid = new GameObject("Grid");
        grid.transform.parent = _levelContainer.transform;

        var tileGridController = grid.AddComponent<TileGridController>();
        tileGridController.GridWidth = Random.Range(LevelProfile.MinGridWidth, LevelProfile.MaxGridWidth);
        tileGridController.GridHeight = Random.Range(LevelProfile.MinGridHeight, LevelProfile.MaxGridHeight);
        tileGridController.GridCellSize = LevelProfile.GridCellSize;
        tileGridController.GridOrigin = LevelProfile.GridOrigin;
        tileGridController.PrimaryHighlightPrefab = LevelProfile.PrimaryHighlightPrefab;
        tileGridController.SecondaryHighlightPrefab = LevelProfile.SecondaryHighlightPrefab;
        tileGridController.TertiaryHighlightPrefab = LevelProfile.TertiaryHighlightPrefab;
        _grid = tileGridController.GenerateGridData();

        var tileGridView = grid.AddComponent<TileGridView>();
        tileGridView.GridController = tileGridController;
        tileGridView.TileInfos = LevelProfile.TileInfos;
        tileGridView.CreateGridMesh();
    }

    private void GenerateCharacters()
    {
        var enemyGenerationOutOfRange =
            LevelProfile.MinEnemyCharacters < 0 ||
            LevelProfile.MaxEnemyCharacters > LevelProfile.PossibleEnemyCharacters.Count;
        var playerGenerationOutOfRange =
            LevelProfile.MinPlayerCharacters < 0 ||
            LevelProfile.MaxPlayerCharacters > LevelProfile.PossiblePlayerCharacters.Count;

        if (enemyGenerationOutOfRange || playerGenerationOutOfRange)
        {
            var characterType = enemyGenerationOutOfRange ? "enemy" : "player";
            Debug.LogError($"The minimum and maximum number of {characterType} characters it out of range of the possible characters.");
            return;
        }

        GenerateEnemyCharacters();
        GeneratePlayerCharacters();
    }

    private void GenerateEnemyCharacters()
    {
        var aiManagerObj = new GameObject("AIManager");
        aiManagerObj.transform.parent = _levelContainer.transform;

        _enemyAIManager = aiManagerObj.AddComponent<EnemyAIManager>();
        _enemyAIManager.AICharacters = new List<CharacterController>();

        var numberOfEnemyCharacters = Random.Range(LevelProfile.MinEnemyCharacters, LevelProfile.MaxEnemyCharacters + 1);
        var enemyProfiles = LevelProfile.PossibleEnemyCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfEnemyCharacters);

        foreach (var enemyProfile in enemyProfiles)
        {
            var characterGameObject = GenerateCharacter(enemyProfile, true);
            characterGameObject.transform.parent = aiManagerObj.transform;
            _enemyAIManager.AICharacters.Add(characterGameObject.GetComponent<CharacterController>());
        }
    }

    private void GeneratePlayerCharacters()
    {
        var playerControllerObj = new GameObject("PlayerController");
        playerControllerObj.transform.parent = _levelContainer.transform;

        _playerController = playerControllerObj.AddComponent<PlayerController>();
        _playerController.PlayerCharacters = new List<CharacterController>();

        var numberOfPlayerCharacters = Random.Range(LevelProfile.MinPlayerCharacters, LevelProfile.MaxPlayerCharacters + 1);
        var playerProfiles = LevelProfile.PossiblePlayerCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfPlayerCharacters);

        foreach (var playerProfile in playerProfiles)
        {
            var characterGameObject = GenerateCharacter(playerProfile, false);
            characterGameObject.transform.parent = playerControllerObj.transform;
            _playerController.PlayerCharacters.Add(characterGameObject.GetComponent<CharacterController>());
        }
    }

    private GameObject GenerateCharacter(
        CharacterGenerationProfile characterProfile,
        bool isEnemy)
    {
        var characterGameObject = new CharacterGenerator().GenerateCharacter(characterProfile, isEnemy);

        var tileXPosition = Random.Range(0, _grid.GetGrid().GetLength(0));
        var tileYPosition = Random.Range(0, _grid.GetGrid().GetLength(1));

        var tile = _grid.GetValue(tileXPosition, tileYPosition);
        var iterations = 0;

        while (!CharacterCanSpawnOnTile(tile, characterProfile) &&
            iterations < MAX_SPAWN_ITERATIONS)
        {
            tileXPosition = Random.Range(0, _grid.GetGrid().GetLength(0));
            tileYPosition = Random.Range(0, _grid.GetGrid().GetLength(1));
            tile = _grid.GetValue(tileXPosition, tileYPosition);
            iterations++;
        }

        characterGameObject.transform.position = _grid.GetWorldPositionCentered(tile.GridX, tile.GridY);
        tile.CharacterControllerId = characterGameObject.GetComponent<CharacterController>().Id;
        return characterGameObject;
    }

    private bool CharacterCanSpawnOnTile(Tile tile, CharacterGenerationProfile characterProfile)
    {
        return
            tile != null &&
            string.IsNullOrEmpty(tile.CharacterControllerId) &&
            characterProfile.NavigableTiles.Contains(tile.Type);
    }

    private void GenerateGameMaster()
    {
        var gameMaster = new GameObject("GameMaster");
        gameMaster.transform.parent = _levelContainer.transform;

        var turnSystemManager = gameMaster.AddComponent<TurnSystemManager>();
        turnSystemManager.PlayerController = _playerController;
        turnSystemManager.AIManager = _enemyAIManager;

        gameMaster.AddComponent<CommandController>();
    }
}
