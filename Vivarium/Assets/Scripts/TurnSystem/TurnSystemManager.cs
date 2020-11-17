using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TurnSystemManager : MonoBehaviour
{
    public static TurnSystemManager Instance { get; private set; }

    public EnemyAIManager AIManager;
    public PlayerController PlayerController;

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
        UIController.OnEndTurnClick += ExecuteAI;
        EnemyAIManager.OnFinishExecute += AllowPlayerToMove;
    }

    void OnDisable()
    {
        UIController.OnEndTurnClick -= ExecuteAI;
        EnemyAIManager.OnFinishExecute -= AllowPlayerToMove;
    }

    private void Start()
    {
        var ids = new List<string>();
        foreach (var character in PlayerController.PlayerCharacters.Concat(AIManager.AICharacters))
        {
            if (ids.Contains(character.Id))
            {
                Debug.LogError($"Character Controller with ID {character.Id} already exists. Please pick a different ID.");
            }
            else
            {
                ids.Add(character.Id);
            }
        }
    }

    private void ExecuteAI()
    {
        CommandController.Instance.ExecuteCommand(
        new LockCameraCommand()
        );
        AIManager.EnableCharacters();
        AIManager.Execute();
    }

    private void AllowPlayerToMove()
    {
        PlayerController.EnableCharacters();
        PlayerTurnCameraReset();
    }

    private void PlayerTurnCameraReset()
    {
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.GetComponent<CameraFollower>().CameraMoveToReset();



        //CommandController.Instance.ExecuteCommand(
        //new WaitCommand()
        //);
        //CommandController.Instance.ExecuteCommand(
        //new MoveCameraCommand(new Vector3(0, 0, 0), 1)
        //);
        //CommandController.Instance.ExecuteCommand(
        //new UnlockCameraCommand()
        //);
    }

    public List<CharacterController> GetCharacterWithIds(List<string> ids, CharacterSearchType characterSearchType)
    {
        var characters = new List<CharacterController>();
        if (characterSearchType == CharacterSearchType.Player || characterSearchType == CharacterSearchType.Both)
        {
            characters = characters.Concat(PlayerController.PlayerCharacters).ToList();
        }

        if (characterSearchType == CharacterSearchType.Enemy || characterSearchType == CharacterSearchType.Both)
        {
            characters = characters.Concat(AIManager.AICharacters).ToList();
        }

        return characters.Where(character => ids.Contains(character.Id)).ToList();
    }
}

public enum CharacterSearchType
{
    Player = 0,
    Enemy = 1,
    Both = 2
}
