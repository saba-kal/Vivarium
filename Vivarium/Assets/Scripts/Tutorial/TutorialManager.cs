using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if(IsTutorial && index == maxVisitedIndex)
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
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            scrollIn = true;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
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
        if(enemy.getIsSelected())
        {
            nextButton.interactable = true;
        }
    }

    public void EnemyThreatRange()
    {
        if(index == 4)
        {
            nextButton.interactable = true;
        }
    }

    private void SelectPlayer()
    {
        if(player.getIsSelected())
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

    public void SelectAction()
    {
        if(PlayerController.Instance.GetActionIsSelected())
        {
            nextButton.interactable = true;
        }
    }

    private void DamageEnemy()
    {
        if(enemy.GetHealthController().GetCurrentHealth() < enemy.Character.MaxHealth)
        {
            nextButton.interactable = true;
        }
    }

    public void EndTurn()
    {
        if(IsTutorial)
        {
            if(index == 9)
            {
                nextButton.interactable = true;
            }
        }
    }

    private void ShowObjective()
    {
        if(!objectiveShown)
        {
            GameObject objective = LevelManager.Instance.LevelGenerationProfiles[0].VisualSettings.LevelObjectivePrefab;
            var grid = TileGridController.Instance.GetGrid();
            var position = grid.GetWorldPositionCentered(25, 25);
            var camera = GameObject.FindGameObjectWithTag("MasterCamera");
            camera.GetComponent<MasterCameraScript>().ResetCamera();

            CommandController.Instance.ExecuteCommand(new WaitCommand());
            CommandController.Instance.ExecuteCommand(new MoveCameraCommand(position, 50));
            CommandController.Instance.ExecuteCommand(new UnlockCameraCommand());

            objectiveShown = true;
        }
        nextButton.interactable = true;
    }

    private void LastPrompt()
    {
        if(!cameraReset)
        {
            var camera = GameObject.FindGameObjectWithTag("MasterCamera");
            camera.GetComponent<MasterCameraScript>().ResetCamera();
            nextButton.interactable = true;
            cameraReset = true;
        }

    }

    public void Skip()
    {
        var levelManager = LevelManager.Instance;
        if (levelManager != null)
        {
            levelManager.CompleteLevel();
        }
    }

    public static bool GetIsTutorial()
    {
        return IsTutorial;
    }

    public static void SetIsTutorial(bool isTutorial)
    {
        IsTutorial = isTutorial;
    }

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

    public void UpdateIndex(int num)
    {
        if (num > 0 && index == maxVisitedIndex)
        {
            maxVisitedIndex += num;
        }
        index += num;
        if(index == 6 && player.getIsSelected() && player.IsAbleToMove())
        {
            UpdateTileHighlights();
        }
    }

    public int GetCurrentIndex()
    {
        return index;
    }

    public int GetMaxVisitedIndex()
    {
        return maxVisitedIndex;
    }

    public bool MoveRestrictionsApply()
    {
        return IsTutorial &&
            maxVisitedIndex == 6 &&
            player.getIsSelected() &&
            player.IsAbleToMove();
    }

    public void UpdateTileHighlights()
    {
        var moveController = player.GetMoveController();
        moveController.HideMoveRadius();
        moveController.ShowMoveRadius();
    }
}
