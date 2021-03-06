using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class QueenBeeAIController : AIController
{
    public int MaxSummons;

    private List<BeeHiveAIController> _beeHives = null;
    private List<CharacterController> _summonedBees = new List<CharacterController>();
    private HealActionController _healActionController;
    private MinionSummonActionController _minionSummonActionController;

    protected override void VirtualStart()
    {
        base.VirtualStart();

        var healAction = _aiCharacter.Character.Weapon.Actions.FirstOrDefault(a => a.ControllerType == ActionControllerType.Heal);
        var summonAction = _aiCharacter.Character.Weapon.Actions.FirstOrDefault(a => a.ControllerType == ActionControllerType.MinionSummon);
        if (healAction == null || summonAction == null)
        {
            Debug.LogError("Queen bee needs heal and summon actions.");
            return;
        }

        _healActionController = (HealActionController)_aiCharacter.GetActionController(healAction);
        _minionSummonActionController = (MinionSummonActionController)_aiCharacter.GetActionController(summonAction);

        GetBeehives();
        for (int i = 0; i < MaxSummons; i++)
        {
            SummonBee(null);
        }
    }

    public override void InitializeTurn(List<CharacterController> playerCharacters)
    {
        base.InitializeTurn(playerCharacters);
    }

    public override void Move(
        System.Action onComplete)
    {
        base.Move(onComplete);
    }

    public override void PerformAction(
        System.Action onComplete)
    {
        GetBeehives();

        if (_summonedBees.Count < MaxSummons &&
            _beeHives.Count > 0)
        {
            var isSuccessfull = SummonBee(onComplete);
            if (!isSuccessfull)
            {
                onComplete?.Invoke();
            }
        }
        else
        {
            base.PerformAction(onComplete);
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

    private bool SummonBee(System.Action onComplete)
    {
        if (_beeHives == null || _beeHives.Count == 0)
        {
            Debug.LogWarning("Unable to summon bee because there are no bee hives.");
            return false;
        }

        //Get spawn point.
        var spawnTile = GetBeeSpawnTile();
        if (spawnTile == null)
        {
            Debug.LogWarning("Unable to find a free spot next to a bee hive in order to summon a bee.");
            return false;
        }

        _minionSummonActionController.Execute(spawnTile, onComplete);

        //Store character in array.
        _summonedBees.Add(_minionSummonActionController.GetSummonedCharacter());

        return true;
    }

    private Tile GetBeeSpawnTile()
    {
        var grid = TileGridController.Instance;
        var randomBeeHiveIndex = Random.Range(0, _beeHives.Count);
        var randomBeeHive = _beeHives[randomBeeHiveIndex];
        var summonAttempts = 0;

        while (summonAttempts <= _beeHives.Count)
        {
            summonAttempts++;

            var beeHiveGridPosition = grid.GetGrid().GetValue(randomBeeHive.transform.position);
            var adjacentTiles = grid.GetAdjacentTiles(beeHiveGridPosition, TileType.Grass);
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

    private void GetMostEffectiveHeal(
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
                var summonedBee = _summonedBees.FirstOrDefault(p => p.Id == affectedTile.CharacterControllerId);
                if (!string.IsNullOrEmpty(affectedTile.CharacterControllerId) &&
                    summonedBee != null)
                {
                    var missingHealth = summonedBee.Character.MaxHealth - summonedBee.GetHealthController().GetCurrentHealth();
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
}