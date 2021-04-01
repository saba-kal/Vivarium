using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Controller for handling the minion summon action.
/// </summary>
public class MinionSummonActionController : ArcProjectileActionController
{
    private CharacterController _summonedCharacter;
    private System.Action<CharacterController> _onCharactorSummon;
    private const string PLACE_HOLDER_CHARACTER_ID = "XXX";

    /// <inheritdoc cref="ArcProjectileActionController.Execute(Tile, System.Action)"/>
    public override void Execute(Tile targetTile, System.Action onActionComplete = null)
    {
        //Set a placeholder for the character controller ID on the tile so that the AI does not accidentally move to this tile. 
        targetTile.CharacterControllerId = PLACE_HOLDER_CHARACTER_ID;

        base.Execute(targetTile, onActionComplete);
    }

    /// <inheritdoc cref="ArcProjectileActionController.ExecuteAction(Dictionary{(int, int), Tile})"/>
    protected override void ExecuteAction(Dictionary<(int, int), Tile> affectedTiles)
    {
        GenerateParticlesOnTiles(affectedTiles);
        SummonCharacter(affectedTiles.Values.ToList()[Random.Range(0, affectedTiles.Values.Count)]);
    }

    private void SummonCharacter(Tile targetTile)
    {
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

        _onCharactorSummon?.Invoke(_summonedCharacter);
    }

    /// <summary>
    /// Sets a callback for when a character is summoned.
    /// </summary>
    /// <param name="onCharactorSummon">The callback to set.</param>
    public void SetOnCharacterSummon(System.Action<CharacterController> onCharactorSummon)
    {
        _onCharactorSummon = onCharactorSummon;
    }

    /// <summary>
    /// Gets the most recent summoned character.
    /// </summary>
    /// <returns>The summoned <see cref="CharacterController"/>.</returns>
    public CharacterController GetSumonedCharacter()
    {
        return _summonedCharacter;
    }
}
