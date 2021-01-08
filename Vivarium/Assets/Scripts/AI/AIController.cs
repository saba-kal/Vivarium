using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AIController : MonoBehaviour
{
    private const int MAX_MOVE_CALC_ITERATIONS = 50;
    private const float POINTS_FOR_KILLING_PLAYER_CHARACTER = 1000f;

    protected CharacterController _aiCharacter;
    protected Grid<Tile> _grid;
    protected List<CharacterController> _playerCharacters = new List<CharacterController>();
    protected GameObject _mainCamera;

    public bool skipEnemyPhase = false;

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

    public virtual void Initialize(List<CharacterController> playerCharacters)
    {
        _playerCharacters = playerCharacters;
    }

    public virtual void Move(
        System.Action onComplete)
    {
        if (AICanMove(out var path))
        {
            // lock on here
            if (path != null)
            {
                EnterCameraFocusCommand();
                _aiCharacter.MoveAlongPath(path, onComplete, skipEnemyPhase);
            }
        }
        else
        {
            onComplete?.Invoke();
        }
    }

    public virtual void PerformAction(
        System.Action onComplete)
    {
        if (AICanAttack(out var attack, out var targetCharacter))
        {
            _aiCharacter.PerformAction(attack, targetCharacter, onComplete);
        }
        else
        {
            onComplete?.Invoke();
        }
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
                _aiCharacter.MoveAlongPath(path, null, skipEnemyPhase);
            }
        }
        else if (AICanAttack(out var attack, out var targetCharacter))
        {
            _aiCharacter.PerformAction(attack, targetCharacter);
        }
    }

    private void EnterCameraFocusCommand()
    {
        if (skipEnemyPhase == false)
        {
            _mainCamera.GetComponent<CameraFollower>().EnterCameraFocusCommand(this.gameObject);
        }
    }

    protected virtual bool AICanAttack(out Action bestAttack, out Tile targetTile)
    {
        bestAttack = null;
        targetTile = null;
        if (!_aiCharacter.IsAbleToAttack())
        {
            return false;
        }

        bestAttack = GetMostEffectiveAttack(out targetTile);

        return bestAttack != null && targetTile != null;
    }

    protected virtual Action GetMostEffectiveAttack(out Tile tileToAttack)
    {
        Action bestAttack = null;
        var maxPotentialDamage = 0f;
        tileToAttack = null;

        if (_aiCharacter.Character.Weapon?.Actions == null ||
            _aiCharacter.Character.Weapon.Actions.Count == 0)
        {
            Debug.LogWarning($"Character {_aiCharacter.Character.Name} does not have a weapon with attacks.");
            return bestAttack;
        }

        foreach (var attack in _aiCharacter.Character.Weapon.Actions)
        {
            var potentialAttackDamage = StatCalculator.CalculateStat(_aiCharacter.Character, attack, StatType.Damage);
            var totalPotentialDamage = 0f;
            Tile potentialTileToAttack = null;

            var attackController = _aiCharacter.GetActionController(attack);
            attackController.CalculateAffectedTiles();
            var tilesThatCanAttacked = attackController.GetAffectedTiles();

            foreach (var targetTile in tilesThatCanAttacked.Values)
            {
                var areaOfAffect = StatCalculator.CalculateStat(attack, StatType.AttackAOE);
                var affectedTiles = TileGridController.Instance.GetTilesInRadius(targetTile.GridX, targetTile.GridY, 0, areaOfAffect);
                var damageOnAffectedTiles = 0f;

                foreach (var affectedTile in affectedTiles.Values)
                {
                    var playerCharacter = _playerCharacters.FirstOrDefault(p => p.Id == affectedTile.CharacterControllerId);
                    if (!string.IsNullOrEmpty(affectedTile.CharacterControllerId) &&
                        playerCharacter != null)
                    {
                        damageOnAffectedTiles += potentialAttackDamage;

                        var playerHealth = playerCharacter.GetHealthController().GetCurrentHealth() +
                            playerCharacter.GetHealthController().GetCurrentShield();

                        if (playerHealth <= potentialAttackDamage)
                        {
                            damageOnAffectedTiles += POINTS_FOR_KILLING_PLAYER_CHARACTER;
                        }
                    }
                }

                if (damageOnAffectedTiles > totalPotentialDamage)
                {
                    totalPotentialDamage = damageOnAffectedTiles;
                    potentialTileToAttack = targetTile;
                }
            }

            if (totalPotentialDamage > maxPotentialDamage)
            {
                maxPotentialDamage = totalPotentialDamage;
                bestAttack = attack;
                tileToAttack = potentialTileToAttack;
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

        var breadthFirstSearch = new BreadthFirstSearch(_grid);
        breadthFirstSearch.Execute(_grid.GetValue(_aiCharacter.transform.position), 10000, _aiCharacter.Character.NavigableTiles);
        var allNavigableTiles = breadthFirstSearch.GetVisitedTiles();

        var aStar = new AStar(_aiCharacter.Character.NavigableTiles);
        var tilesToExclude = new List<Tile>();
        var iterations = 0;
        List<Tile> pathToTarget = null;

        while (pathToTarget == null)
        {
            var targetTile = GetTileWithHighestPoints(tilesToExclude, allNavigableTiles.Values);
            pathToTarget = aStar.Execute(
                _grid.GetValue(_aiCharacter.transform.position),
                targetTile);
            if (pathToTarget == null)
            {
                Debug.LogWarning($"Unable to find path to tile {targetTile.GridX}, {targetTile.GridY}. Retrying.");
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

    private Tile GetTileWithHighestPoints(List<Tile> tilesToExclude, IEnumerable<Tile> allNavigableTiles)
    {
        Tile targetTile = _grid.GetValue(_aiCharacter.transform.position);
        foreach (var tile in allNavigableTiles)
        {
            if (tile.Points > targetTile.Points &&
                !tilesToExclude.Contains(tile))
            {
                targetTile = tile;
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

    public void turnOnSkipEnemyPhase()
    {
        skipEnemyPhase = true;
        Debug.Log(this.gameObject.name + " set to: " + skipEnemyPhase);
    }

    public void turnOffSkipEnemyPhase()
    {
        skipEnemyPhase = false;
        Debug.Log(this.gameObject.name + " set to: " + skipEnemyPhase);
    }
}
