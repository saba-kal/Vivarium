using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TurnSystemManager : MonoBehaviour
{
    public static TurnSystemManager Instance { get; private set; }

    public delegate void TurnStart();
    public static event TurnStart OnTurnStart;

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
        OnTurnStart?.Invoke();
        PlayerController.DeselectCharacter();

        var mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
        mainCamera?.GetComponent<MasterCameraScript>().lockCamera();

        AIManager.EnableCharacters();
        AIManager.Execute();
    }

    private void AllowPlayerToMove()
    {
        OnTurnStart?.Invoke();
        PlayerController.EnableCharacters();
        PlayerTurnCameraReset();
    }

    private void PlayerTurnCameraReset()
    {
        var mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
        mainCamera.GetComponent<MasterCameraScript>().CameraMoveToReset();
    }

    public CharacterController GetCharacterController(string id)
    {
        return GetCharacterWithIds(new List<string> { id }, CharacterSearchType.Both).FirstOrDefault();
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
