using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// Controls the turn-based system between player and AI.
/// </summary>
public class TurnSystemManager : MonoBehaviour
{
    public static TurnSystemManager Instance { get; private set; }

    public delegate void TurnStart();
    public static event TurnStart OnTurnStart;

    public EnemyAIManager AIManager;
    public PlayerController PlayerController;

    private bool _isPlayersTurn = true;

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
        _isPlayersTurn = false;

        OnTurnStart?.Invoke();
        PlayerController.DeselectCharacter();

        var mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
        mainCamera?.GetComponent<MasterCameraScript>().saveCameraPosition();
        mainCamera?.GetComponent<MasterCameraScript>().lockCamera();

        AIManager.EnableCharacters();
        AIManager.Execute();
    }

    private void AllowPlayerToMove()
    {
        _isPlayersTurn = true;

        foreach (var character in AIManager.AICharacters)
        {
            if (character.Character.MoveRange < 1)
            {
                character.Destun();
            }
        }
        OnTurnStart?.Invoke();
        PlayerController.EnableCharacters();
        PlayerTurnCameraReset();

    }

    private void PlayerTurnCameraReset()
    {
        var mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
        mainCamera.GetComponent<MasterCameraScript>().CameraMoveToReset();
    }

    /// <summary>
    /// Gets a <see cref="CharacterController"/> for a given ID.
    /// </summary>
    /// <param name="id">The string ID of the character.</param>
    /// <returns>The result <see cref="CharacterController"/>. If none were found, null is returned.</returns>
    public CharacterController GetCharacterController(string id)
    {
        return GetCharacterWithIds(new List<string> { id }, CharacterSearchType.Both).FirstOrDefault();
    }

    /// <summary>
    /// Gets a list of <see cref="CharacterController"/> for given list of IDs.
    /// </summary>
    /// <param name="ids">List of character IDs to search.</param>
    /// <param name="characterSearchType">Type of characters to search.</param>
    /// <returns>List of <see cref="CharacterController"/>.</returns>
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

    /// <summary>
    /// Gets whether or not it is currently the player's turn.
    /// </summary>
    /// <returns>Whether or not it is currently the player's turn.</returns>
    public bool IsPlayersTurn()
    {
        return _isPlayersTurn;
    }
}

public enum CharacterSearchType
{
    Player = 0,
    Enemy = 1,
    Both = 2
}
