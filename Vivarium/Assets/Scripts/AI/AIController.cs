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
        CharacterController.OnDamageTaken += OnCharacterDamage;
    }

    void OnDisable()
    {
        CharacterController.OnDeath -= OnCharacterDeath;
        CharacterController.OnDamageTaken -= OnCharacterDamage;
    }

    void Start()
    {
        VirtualStart();
    }

    /// <summary>
    /// Container for MonoBehaviour Start. Allows it to be overridden.
    /// </summary>
    protected virtual void VirtualStart()
    {
        _grid = TileGridController.Instance.GetGrid();
        _aiCharacter = GetComponent<CharacterController>();
        _mainCamera = GameObject.FindGameObjectWithTag("MasterCamera");
    }

    /// <summary>
    /// Initializes AI at the start of a level.
    /// </summary>
    /// <remarks>
    /// Fires before the built-in MonoBehaviour Start method.
    /// </remarks>
    public virtual void Initialize()
    {
        return; //Meant to be overridden.
    }

    /// <summary>
    /// Initializes AI per turn.
    /// </summary>
    /// <param name="playerCharacters">List of player characters that are alive this turn.</param>
    public virtual void InitializeTurn(List<CharacterController> playerCharacters)
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

    /// <summary>
    /// Computes the best possible action at the current position and executes it.
    /// </summary>
    /// <param name="onComplete">Callback for when the action is complete.</param>
    public virtual void PerformAction(
        System.Action onComplete)
    {
        if (AICanAttack(out var attack, out var targetTile))
        {
            EnterCameraFocusCommand();
            _aiCharacter.PerformAction(attack, targetTile, onComplete);
        }
        else
        {
            onComplete?.Invoke();
        }
    }

    protected void EnterCameraFocusCommand()
    {
        if (skipEnemyPhase == false)
        {
            _mainCamera.GetComponent<MasterCameraScript>().EnterCameraFocusCommand(this.gameObject);
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

        bestAttack = GetMostEffectiveAttack(out targetTile, out var _);

        return bestAttack != null && targetTile != null;
    }

    protected virtual Action GetMostEffectiveAttack(
        out Tile tileToAttack,
        out float maxPotentialDamage)
    {
        Action bestAttack = null;
        maxPotentialDamage = 0f;
        tileToAttack = null;

        if (_aiCharacter.Character.Weapon?.Actions == null ||
            _aiCharacter.Character.Weapon.Actions.Count == 0)
        {
            Debug.LogWarning($"Character {_aiCharacter.Character.Name} does not have a weapon with attacks.");
            return bestAttack;
        }

        foreach (var attack in _aiCharacter.Character.Weapon.Actions)
        {
            if (attack.ControllerType == ActionControllerType.Heal ||
                attack.ControllerType == ActionControllerType.MinionSummon)
            {
                continue;
            }

            if (!(attack.ControllerType == ActionControllerType.Skewer && !_aiCharacter.IsAbleToMove()))
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

                            damageOnAffectedTiles += playerCharacter.Character.Aggro;
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
        }

        return bestAttack;
    }

    protected virtual bool AICanMove(out List<Tile> path)
    {
        path = null;
        if (!_aiCharacter.IsAbleToMove() || _aiCharacter.Character.MoveRange == 0)
        {
            return false;
        }

        var breadthFirstSearch = new BreadthFirstSearch(_grid, true);
        breadthFirstSearch.Execute(_grid.GetValue(_aiCharacter.transform.position), 10000, _aiCharacter.Character.NavigableTiles);
        var allNavigableTiles = breadthFirstSearch.GetVisitedTiles();

        var aStar = new AStar(_aiCharacter.Character.NavigableTiles, _aiCharacter.Character.CanMoveThroughCharacters);
        var tilesToExclude = new Dictionary<(int, int), Tile>();
        var iterations = 0;
        List<Tile> pathToTarget = null;
        var waterTileCost = 0;

        while (pathToTarget == null)
        {
            var targetTile = GetTileWithHighestPoints(tilesToExclude, allNavigableTiles.Values);
            pathToTarget = aStar.Execute(
                _grid.GetValue(_aiCharacter.transform.position),
                targetTile);

            if (pathToTarget == null)
            {
                Debug.LogWarning($"Unable to find path to tile {targetTile.GridX}, {targetTile.GridY}. Retrying.");
                tilesToExclude.Add((targetTile.GridX, targetTile.GridY), targetTile);
            }
            else
            {
                path = GetTravelPath(pathToTarget);

                // Don't want to land on a tile that can be passed over (e.g. water or character)
                if (TileCanBePassedOver(path.Last()))
                {
                    aStar.SetIgnoreCharacters(false);
                    waterTileCost++; //Iteratively increase the cost of water tile until AI does not land on one.
                    aStar.SetWaterTileCost(waterTileCost);
                    pathToTarget = null;
                }
            }

            iterations++;
            if (iterations > MAX_MOVE_CALC_ITERATIONS)
            {
                Debug.LogError("Reached maximum move calculation iterations for AI character.");
                return false;
            }
        }

        return true;
    }

    private List<Tile> GetTravelPath(List<Tile> pathToTarget)
    {
        var travelRange = StatCalculator.CalculateStat(_aiCharacter.Character, StatType.MoveRadius);
        var path = new List<Tile>();
        for (int step = 0; step < travelRange && step < pathToTarget.Count; step++)
        {
            path.Add(pathToTarget[step]);
        }

        return path;
    }

    private bool TileCanBePassedOver(Tile tile)
    {
        return (_aiCharacter.Character.NavigableTiles.Contains(TileType.Water) && tile.Type == TileType.Water) ||
            (_aiCharacter.Character.CanMoveThroughCharacters && !string.IsNullOrEmpty(tile.CharacterControllerId));
    }

    private Tile GetTileWithHighestPoints(Dictionary<(int, int), Tile> tilesToExclude, IEnumerable<Tile> allNavigableTiles)
    {
        Tile targetTile = _grid.GetValue(_aiCharacter.transform.position);
        foreach (var tile in allNavigableTiles)
        {
            if (tile.Points > targetTile.Points &&
                string.IsNullOrEmpty(tile.CharacterControllerId) &&
                !tilesToExclude.ContainsKey((tile.GridX, tile.GridY)) &&
                tile.Type == TileType.Grass)
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

    protected virtual void OnCharacterDeath(CharacterController deadCharacterController)
    {
        for (var i = 0; i < _playerCharacters.Count; i++)
        {
            if (_playerCharacters[i].Id == deadCharacterController.Id)
            {
                _playerCharacters.RemoveAt(i);
            }
        }
    }

    protected virtual void OnCharacterDamage(CharacterController characterController)
    {
        return; //Meant to be overridden.
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
