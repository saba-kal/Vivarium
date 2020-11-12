using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    protected CharacterController _aiCharacter;
    protected Grid<Tile> _grid;
    protected List<CharacterController> _playerCharacters = new List<CharacterController>();

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
        _grid = TileGridController.Instance.GetGrid();
        _aiCharacter = GetComponent<CharacterController>();
    }

    public virtual void Execute(List<CharacterController> playerCharacters)
    {
        _playerCharacters = playerCharacters;
        if (AICanAttack(out var attack, out var targetCharacter))
        {
            _aiCharacter.PerformAction(attack, targetCharacter);
        }
        else if (AICanMove(out var targetTile))
        {
            if (targetTile != null)
            {
                _aiCharacter.MoveToTile(targetTile);
            }
        }
    }

    protected virtual bool AICanAttack(out Action bestAttack, out Tile targetTile)
    {
        var maxPotentialDamage = 0f;
        bestAttack = null;
        targetTile = null;
        if (!_aiCharacter.IsAbleToAttack())
        {
            return false;
        }

        foreach (var playerCharacter in _playerCharacters)
        {
            var mostAffectiveAttack = GetMostEffectiveAttack(playerCharacter, out var potentialDamage);
            if (mostAffectiveAttack != null && potentialDamage > maxPotentialDamage)
            {
                maxPotentialDamage = potentialDamage;
                bestAttack = mostAffectiveAttack;
                targetTile = _grid.GetValue(playerCharacter.transform.position);
            }
        }

        return bestAttack != null;
    }

    protected virtual Action GetMostEffectiveAttack(CharacterController playerCharacter, out float maxPotentialDamage)
    {
        Action bestAttack = null;
        maxPotentialDamage = 0f;

        if (_aiCharacter.Character.Weapon?.Actions == null ||
            _aiCharacter.Character.Weapon.Actions.Count == 0)
        {
            Debug.LogWarning($"Character {_aiCharacter.Character.Name} does not have a weapon with attacks.");
            return bestAttack;
        }

        foreach (var attack in _aiCharacter.Character.Weapon.Actions)
        {
            if (Vector3.Distance(_aiCharacter.transform.position, playerCharacter.transform.position) <= attack.Range + attack.AreaOfAffect)
            {
                
                var potentialDamage = StatCalculator.CalculateStat(attack, StatType.Damage);
                if (potentialDamage > maxPotentialDamage)
                {
                    maxPotentialDamage = potentialDamage;
                    bestAttack = attack;
                }
            }
        }

        return bestAttack;
    }

    protected virtual bool AICanMove(out Tile targetTile)
    {
        targetTile = null;
        if (!_aiCharacter.IsAbleToMove())
        {
            return false;
        }

        var minDistance = float.MaxValue;
        var availableMoves = _aiCharacter.CalculateAvailableMoves();

        foreach (var playerCharacter in _playerCharacters)
        {
            var closestTileToPlayer = GetClosestTileToPlayer(
                playerCharacter.transform.position,
                availableMoves,
                out var distance);

            if (closestTileToPlayer != null &&
                string.IsNullOrEmpty(closestTileToPlayer.CharacterControllerId) &&
                distance < minDistance)
            {
                targetTile = closestTileToPlayer;
                minDistance = distance;
            }
        }

        return true;
    }

    protected Tile GetClosestTileToPlayer(
        Vector3 playerPosition,
        Dictionary<(int, int), Tile> availableMoves,
        out float minimumDistance)
    {
        Tile closestTile = null;
        minimumDistance = float.MaxValue;

        foreach (var tileEntry in availableMoves)
        {
            var tileWorldPos = _grid.GetWorldPositionCentered(tileEntry.Value.GridX, tileEntry.Value.GridY);
            var distance = Vector3.Distance(tileWorldPos, playerPosition);
            if (distance < minimumDistance)
            {
                closestTile = tileEntry.Value;
                minimumDistance = distance;
            }
        }

        return closestTile;
    }

    private void OnCharacterDeath(CharacterController deadCharacterController)
    {
        for (var i = 0; i < _playerCharacters.Count; i++)
        {
            if (_playerCharacters[i].Id == deadCharacterController.Id)
            {
                _playerCharacters.RemoveAt(i);
            }
        }
    }
}
