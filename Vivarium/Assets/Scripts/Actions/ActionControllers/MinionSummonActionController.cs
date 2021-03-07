using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MinionSummonActionController : ArcProjectileActionController
{
    private CharacterController _summonedCharacter;
    private System.Action<CharacterController> _onCharactorSummon;

    protected override void ExecuteAction(Dictionary<(int, int), Tile> affectedTiles)
    {
        GenerateParticlesOnTiles(affectedTiles);
        SummonCharacter(affectedTiles.Values.ToList()[Random.Range(0, affectedTiles.Values.Count)]);
    }

    private void SummonCharacter(Tile targetTile)
    {
        _onCharactorSummon?.Invoke(_summonedCharacter);

        var grid = TileGridController.Instance;

        _summonedCharacter = new CharacterGenerator()
            .GenerateCharacter(ActionReference.SummonProfile, true)
            .GetComponent<CharacterController>();
        _summonedCharacter.transform.position = grid.GetGrid().GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
        targetTile.CharacterControllerId = _summonedCharacter.Id;

        if (_characterController.IsEnemy)
        {
            var aiManager = TurnSystemManager.Instance.AIManager;
            _summonedCharacter.transform.parent = aiManager.transform;
            aiManager.AICharacters.Add(_summonedCharacter);
        }
        else
        {
            var playerController = TurnSystemManager.Instance.PlayerController;
            _summonedCharacter.transform.parent = playerController.transform;
            playerController.PlayerCharacters.Add(_summonedCharacter);
        }
    }

    public void SetOnCharacterSummon(System.Action<CharacterController> onCharactorSummon)
    {
        _onCharactorSummon = onCharactorSummon;
    }

    public CharacterController GetSumonedCharacter()
    {
        return _summonedCharacter;
    }
}
