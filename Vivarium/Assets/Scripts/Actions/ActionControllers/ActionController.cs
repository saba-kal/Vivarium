using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ActionController : MonoBehaviour
{
    public Action ActionReference;
    public ParticleSystem ParticleEffectPrefab;
    public float ParticleAffectLifetime = 5f;

    protected CharacterController _characterController;
    protected Grid<Tile> _grid;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _grid = TileGridController.Instance.GetGrid();
    }

    public virtual void Execute(Tile targetTile)
    {
        var areaOfAffect = StatCalculator.CalculateStat(ActionReference, StatType.AttackAOE);
        var affectedTiles = TileGridController.Instance.GetTilesInRadius(targetTile.GridX, targetTile.GridY, areaOfAffect);
        this.ExecuteAction(affectedTiles);
    }

    protected virtual void ExecuteAction(Dictionary<(int, int), Tile> affectedTiles)
    {
        var targetCharacterIds = new List<int>();

        foreach (var tile in affectedTiles)
        {
            if (tile.Value.CharacterControllerId.HasValue)
            {
                targetCharacterIds.Add(tile.Value.CharacterControllerId.Value);
            }
        }

        var targetCharacters = TurnSystemManager.Instance.GetCharacterWithIds(targetCharacterIds, GetTargetType());
        CommandController.Instance.ExecuteCoroutine(ExecuteAction(targetCharacters, affectedTiles));
    }

    protected virtual CharacterSearchType GetTargetType()
    {
        if (ActionReference.ActionTargetType == ActionTarget.Both)
        {
            return CharacterSearchType.Both;
        }
        if (!_characterController.IsEnemy && ActionReference.ActionTargetType == ActionTarget.Opponent ||
            _characterController.IsEnemy && ActionReference.ActionTargetType == ActionTarget.Self)
        {
            return CharacterSearchType.Enemy;
        }
        if (_characterController.IsEnemy && ActionReference.ActionTargetType == ActionTarget.Opponent ||
            !_characterController.IsEnemy && ActionReference.ActionTargetType == ActionTarget.Self)
        {
            return CharacterSearchType.Player;
        }

        return CharacterSearchType.Both;
    }

    protected virtual IEnumerator ExecuteAction(List<CharacterController> targetCharacters, Dictionary<(int, int), Tile> affectedTiles)
    {
        foreach (var tile in affectedTiles)
        {
            InstantiateParticleAffect(tile.Value);
        }

        foreach (var targetCharacter in targetCharacters)
        {
            ExecuteActionOnCharacter(targetCharacter);
        }

        yield return null;
    }

    protected virtual GameObject InstantiateParticleAffect(Tile tile)
    {
        if (ParticleEffectPrefab == null)
        {
            return null;
        }

        var particleAffect = Instantiate(ParticleEffectPrefab);
        particleAffect.gameObject.name = $"ParticleAffect_{tile.GridX}_{tile.GridY}";
        particleAffect.transform.position = TileGridController.Instance.GetGrid().GetWorldPositionCentered(tile.GridX, tile.GridY);
        particleAffect.Play();
        Destroy(particleAffect.gameObject, ParticleAffectLifetime);

        return particleAffect.gameObject;
    }

    protected virtual void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        var damage = StatCalculator.CalculateStat(ActionReference, StatType.Damage);
        targetCharacter.TakeDamage(damage);
        Debug.Log($"{targetCharacter.Character.Name} took {damage} damage from {_characterController.Character.Name}.");
    }
}
