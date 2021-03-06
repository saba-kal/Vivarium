using UnityEngine;
using System.Collections;

public class MinionSummonActionController : ActionController
{
    private CharacterController _summonedCharacter;

    public override void Execute(Tile targetTile, System.Action onActionComplete = null)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        _delay = ActionReference.ActionTriggerDelay;

        CommandController.Instance.ExecuteCommand(
            new MakeCharacterFaceTileCommand(
                _characterController,
                targetTile,
                true));

        PlaySound();
        PerformAnimation();

        SummonCharacter(targetTile);

        onActionComplete?.Invoke();
    }

    private void SummonCharacter(Tile targetTile)
    {
        var newCharacterController = new CharacterGenerator()
            .GenerateCharacter(ActionReference.SummonProfile, true)
            .GetComponent<CharacterController>();

        _summonedCharacter = newCharacterController;

        var grid = TileGridController.Instance;
        newCharacterController.transform.position = grid.GetGrid().GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
        targetTile.CharacterControllerId = newCharacterController.Id;

        if (_characterController.IsEnemy)
        {
            var aiManager = TurnSystemManager.Instance.AIManager;
            newCharacterController.transform.parent = aiManager.transform;
            aiManager.AICharacters.Add(newCharacterController);
        }
        else
        {
            var playerController = TurnSystemManager.Instance.PlayerController;
            newCharacterController.transform.parent = playerController.transform;
            playerController.PlayerCharacters.Add(newCharacterController);
        }
    }

    public CharacterController GetSummonedCharacter()
    {
        return _summonedCharacter;
    }
}
