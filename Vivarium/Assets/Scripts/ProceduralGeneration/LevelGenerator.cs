using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Randomly generates levels based on a given <see cref="LevelGenerationProfile"/>.
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    public delegate void LevelGenerationComplete();
    public static event LevelGenerationComplete OnLevelGenerationComplete;

    public GameObject TreasureChestPrefab;
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
    private Dictionary<(int, int), Tile> _possibleBossSpawnTiles = new Dictionary<(int, int), Tile>();
    private bool _isInitialGeneration = true;

    public GameObject mainCamera;

    private void Awake()
    {
        InventoryManager.ClearInventory();
        if (GenerateLevelOnStart)
        {
            GenerateLevel();
        }
    }

    /// <summary>
    /// Destroys the previous level and generates a new one.
    /// </summary>
    public void GenerateLevel()
    {
        mainCamera.GetComponent<MasterCameraScript>().ResetCamera();
        mainCamera.GetComponent<MasterCameraScript>().refreshFocusCharacters();
        this.GetComponent<GenerateObstacles>().clearObjects();
        DestroyExistingLevel();
        CheckIsTutorial();
        SetupLevelContainer();
        SetupPlayerController();
        GenerateGrid();
        GenerateCharacters();
        GenerateGameMaster();
        this.GetComponent<GenerateObstacles>().generateEnvironment(LevelProfile);
        if (_isInitialGeneration)
        {
            this.GetComponent<EnemyThreatRangeViewer>()?.CalculateThreatRange();
            _isInitialGeneration = false;
        }
        OnLevelGenerationComplete?.Invoke();
    }

    /// <summary>
    /// Destroys the GameObject containing the current level and resets the <see cref="PlayerController"/> if necessary.
    /// </summary>
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

        //Clears player characters only after tutorial
        if (TutorialManager.GetIsTutorial())
        {
            var characterControllers = new List<CharacterController>();
            foreach (var characterController in PlayerCharacters)
            {
                if (characterController.gameObject.activeSelf)
                {
                    Destroy(characterController.gameObject, 0.1f);
                }
                characterControllers.Add(characterController);
            }

            foreach (var characterController in characterControllers)
            {
                PlayerCharacters.Remove(characterController);
            }
        }
    }

    private void CheckIsTutorial()
    {
        TutorialManager.SetIsTutorial(LevelProfile.IsTutorial);
    }

    /// <summary>
    /// Sets up a GameObject to contain a level.
    /// </summary>
    public void SetupLevelContainer()
    {
        _levelContainer = new GameObject("Level");
        _levelContainer.tag = Constants.LEVEL_CONTAINER_TAG;
    }

    /// <summary>
    /// Sets up the <see cref="PlayerController"/> if it has not been done already.
    /// </summary>
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

        LevelProfile.GridProfile.TreasureChests = LevelProfile.TreasureChests;
        LevelProfile.GridProfile.ChestGenerationSubdivisions = LevelProfile.ChestGenerationSubdivisions;
        _grid = new GridGenerator().Generate(LevelProfile.GridProfile, LevelProfile.BossCharacter != null);

        _gridController = grid.AddComponent<TileGridController>();
        _gridController.Initialize(_grid);
        _gridController.VisualSettings = LevelProfile.VisualSettings;

        var tileGridView = grid.AddComponent<TileGridView>();
        tileGridView.GridController = _gridController;
        tileGridView.TileInfos = LevelProfile.GridProfile.TileInfos;
        tileGridView.GridSettings = LevelProfile.VisualSettings;

        tileGridView.CreateGridMesh(_gridController.GridOrigin);
        GeneratePossibleSpawnTiles();
    }

    private void GeneratePossibleSpawnTiles()
    {
        _possiblePlayerSpawnTiles = new Dictionary<(int, int), Tile>();
        for (int i = 0; i < _grid.GetGrid().GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetGrid().GetLength(1); j++)
            {
                switch (_grid.GetValue(i, j).SpawnType)
                {
                    case TileSpawnType.Player:
                        _possiblePlayerSpawnTiles[(i, j)] = _grid.GetValue(i, j);
                        break;
                    case TileSpawnType.Enemy:
                        _possibleEnemySpawnTiles[(i, j)] = _grid.GetValue(i, j);
                        break;
                    case TileSpawnType.Boss:
                        _possibleBossSpawnTiles[(i, j)] = _grid.GetValue(i, j);
                        break;
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
        if (PlayerCharacters.Count == 0 || !TutorialManager.GetIsTutorial())
        {
            GeneratePlayerCharacters();
        }

        if (CharacterReward.rewardLevel)
        {
            if (!TutorialManager.GetIsTutorial())
            {
                CharacterReward.selectedCharacter?.SetActive(true);

                foreach (var characterGameObject in CharacterReward.characterGameObjects)
                {
                    if (!characterGameObject.activeSelf)
                    {
                        PlayerCharacters.Remove(characterGameObject.GetComponent<CharacterController>());
                        Destroy(characterGameObject, 0.1f);
                    }
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
            if (characterController.gameObject.activeSelf)
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
        var enemyProfiles = LevelProfile.PossibleEnemyCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfEnemyCharacters).ToList();

        if (LevelProfile.BossCharacter != null)
        {
            enemyProfiles.Add(LevelProfile.BossCharacter);
        }

        foreach (var enemyProfile in enemyProfiles)
        {
            var characterGameObject = GenerateCharacter(enemyProfile, true);
            characterGameObject.transform.parent = aiManagerObj.transform;

            var characterController = characterGameObject.GetComponent<CharacterController>();
            _enemyAIManager.AICharacters.Add(characterController);

            if (enemyProfile == LevelProfile.BossCharacter)
            {
                PlaceBossOnGrid(characterController);
            }
            else
            {
                PlaceCharacterOnGrid(characterController);
            }
        }
    }

    private void GeneratePlayerCharacters()
    {
        var numberOfPlayerCharacters = Random.Range(LevelProfile.MinPlayerCharacters, LevelProfile.MaxPlayerCharacters + 1);
        numberOfPlayerCharacters -= LevelProfile.GuaranteedPlayerCharacters.Count;
        var playerProfiles = LevelProfile.PossiblePlayerCharacters.OrderBy(x => Random.Range(0, 100)).Take(numberOfPlayerCharacters);
        var playerProfileList = playerProfiles.ToList();

        foreach (var playerProfile in LevelProfile.GuaranteedPlayerCharacters)
        {
            playerProfileList.Add(playerProfile);
        }

        foreach (var playerProfile in playerProfileList)
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

        Tile tile;
        int tileXPosition;
        int tileYPosition;
        if (TutorialManager.GetIsTutorial() && characterController.IsEnemy)
        {
            tile = _possibleEnemySpawnTiles.Values.ToList()[0];
        }
        else
        {
            tileXPosition = Random.Range(0, _grid.GetGrid().GetLength(0));
            tileYPosition = Random.Range(0, _grid.GetGrid().GetLength(1));

            tile = _grid.GetValue(tileXPosition, tileYPosition);
        }

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

    private void PlaceBossOnGrid(CharacterController characterController)
    {
        var spawnTile = _possibleBossSpawnTiles.Values.FirstOrDefault();
        if (spawnTile == null)
        {
            Debug.LogError("Unable to set boss spawn location.");
            return;
        }

        characterController.gameObject.transform.position = _grid.GetWorldPositionCentered(spawnTile.GridX, spawnTile.GridY);
        spawnTile.CharacterControllerId = characterController.Id;
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

        gameMaster.AddComponent<CommandController>();

        GenerateTreasureChests(gameMaster);
    }

    private void GenerateTreasureChests(GameObject gameMaster)
    {
        var rewardsChestController = gameMaster.AddComponent<RewardsChestController>();
        rewardsChestController.RewardsChestPrefab = TreasureChestPrefab;
        rewardsChestController.SetGrid(_grid);

        var treasureChestSpawns = new List<Tile>();

        for (int x = 0; x < _grid.GetGrid().GetLength(0); x++)
        {
            for (int y = 0; y < _grid.GetGrid().GetLength(1); y++)
            {
                if (_grid.GetValue(x, y).SpawnType == TileSpawnType.TreasureChest)
                {
                    treasureChestSpawns.Add(_grid.GetValue(x, y));
                }
            }
        }

        for (var i = 0; i < treasureChestSpawns.Count && i < LevelProfile.TreasureChests.Count; i++)
        {
            rewardsChestController.AddChest(treasureChestSpawns[i], LevelProfile.TreasureChests[i]);
        }
    }

    /// <summary>
    /// Gets the current enemy AI manager
    /// </summary>
    /// <returns><see cref="EnemyAIManager"/></returns>
    public EnemyAIManager GetEnemyAIManager()
    {
        return _enemyAIManager;
    }

    public Dictionary<(int, int), Tile> GetPossibleEnemySpawnTiles()
    {
        return _possibleEnemySpawnTiles;
    }
}
