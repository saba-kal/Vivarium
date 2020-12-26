﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AIController : MonoBehaviour
{
    private const int MAX_MOVE_CALC_ITERATIONS = 100;

    protected CharacterController _aiCharacter;
    protected Grid<Tile> _grid;
    protected List<CharacterController> _playerCharacters = new List<CharacterController>();
    protected GameObject _mainCamera;

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
        _mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
    }

    public virtual void Execute(List<CharacterController> playerCharacters)
    {
        _playerCharacters = playerCharacters;
        if (AICanMove(out var path))
        {
            // lock on here
            if (path != null)
            {
                EnterCameraFocusCommand();
                _aiCharacter.MoveAlongPath(path);
            }
        }
        else if (AICanAttack(out var attack, out var targetCharacter))
        {
            _aiCharacter.PerformAction(attack, targetCharacter);
        }
    }

    private void EnterCameraFocusCommand()
    {
        _mainCamera.GetComponent<CameraFollower>().EnterCameraFocusCommand(this.gameObject);
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
            if (Vector3.Distance(_aiCharacter.transform.position, playerCharacter.transform.position) <= attack.MaxRange + attack.AreaOfAffect)
            {
                if (Vector3.Distance(_aiCharacter.transform.position, playerCharacter.transform.position) >= attack.MinRange - attack.AreaOfAffect)
                {
                    var potentialDamage = StatCalculator.CalculateStat(attack, StatType.Damage);
                    if (potentialDamage > maxPotentialDamage)
                    {
                        maxPotentialDamage = potentialDamage;
                        bestAttack = attack;
                    }
                }
            }
        }

        return bestAttack;
    }

    protected virtual bool AICanMove(out List<Tile> path)
    {
        path = null;
        if (!_aiCharacter.IsAbleToMove())
        {
            return false;
        }

        var aStar = new AStar(_aiCharacter.Character.NavigableTiles);
        var tilesToExclude = new List<Tile>();
        var iterations = 0;
        List<Tile> pathToTarget = null;
        while (pathToTarget == null)
        {
            var targetTile = GetTileWithHighestPoints(tilesToExclude);
            pathToTarget = aStar.Execute(
                _grid.GetValue(_aiCharacter.transform.position),
                targetTile);
            if (pathToTarget == null)
            {
                tilesToExclude.Add(targetTile);
            }
            iterations++;
            if (iterations > MAX_MOVE_CALC_ITERATIONS)
            {
                Debug.LogError("Reached maximum move calculation iterations for AI character.");
                return false;
            }
        }

        var travelRange = StatCalculator.CalculateStat(_aiCharacter.Character, StatType.MoveRadius);
        path = new List<Tile>();
        for (int step = 0; step < travelRange && step < pathToTarget.Count; step++)
        {
            path.Add(pathToTarget[step]);
        }

        return true;
    }

    private Tile GetTileWithHighestPoints(List<Tile> tilesToExclude)
    {
        Tile targetTile = _grid.GetValue(_aiCharacter.transform.position);
        for (var x = 0; x < _grid.GetGrid().GetLength(0); x++)
        {
            for (var y = 0; y < _grid.GetGrid().GetLength(1); y++)
            {
                var tile = _grid.GetValue(x, y);
                if (tile.Points > targetTile.Points &&
                    !tilesToExclude.Contains(tile))
                {
                    targetTile = tile;
                }
            }
        }

        return targetTile;
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
