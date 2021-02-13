using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelGenerationProfile LevelProfile;
    public List<CharacterController> PlayerCharacters;
    public PlayerController PlayerController;
    public bool GenerateLevelOnStart = false;

    private const int MAX_SPAWN_ITERATIONS = 1000;

    private GameObject _levelContainer;
    private EnemyAIManager _enemyAIManager;
    private Grid<Tile> _grid;
    private TileGridController _gridController;

    private Dictionary<(int, int), Tile> _possiblePlayerSpawnTiles = new Dictionary<(int, int), Tile>();
    private Dictionary<(int, int), Tile> _possibleEnemySpawnTiles = new Dictionary<(int, int), Tile>();

    public GameObject mainCamera;

    private void Awake()
    {
        if (GenerateLevelOnStart)
        {
            GenerateLevel();
        }
    }

    public void GenerateLevel()
    {
        mainCamera.GetComponent<MasterCameraScript>().ResetCamera();
        this.GetComponent<GenerateObstacles>().clearObjects();
        DestroyExistingLevel();
        SetupLevelContainer();
        SetupPlayerController();
        GenerateGrid();
        GenerateCharacters();
        GenerateGameMaster();
        this.GetComponent<GenerateObstacles>().generateEnvironment();
    }

    public void DestroyExistingLevel()
    {
        var levelContainer = GameObject.FindGameObjectWithTag(Constants.LEVEL_CONTAINER_TAG);
        if (levelContainer != null)
        {
            DestroyImmediate(levelContainer);
        }

        var playerController = GameObject.FindGameObjectWithTag(Constants.PLAYER_CONTROLLER_TAG);
        if (PlayerCharacters.Count == 0 && playerController != null)
        {
            if (playerController != null)
            {
                DestroyImmediate(playerController);
            }
        }
        else if (playerController != null)
        {
            PlayerController = playerController.GetComponent<PlayerController>();
        }
        _possibleEnemySpawnTiles.Clear();
    }

    public void SetupLevelContainer()
    {
        _levelContainer = new GameObject("Level");
        _levelContainer.tag = Constants.LEVEL_CONTAINER_TAG;
    }

    public void SetupPlayerController()
    {
        if (PlayerController == null)
        {
            var playerControllerObj = new GameObject("PlayerController");
            playerControllerObj.tag = Constants.PLAYER_CONTROLLER_TAG;

            PlayerController = playerControllerObj.AddComponent<PlayerController>();
            PlayerController.PlayerCharacters = new List<CharacterController>();
        }
    }

    private void GenerateGrid()
    {
        var grid = new GameObject("Grid");
        grid.transform.parent = _levelContainer.transform;

        _grid = new GridGenerator().Generate(LevelProfile.GridProfile);

        _gridController = grid.AddComponent<TileGridController>();
        _gridController.Initialize(_grid);
        _gridController.PrimaryHighlightPrefab = LevelProfile.PrimaryHighlightPrefab;
        _gridController.SecondaryHighlightPrefab = LevelProfile.SecondaryHighlightPrefab;
        _gridController.TertiaryHighlightPrefab = LevelProfile.TertiaryHighlightPrefab; ;

        var tileGridView = grid.AddComponent<TileGridView>();
        tileGridView.GridController = _gridController;
        tileGridView.TileInfos = LevelProfile.GridProfile.TileInfos;
        tileGridView.LevelObjectivePrefab = LevelProfile.LevelObjectivePrefab;
        tileGridView.CreateGridMesh();
        GeneratePossibleSpawnTiles();
    }

    private void GeneratePossibleSpawnTiles()
    {
        _possiblePlayerSpawnTiles = new Dictionary<(int, int), Tile>();
        for (int i = 0; i < _grid.GetGrid().GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetGrid().GetLength(1); j++)
            {
                if (_grid.GetValue(i, j).SpawnType == TileSpawnType.Player)
                {
                    _possiblePlayerSpawnTiles.Add((i, j), _grid.GetValue(i, j));
                }
                else if (_grid.GetValue(i, j).SpawnType == TileSpawnType.Enemy)
                {
                    _possibleEnemySpawnTiles.Add((i, j), _grid.GetValue(i, j));
                }
            }
        }
    }

    private void GenerateCharacters()
    {
        var enemyGenerationOutOfRange =
            LevelProfile.MinEnemyCharacters < 0 ||
            LevelProfile.MaxEnemyCharacters > LevelProfile.PossibleEnemyCharacters.Count;
        var playerGenerationOutOfRange =
            LevelProfile.MinPlayerCharacters < 0 ||
            LevelProfile.MaxPlayerCharacters > LevelProfile.PossiblePlayerCharacters.Count;

        if (enemyGenerationOutOfRange || (PlayerCharacters.Count == 0 && playerGenerationOutOfRange))
        {
            var characterType = enemyGenerationOutOfRange ? "enemy" : "player";
            Debug.LogError($"The minimum and maximum number of {characterType} characters it out of range of the possible characters.");
            return;
        }

        GenerateEnemyCharacters();
        if (PlayerCharacters.Count == 0)
        {
            GeneratePlayerCharacters();
        }

        if(CharacterReward.rewardLevel)
        {
            CharacterReward.selectedCharacter.SetActive(true);
           
            foreach(var characterGameObject in CharacterReward.characterGameObjects)
            {
                if (!characterGameObject.activeSelf)
                {
                    PlayerCharacters.Remove(characterGameObject.GetComponent<CharacterController>());
                    Destroy(characterGameObject, 0.1f);
                }
            }
            CharacterReward.rewardLevel = false;
            CharacterReward.characterGameObjects = new List<GameObject>();
            CharacterReward.selectedCharacter = null;
        }

        if (LevelProfile.RewardCharacters.Count != 0)
        {
            GenerateRewardCharacter();
            CharacterReward.rewardLevel = true;
        }

        foreach (var characterController in PlayerCharacters)
        {
            if(characterController.gameObject.activeSelf)
            {
                PlacePlayerOnGrid(characterController);
            }
        }
    }

    private void GenerateEnemyCharacters()
    {
        var aiManagerObj = new GameObject("AIManager");
        aiManagerObj.transform.parent = _levelContainer.transform;

        var gridPointCalculator = aiManagerObj.AddComponent<GridPointCalculator>();
        gridPointCalculator.TextLabelPrefab = LevelProfile.AISettings.PreviewLabelPrefab;
        gridPointCalculator.ShowPreview = LevelProfile.AISettings.ShowPreview;

        _enemyAIManager = aiManagerObj.AddComponent<EnemyAIManager>();
        _enemyAIManager.AICharacters = new List<CharacterController>();
        _enemyAIManager.GridPointCalculator = gridPointCalculator;

        var numberOfEnemyCharacters = Random.Range(LevelProfile.MinEnemyCharacters, LevelProfile.MaxEnemyCharacters + 1);
        var enemyProfiles = LevelProfile.PossibleEnemyCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfEnemyCharacters);

        foreach (var enemyProfile in enemyProfiles)
        {
            var characterGameObject = GenerateCharacter(enemyProfile, true);
            characterGameObject.transform.parent = aiManagerObj.transform;
            _enemyAIManager.AICharacters.Add(characterGameObject.GetComponent<CharacterController>());
            PlaceCharacterOnGrid(characterGameObject.GetComponent<CharacterController>());
        }
    }

    private void GeneratePlayerCharacters()
    {
        var numberOfPlayerCharacters = Random.Range(LevelProfile.MinPlayerCharacters, LevelProfile.MaxPlayerCharacters + 1);
        var playerProfiles = LevelProfile.PossiblePlayerCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfPlayerCharacters);

        foreach (var playerProfile in playerProfiles)
        {
            var characterGameObject = GenerateCharacter(playerProfile, false);
            characterGameObject.transform.parent = PlayerController.transform;
            PlayerController.PlayerCharacters.Add(characterGameObject.GetComponent<CharacterController>());
        }

        PlayerCharacters = PlayerController.PlayerCharacters;
    }

    private void GenerateRewardCharacter()
    {
        var playerProfiles = LevelProfile.RewardCharacters;
        foreach (var playerProfile in playerProfiles)
        {
            var characterGameObject = GenerateCharacter(playerProfile, false);
            characterGameObject.transform.parent = PlayerController.transform;
            CharacterReward.characterGameObjects.Add(characterGameObject);
            characterGameObject.SetActive(false);
            PlayerController.PlayerCharacters.Add(characterGameObject.GetComponent<CharacterController>());
        }
    }

    private GameObject GenerateCharacter(
        CharacterGenerationProfile characterProfile,
        bool isEnemy)
    {
        var characterGameObject = new CharacterGenerator().GenerateCharacter(characterProfile, isEnemy);
        return characterGameObject;
    }

    private void PlacePlayerOnGrid(CharacterController characterController)
    {
        var tile = _possiblePlayerSpawnTiles.ElementAt(Random.Range(0, _possiblePlayerSpawnTiles.Count)).Value;
        var iterations = 0;
        while (!CharacterCanSpawnOnTile(tile, characterController) &&
            iterations < MAX_SPAWN_ITERATIONS)
        {
            tile = _possiblePlayerSpawnTiles.ElementAt(Random.Range(0, _possiblePlayerSpawnTiles.Count)).Value;
            iterations++;
        }
        characterController.gameObject.transform.position = _grid.GetWorldPositionCentered(tile.GridX, tile.GridY);
        tile.CharacterControllerId = characterController.Id;
    }

    private void PlaceCharacterOnGrid(CharacterController characterController)
    {
        if (characterController == null)
        {
            return;
        }

        var tileXPosition = Random.Range(0, _grid.GetGrid().GetLength(0));
        var tileYPosition = Random.Range(0, _grid.GetGrid().GetLength(1));

        var tile = _grid.GetValue(tileXPosition, tileYPosition);
        var iterations = 0;

        while (!CharacterCanSpawnOnTile(tile, characterController) &&
            iterations < MAX_SPAWN_ITERATIONS)
        {
            tileXPosition = Random.Range(0, _grid.GetGrid().GetLength(0));
            tileYPosition = Random.Range(0, _grid.GetGrid().GetLength(1));
            tile = _grid.GetValue(tileXPosition, tileYPosition);
            iterations++;
        }

        characterController.gameObject.transform.position = _grid.GetWorldPositionCentered(tile.GridX, tile.GridY);
        tile.CharacterControllerId = characterController.Id;
    }

    private bool CharacterCanSpawnOnTile(Tile tile, CharacterController characterController)
    {
        if (characterController.IsEnemy)
        {
            if (tile.SpawnType != TileSpawnType.Enemy)
            {
                return false;
            }
        }
        return
            tile != null &&
            string.IsNullOrEmpty(tile.CharacterControllerId) &&
            characterController.Character.NavigableTiles.Contains(tile.Type);
    }

    private void GenerateGameMaster()
    {
        var gameMaster = new GameObject("GameMaster");
        gameMaster.transform.parent = _levelContainer.transform;

        var turnSystemManager = gameMaster.AddComponent<TurnSystemManager>();
        turnSystemManager.PlayerController = PlayerController;
        turnSystemManager.AIManager = _enemyAIManager;

        var InventoryInitializer = gameMaster.AddComponent<InventoryInitializer>();
        InventoryInitializer.StartingItems = LevelProfile.StartingItems;
        InventoryInitializer.Initialize(PlayerData.CurrentLevelIndex == 0);
        InventoryInitializer.InitializeForEnemies();

        gameMaster.AddComponent<CommandController>();
    }
}
