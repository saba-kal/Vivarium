﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class ActionController : MonoBehaviour
{
    public Action ActionReference;
    public ParticleSystem ParticleEffectPrefab;
    public float ParticleAffectLifetime = 5f;

    protected CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public virtual void Execute(Tile targetTile)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        var areaOfAffect = StatCalculator.CalculateStat(ActionReference, StatType.AttackAOE);
        var affectedTiles = TileGridController.Instance.GetTilesInRadius(targetTile.GridX, targetTile.GridY, 0, areaOfAffect);

        CommandController.Instance.ExecuteCommand(
            new MakeCharacterFaceTileCommand(
                _characterController,
                targetTile,
                true));

        this.ExecuteAction(affectedTiles);
    }

    // Looks at the action's animation type and performs the animation accordingly 
    protected virtual void PerformAnimation()
    {
        var animationType = ActionReference.AnimType;
        var animationTypeName = Enum.GetName(typeof(AnimationType), animationType);
        Debug.Log("DOING ANIMATION: " + animationTypeName);

        Animator myAnimator = gameObject.GetComponentInChildren<Animator>();
        myAnimator.SetTrigger(animationTypeName);
    }

    protected virtual void ExecuteAction(Dictionary<(int, int), Tile> affectedTiles)
    {
        this.PerformAnimation();

        var targetCharacterIds = new List<string>();

        foreach (var tile in affectedTiles)
        {
            if (!string.IsNullOrEmpty(tile.Value.CharacterControllerId))
            {
                targetCharacterIds.Add(tile.Value.CharacterControllerId);
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

            // executing the actual action on target character
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
        if (targetCharacter == null)
        {
            Debug.LogWarning($"Cannot execute action on target character {targetCharacter.Character.Name} because it is null. Most likely, the character is dead");
            return;
        }
        var damage = StatCalculator.CalculateStat(ActionReference, StatType.Damage);
        targetCharacter.TakeDamage(damage);
        Debug.Log($"{targetCharacter.Character.Name} took {damage} damage from {_characterController.Character.Name}.");
    }
}
