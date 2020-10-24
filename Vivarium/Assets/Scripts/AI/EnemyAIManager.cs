using UnityEngine;
using System.Collections.Generic;

public class EnemyAIManager : MonoBehaviour
{
    public delegate void FinishExecute();
    public static event FinishExecute OnFinishExecute;

    public List<CharacterController> AICharacters;

    private bool _isPerformingActions = false;
    private float _timeSinceLastAction = 0f;
    private List<CharacterController> _playerCharacters = new List<CharacterController>();

    void OnEnable()
    {
        CharacterController.OnDeath += OnCharacterDeath;
    }

    void OnDisable()
    {
        CharacterController.OnDeath -= OnCharacterDeath;
    }

    // Use this for initialization
    void Start()
    {
        var playerCharacterObjects = GameObject.FindGameObjectsWithTag(Constants.PLAYER_CHAR_TAG);
        foreach (var playerCharacterObject in playerCharacterObjects)
        {
            var playerCharacter = playerCharacterObject.GetComponent<CharacterController>();
            if (playerCharacter != null)
            {
                _playerCharacters.Add(playerCharacter);
            }
        }
    }

    private void Update()
    {
        //Execute AI a second time after first set of actions are done.
        //So, this allows AI to move+attack.
        if (_isPerformingActions)
        {
            _timeSinceLastAction += Time.deltaTime;

            if (_timeSinceLastAction >= Constants.AI_DELAY_BETWEEN_ACTIONS && !CommandController.Instance.CommandsAreExecuting())
            {
                Execute();
                _isPerformingActions = false;
                OnFinishExecute?.Invoke();
            }
        }
    }

    public void Execute()
    {
        _timeSinceLastAction = 0f;
        _isPerformingActions = true;
        foreach (var aiCharacter in AICharacters)
        {
            var aiController = aiCharacter.GetComponent<AIController>();
            if (aiController != null)
            {
                aiController.Execute(_playerCharacters);
            }
            else
            {
                Debug.LogWarning($"Character {aiCharacter.Character.Name} does not have an AI controller.");
            }
        }
    }

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
                Debug.Log($"Enemy character {deadCharacterController.Character.Name} died.");
                AICharacters.RemoveAt(i);
                Destroy(deadCharacterController.gameObject);
            }
        }
    }
}
