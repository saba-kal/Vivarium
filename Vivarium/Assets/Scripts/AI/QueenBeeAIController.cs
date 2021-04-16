using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// <see cref="AIController"/> for the Queen Bee character.
/// </summary>
public class QueenBeeAIController : AIController
{
    private const string PHASE1_ANIM_KEY = "phase1";

    public int MaxSummons;
    public int StartingSummons;
    public int ActionsPerTurn;

    private QueenBeePhase _phase = QueenBeePhase.Defend;
    private List<BeeHiveAIController> _beeHives = null;
    private List<CharacterController> _summonedBees = new List<CharacterController>();
    private HealActionController _healActionController;
    private MinionSummonActionController _minionSummonActionController;
    private Animator _animator;

    protected override void VirtualStart()
    {
        base.VirtualStart();
        GetQueenBeeActions();
        GetBeehives();
        PerformStartOfLevelSummon();
        _animator = GetComponentInChildren<Animator>();
        _animator?.SetBool(PHASE1_ANIM_KEY, true);
    }

    private void GetQueenBeeActions()
    {
        var healAction = _aiCharacter.Character.Weapon.Actions.FirstOrDefault(a => a.ControllerType == ActionControllerType.Heal);
        var summonAction = _aiCharacter.Character.Weapon.Actions.FirstOrDefault(a => a.ControllerType == ActionControllerType.MinionSummon);
        if (healAction == null || summonAction == null)
        {
            Debug.LogError("Queen bee needs heal and summon actions.");
            return;
        }

        _healActionController = (HealActionController)_aiCharacter.GetActionController(healAction);
        _minionSummonActionController = (MinionSummonActionController)_aiCharacter.GetActionController(summonAction);
    }

    private void PerformStartOfLevelSummon()
    {
        _minionSummonActionController.DisableSound();
        _minionSummonActionController.SkipCommandQueue = true;
        var excludedTileSpawns = new Dictionary<(int, int), Tile>();
        for (int i = 0; i < StartingSummons; i++)
        {
            var spawnTile = GetBeeSpawnTile(excludedTileSpawns);
            if (spawnTile == null)
            {
                Debug.LogWarning("Unable to find a free spot next to a bee hive in order to summon a bee.");
                continue;
            }
            SummonBee(spawnTile, null);
            excludedTileSpawns.Add((spawnTile.GridX, spawnTile.GridY), spawnTile);
        }
        _minionSummonActionController.EnableSound();
        _minionSummonActionController.SkipCommandQueue = false;
    }

    /// <inheritdoc cref="AIController.Move(System.Action)"/>
    public override void Move(
        System.Action onComplete)
    {
        if (_phase == QueenBeePhase.Attack)
        {
            base.Move(onComplete);
        }
        else
        {
            onComplete?.Invoke();
        }
    }

    /// <summary>
    /// Computes a queue of best possible actions at the current position and executes them one-by-one.
    /// </summary>
    /// <param name="onComplete">Callback for when all actions have been executed.</param>
    public override void PerformAction(
        System.Action onComplete)
    {
        GetBeehives();

        var currentActionNumber = 0;
        var actionCallbacks = new Queue<QueenBeeAIAction>();
        var excludedActions = new List<Action>();
        var nextActionExists = GetNextAction(excludedActions, out var bestAction, out var targetTile);

        while (nextActionExists && currentActionNumber < ActionsPerTurn)
        {
            actionCallbacks.Enqueue(
                new QueenBeeAIAction
                {
                    ActionReference = bestAction,
                    TargetTile = targetTile,
                    ExecuteAction = (action, target, onActionComplete) =>
                    {
                        EnterCameraFocusCommand();
                        if (action.ControllerType == ActionControllerType.MinionSummon)
                        {
                            SummonBee(target, onActionComplete);
                        }
                        else
                        {
                            _aiCharacter.PerformAction(action, target, onActionComplete);
                        }
                    }
                });

            excludedActions.Add(bestAction);
            nextActionExists = GetNextAction(excludedActions, out bestAction, out targetTile);
            currentActionNumber++;
        }

        ExecuteActions(actionCallbacks, onComplete);
    }

