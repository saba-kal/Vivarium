using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Linq;

/// <summary>
/// Manages all the <see cref="AIController"/> on a given level.
/// </summary>
public class EnemyAIManager : MonoBehaviour
{
    public delegate void FinishExecute();
    public static event FinishExecute OnFinishExecute;

    public List<CharacterController> AICharacters;
    public GridPointCalculator GridPointCalculator;

    public bool skipEnemyPhase = false;

    void OnEnable()
    {
        CharacterController.OnDeath += OnCharacterDeath;
        LevelGenerator.OnLevelGenerationComplete += Initialize;
    }

    void OnDisable()
    {
        CharacterController.OnDeath -= OnCharacterDeath;
        LevelGenerator.OnLevelGenerationComplete -= Initialize;
    }

    /// <summary>
    /// Significantly speeds up enemy turn phase.
    /// </summary>
    public void turnOnSkipEnemyPhase()
    {
        skipEnemyPhase = true;
        for (int i = 0; i < AICharacters.Count; i++)
        {
            AICharacters[i].GetComponent<AIController>().turnOnSkipEnemyPhase();
        }
    }

    /// <summary>
    /// Restores the turn speed for enemy.
    /// </summary>
    public void turnOffSkipEnemyPhase()
    {
        skipEnemyPhase = false;
        for (int i = 0; i < AICharacters.Count; i++)
        {
            AICharacters[i].GetComponent<AIController>().turnOffSkipEnemyPhase();
        }
    }

    // Use this for initialization
    void Start()
    {
        GridPointCalculator.PreviewGridPoints();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(skipEnemyPhase);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            turnOnSkipEnemyPhase();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            turnOffSkipEnemyPhase();
        }
    }

    /// <summary>
    /// Initializes all <see cref="AIController"/>.
    /// </summary>
    public void Initialize()
    {
        foreach (var aiCharacter in AICharacters.ToList())
        {
            if (aiCharacter.Character.Type == CharacterType.BeeHive)
            {
                continue; //Bee hives don't have brains.
            }

            var aiController = aiCharacter.GetComponent<AIController>();
            aiController?.Initialize();
        }
    }

    /// <summary>
    /// Executes each <see cref="AIController"/> one by one.
    /// </summary>
    public void Execute()
    {
        StartCoroutine(ExecuteAIControllers());
    }

    private IEnumerator ExecuteAIControllers()
    {
        foreach (var aiCharacter in AICharacters.ToList())
        {
            if (aiCharacter.Character.Type == CharacterType.BeeHive)
            {
                continue; //Bee hives don't have brains.
            }

            var aiController = aiCharacter.GetComponent<AIController>();
            if (aiController != null)
            {
                aiController.InitializeTurn(TurnSystemManager.Instance.PlayerController.PlayerCharacters);
                GridPointCalculator.CalculateGridPoints(aiCharacter.GetComponent<CharacterController>());
                GridPointCalculator.UpdatePreview();

                yield return new WaitForSeconds(Constants.AI_DELAY_BETWEEN_ACTIONS);

                var actionIsComplete = false;
                System.Action callback = () =>
                {
                    actionIsComplete = true;
                };

                var mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
                mainCamera.GetComponent<MasterCameraScript>().EnterSetZoomCameraPositionCommand(new Vector3(0, 6, -5));
                CommandController.Instance.ExecuteCommand(new WaitCommand(1f));

                aiController.Move(callback);
                while (!actionIsComplete)
                {
                    yield return null;
                }

                actionIsComplete = false;
                aiController.PerformAction(callback);
                while (!actionIsComplete)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(Constants.AI_DELAY_BETWEEN_ACTIONS);
            }
            else
            {
                Debug.LogWarning($"Character {aiCharacter.Character.Flavor.Name} does not have an AI controller.");
            }
        }

        OnFinishExecute?.Invoke();
    }

    /// <summary>
    /// Removes the hasAttacked and hasMoved flags from <see cref="CharacterController"/>.
    /// </summary>
    public void EnableCharacters()
    {
        foreach (var character in AICharacters)
        {
            character.SetHasAttacked(false);
            character.SetHasMoved(false);
        }
    }

    private void OnCharacterDeath(CharacterController deadCharacterController)
    {
        for (var i = 0; i < AICharacters.Count; i++)
        {
            if (AICharacters[i].Id == deadCharacterController.Id)
            {
                AICharacters.RemoveAt(i);
            }
        }

        deadCharacterController.DestroyCharacter();
    }
}
