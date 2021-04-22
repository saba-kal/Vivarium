using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages everything related to the tutorial.  Forces the player to complete the prompted action before continuing.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }
    public Queue<string> tutorialPrompts;
    public GameObject screen;
    public Button nextButton;
    public Button skipButton;
    public Button backButton;
    private static bool IsTutorial = false;
    private int index;
    private int maxVisitedIndex;

    private bool wPress = false;
    private bool aPress = false;
    private bool sPress = false;
    private bool dPress = false;
    private bool qPress = false;
    private bool ePress = false;
    private bool scrollIn = false;
    private bool scrollOut = false;
    private bool objectiveShown = false;
    private bool cameraReset = false;

    private CharacterController enemy;
    private CharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        tutorialPrompts = new Queue<string>();
        TutorialManager.UpdateScreen();
        nextButton.interactable = false;
        enemy = LevelManager.Instance.LevelGenerator.GetEnemyAIManager().AICharacters[0];
        player = LevelManager.Instance.LevelGenerator.PlayerCharacters[0];
        backButton.interactable = false;
    }

    private void Update()
    {
        if (IsTutorial && index == maxVisitedIndex)
        {
            switch (index)
            {
                case 0:
                    CameraPan();
                    break;
                case 1:
                    CameraRotate();
                    break;
                case 2:
                    CameraZoom();
                    break;
                case 3:
                    SelectEnemy();
                    break;
                case 5:
                    SelectPlayer();
                    break;
                case 6:
                    MoveCharacter();
                    break;
                case 7:
                    SelectAction();
                    break;
                case 8:
                    DamageEnemy();
                    break;
                case 10:
                    ShowObjective();
                    break;
                case 11:
                    LastPrompt();
                    break;
            }
        }
    }

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

    private void CameraPan()
    {
        if (Input.GetKeyDown("w"))
        {
            wPress = true;
        }
        else if (Input.GetKeyDown("a"))
        {
            aPress = true;
        }
        else if (Input.GetKeyDown("s"))
        {
            sPress = true;
        }
        else if (Input.GetKeyDown("d"))
        {
            dPress = true;
        }
        if (wPress && aPress && sPress && dPress && !nextButton.interactable)
        {
            nextButton.interactable = true;
        }
    }

    private void CameraRotate()
    {
        if (Input.GetKeyDown("q"))
        {
            qPress = true;
        }
        else if (Input.GetKeyDown("e"))
        {
            ePress = true;
        }
        if (qPress && ePress && !nextButton.interactable)
        {
            nextButton.interactable = true;
        }
    }

    private void CameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            scrollIn = true;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            scrollOut = true;
        }
        if (scrollIn && scrollOut && !nextButton.interactable)
        {
            nextButton.interactable = true;
        }
    }

    private void SelectEnemy()
    {
        if (enemy.getIsSelected())
        {
            nextButton.interactable = true;
        }
    }

    /// <summary>
    /// Allows the player to continue the tutorial if the enemy threat range box is selected at the appropriate time
    /// </summary>
    public void EnemyThreatRange()
    {
        if (index == 4)
        {
            nextButton.interactable = true;
        }
    }

    private void SelectPlayer()
    {
        if (player.getIsSelected())
        {
            nextButton.interactable = true;
        }
    }

    private void MoveCharacter()
    {
        if (!player.IsAbleToMove())
        {
            nextButton.interactable = true;
        }
    }

    /// <summary>
    /// Allows the player to continue the tutorial if an action is selected at the appropriate time
    /// </summary>
    public void SelectAction()
    {
        if (PlayerController.Instance.GetActionIsSelected())
        {
            nextButton.interactable = true;
        }
    }

    private void DamageEnemy()
    {
        if (enemy.GetHealthController().GetCurrentHealth() < enemy.Character.MaxHealth)
        {
            nextButton.interactable = true;
        }
    }

    /// <summary>
    /// Allows the player to continue the tutorial if their turn is ended at the appropriate time
    /// </summary>
    public void EndTurn()
    {
        if (IsTutorial)
        {
            if (index == 9)
            {
                nextButton.interactable = true;
            }
        }
    }

    private void ShowObjective()
    {
        if (!objectiveShown)
        {
            GameObject objective = LevelManager.Instance.LevelGenerationProfiles[0].VisualSettings.LevelObjectivePrefab;
            var grid = TileGridController.Instance.GetGrid();
            var position = grid.GetWorldPositionCentered(25, 25);
            var camera = GameObject.FindGameObjectWithTag("MasterCamera");
            camera.GetComponent<MasterCameraScript>().ResetCamera();

            CommandController.Instance.ExecuteCommand(new WaitCommand(0.5f));
            CommandController.Instance.ExecuteCommand(new MoveCameraCommand(position, 50));
            CommandController.Instance.ExecuteCommand(new UnlockCameraCommand());

            objectiveShown = true;
        }
        nextButton.interactable = true;
    }

    private void LastPrompt()
    {
        if (!cameraReset)
        {
            var camera = GameObject.FindGameObjectWithTag("MasterCamera");
            camera.GetComponent<MasterCameraScript>().ResetCamera();
            nextButton.interactable = true;
            cameraReset = true;
        }

    }

    /// <summary>
    /// Skips tutorial
    /// </summary>
    public void Skip()
    {
        var levelManager = LevelManager.Instance;
        if (levelManager != null)
        {
            levelManager.CompleteLevel();
        }
    }

    /// <summary>
    /// Checks if the player is currently playing the tutorial
    /// </summary>
    /// <returns>true if they are in the tutorial, otherwise false</returns>
    public static bool GetIsTutorial()
    {
        return IsTutorial;
    }

    /// <summary>
    /// Sets whether or not the current level is the tutorial
    /// </summary>
    /// <param name="isTutorial">true to set it to the tutorial, false to set it to a normal level</param>
    public static void SetIsTutorial(bool isTutorial)
    {
        IsTutorial = isTutorial;
    }

    /// <summary>
    /// Updates the tutorial UI to be active only when appropriate
    /// </summary>
    public static void UpdateScreen()
    {
        var tutorialManager = TutorialManager.Instance;
        var screen = tutorialManager.screen;
        var skipButton = tutorialManager.skipButton;
        if (screen != null)
        {
            if (IsTutorial)
            {
                screen.SetActive(true);
                skipButton.gameObject.SetActive(true);
            }
            else
            {
                screen.SetActive(false);
                skipButton.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Adjusts the index signifying the current tutorial prompt being shown to the player
    /// </summary>
    /// <param name="num">The amount that the index is adjusted by</param>
    public void UpdateIndex(int num)
    {
        if (num > 0 && index == maxVisitedIndex)
        {
            maxVisitedIndex += num;
        }
        index += num;
        if (index == 6 && player.getIsSelected() && player.IsAbleToMove())
        {
            UpdateTileHighlights();
        }
    }

    /// <summary>
    /// Gets the current index signifying the current tutorial prompt being show to the player
    /// </summary>
    /// <returns>The index of the current prompt</returns>
    public int GetCurrentIndex()
    {
        return index;
    }

    /// <summary>
    /// Gets the largest index of all tutorial prompts seen by the player
    /// </summary>
    /// <returns>Largest index of all tutorial prompts seen by the player</returns>
    public int GetMaxVisitedIndex()
    {
        return maxVisitedIndex;
    }

    /// <summary>
    /// Checks if the movement of the player character must be restricted for the sake of the tutorial
    /// </summary>
    /// <returns>true if their movement should be restricted, otherwise false</returns>
    public bool MoveRestrictionsApply()
    {
        return IsTutorial &&
            maxVisitedIndex == 6 &&
            player.getIsSelected() &&
            player.IsAbleToMove();
    }

    /// <summary>
    /// Updates the tile highlights related to player movement
    /// </summary>
    public void UpdateTileHighlights()
    {
        var moveController = player.GetMoveController();
        moveController.HideMoveRadius();
        moveController.ShowMoveRadius();
    }
}