    private void ExecuteActions(Queue<QueenBeeAIAction> actions, System.Action onComplete)
    {
        if (actions.Count == 0)
        {
            onComplete();
        }
        else
        {
            var action = actions.Dequeue();
            action.ExecuteAction(action.ActionReference, action.TargetTile, () =>
            {
                ExecuteActions(actions, onComplete);
            });
        }
    }

    private void GetBeehives()
    {
        _beeHives = new List<BeeHiveAIController>();

        var enemyCharacters = TurnSystemManager.Instance?.AIManager?.AICharacters;
        if (enemyCharacters == null || enemyCharacters.Count == 0)
        {
            Debug.LogError("Level must have bee hives as enemy characters for the queen bee to be able to summon bees.");
            return;
        }

        foreach (var characterController in enemyCharacters)
        {
            if (characterController.Character.Type == CharacterType.BeeHive)
            {
                var beeHiveAi = characterController.GetComponent<BeeHiveAIController>();
                if (beeHiveAi == null)
                {
                    Debug.LogError("Be hive character must be using a bee hive AI controller. Check to ensure the character generator is setting the correct AI");
                    continue;
                }

                _beeHives.Add(beeHiveAi);
            }
        }
    }

    private bool SummonBee(Tile targetTile, System.Action onComplete)
    {
        if (_beeHives == null || _beeHives.Count == 0)
        {
            Debug.LogWarning("Unable to summon bee because there are no bee hives.");
            return false;
        }

        _minionSummonActionController.SetOnCharacterSummon(newCharacterController =>
        {
            _summonedBees.Add(newCharacterController);
        });
        _minionSummonActionController.Execute(targetTile, onComplete);

        //Store character in array.

        return true;
    }

    private Tile GetBeeSpawnTile(Dictionary<(int, int), Tile> excludedTiles = null)
    {
        var grid = TileGridController.Instance;
        var randomBeeHiveIndex = Random.Range(0, _beeHives.Count);
        var randomBeeHive = _beeHives[randomBeeHiveIndex];
        var summonAttempts = 0;

        while (summonAttempts <= _beeHives.Count)
        {
            summonAttempts++;

            var beeHiveGridPosition = grid.GetGrid().GetValue(randomBeeHive.transform.position);
            var adjacentTiles = grid.GetAdjacentTiles(beeHiveGridPosition, TileType.Grass, false, excludedTiles);
            if (adjacentTiles.Count == 0)
            {
                randomBeeHiveIndex = (randomBeeHiveIndex + 1) % _beeHives.Count;
                randomBeeHive = _beeHives[randomBeeHiveIndex];
            }
            else
            {
                return adjacentTiles[Random.Range(0, adjacentTiles.Count)];
            }
        }

        return null;
    }

    private bool GetNextAction(List<Action> excludedActions, out Action bestAction, out Tile targetTile)
    {
        bestAction = null;
        targetTile = null;
        if (!_aiCharacter.IsAbleToAttack())
        {
            return false;
        }

        var canSummon = _summonedBees.Count < MaxSummons && _beeHives.Count > 0;
        if (canSummon && !excludedActions.Contains(_minionSummonActionController.ActionReference))
        {
            if (_beeHives == null || _beeHives.Count == 0)
            {
                Debug.LogWarning("Unable to summon bee because there are no bee hives.");
            }
            else
            {
                var spawnTile = GetBeeSpawnTile();
                if (spawnTile != null)
                {
                    targetTile = spawnTile;
                    bestAction = _minionSummonActionController.ActionReference;
                    return true;
                }
            }
        }

        var bestDamageAction = GetMostEffectiveAttack(out var targetAttackTile, out var potentialDamage);
        var bestHealAction = GetMostEffectiveHeal(out var targetHealTile, out var potentialHeal);
        if (excludedActions.Contains(bestDamageAction))
        {
            bestDamageAction = null;
        }
        if (excludedActions.Contains(bestHealAction))
        {
            bestHealAction = null;
        }

        var damageActionExists = bestDamageAction != null && targetAttackTile != null;
        var healActionExists = bestHealAction != null && targetHealTile != null;

        if ((damageActionExists && healActionExists && potentialDamage > potentialHeal) ||
            (damageActionExists && !healActionExists))
        {
            targetTile = targetAttackTile;
            bestAction = bestDamageAction;
        }
        else if ((damageActionExists && healActionExists && potentialDamage <= potentialHeal) ||
            (!damageActionExists && healActionExists))
        {
            targetTile = targetHealTile;
            bestAction = bestHealAction;
        }
        else
        {
            return false;
        }

        return true;
    }

