using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Each action represents one thing, typically an attack, that can be done using a weapon.
/// </summary>
[CreateAssetMenu(fileName = "New Action", menuName = "Action", order = 6)]
public class Action : ScriptableObject
{
    /// <summary>
    /// Identifies the type of action.
    /// </summary>
    public string Id;

    /// <summary>
    /// Contains text of the name of the action and a description.
    /// </summary>
    public FlavorTextData ActionFlavorText;

    /// <summary>
    /// The name of the sound that is played when the action is used.
    /// </summary>
    public string SoundName;

    /// <summary>
    /// The damage done by the action before any other modifiers are applied.
    /// </summary>
    public float BaseDamage;

    /// <summary>
    /// Value from 0 to 1 representing the percent chance the action will land a critical hit.
    /// </summary>
    [Range(0, 1)]
    public float CriticalChance;

    /// <summary>
    /// The value that the base damage will be multiplied by in the case of a critical hit.
    /// </summary>
    public float CriticalMultiplier;

    /// <summary>
    /// The minimum number of tiles a target can be from the user to use this action on it.
    /// </summary>
    public float MinRange;

    /// <summary>
    /// The maximum number of tiles a target can be from the user to use this action on it.
    /// </summary>
    public float MaxRange;

    /// <summary>
    /// The radius of the tiles surrounding the target tile that will also be affected by the action.
    /// </summary>
    public float AreaOfAffect;

    /// <summary>
    /// The type of character that can be targeted with the action (Opponent, self, or both).
    /// </summary>
    public ActionTarget ActionTargetType;

    /// <summary>
    /// The type of action controller required for this action.
    /// </summary>
    public ActionControllerType ControllerType;

    /// <summary>
    /// List of attributes that can affect the base stats of the action, such as modifiers to damage or crit chance.
    /// </summary>
    public List<Attribute> Attributes;

    /// <summary>
    /// The type of animation that will be used in game when the action is used.
    /// </summary>
    public AnimationType AnimType;

    /// <summary>
    /// The GameObject used to display projectiles from relevant actions, such as the shot of a bow.
    /// </summary>
    public GameObject ProjectilePrefab;

    /// <summary>
    /// The particles that will be visible around the target of the action upon its use.
    /// </summary>
    public ParticleSystem ParticleEffect;

    /// <summary>
    /// The particles that will be visible around the tiles affected by the action upon its use.
    /// </summary>
    public GameObject TileParticleEffect;

    /// <summary>
    /// The time delay before the action is performed.
    /// </summary>
    public float ActionTriggerDelay = 1f;

    /// <summary>
    /// The generation profile of the character that is summoned by the action.
    /// </summary>
    public CharacterGenerationProfile SummonProfile;

    /// <summary>
    /// Gets the flavor text of the action.
    /// </summary>
    public FlavorText Flavor { get => FlavorText.FromFlavorTextData(ActionFlavorText); }
}