    protected override bool AICanAttack(out Action bestAttack, out Tile targetTile)
    {
        bestAttack = null;
        targetTile = null;
        if (!_aiCharacter.IsAbleToAttack())
        {
            return false;
        }

        var bestDamageAction = GetMostEffectiveAttack(out var targetAttackTile, out var potentialDamage);
        GetMostEffectiveHeal(out var targetHealTile, out var potentialHeal);

        var damageActionExists = bestDamageAction != null && targetAttackTile != null;
        var healActionExists = _healActionController != null && targetHealTile != null;

        if ((damageActionExists && healActionExists && potentialDamage > potentialHeal) ||
            (damageActionExists && !healActionExists))
        {
            targetTile = targetAttackTile;
            bestAttack = bestDamageAction;
        }
        else if ((damageActionExists && healActionExists && potentialDamage <= potentialHeal) ||
            (!damageActionExists && healActionExists))
        {
            targetTile = targetHealTile;
            bestAttack = _healActionController.ActionReference;
        }
        else
        {
            return false;
        }

        return true;
    }

    private Action GetMostEffectiveHeal(
        out Tile targetTile,
        out float maxPotentialHeal)
    {
        targetTile = null;
        maxPotentialHeal = 0;
        var potentialHeal = StatCalculator.CalculateStat(_aiCharacter.Character, _healActionController.ActionReference, StatType.Damage);

        _healActionController.CalculateAffectedTiles();
        var tilesThatCanBeHealed = _healActionController.GetAffectedTiles();

        foreach (var potentialTargetTile in tilesThatCanBeHealed.Values)
        {
            var areaOfAffect = StatCalculator.CalculateStat(_healActionController.ActionReference, StatType.AttackAOE);
            var affectedTiles = TileGridController.Instance.GetTilesInRadius(potentialTargetTile.GridX, potentialTargetTile.GridY, 0, areaOfAffect);
            var healOnAffectedTiles = 0f;

            foreach (var affectedTile in affectedTiles.Values)
            {
                if (string.IsNullOrEmpty(affectedTile.CharacterControllerId))
                {
                    continue;
                }

                var allyCharacter = TurnSystemManager.Instance.AIManager.AICharacters
                    .FirstOrDefault(p => p?.Id == affectedTile.CharacterControllerId);

                if (allyCharacter != null)
                {
                    var missingHealth = allyCharacter.Character.MaxHealth - allyCharacter.GetHealthController().GetCurrentHealth();
                    var healAmount = Mathf.Clamp(missingHealth, 0, potentialHeal);
                    healOnAffectedTiles += healAmount;
                }
            }

            if (healOnAffectedTiles > maxPotentialHeal)
            {
                maxPotentialHeal = healOnAffectedTiles;
                targetTile = potentialTargetTile;
            }
        }

        if (targetTile == null)
        {
            return null;
        }

        return _healActionController.ActionReference;
    }

    protected override void OnCharacterDeath(CharacterController deadCharacterController)
    {
        base.OnCharacterDeath(deadCharacterController);
        for (var i = 0; i < _summonedBees.Count; i++)
        {
            if (_summonedBees[i].Id == deadCharacterController.Id)
            {
                _summonedBees.RemoveAt(i);
            }
        }
    }

    protected override void OnCharacterDamage(CharacterController characterController)
    {
        base.OnCharacterDamage(characterController);

        if (characterController.Id == _aiCharacter.Id)
        {
            _phase = QueenBeePhase.Attack;
            _animator?.SetBool(PHASE1_ANIM_KEY, false);
        }
    }
}

public enum QueenBeePhase
{
    Defend = 0,
    Attack = 1
}