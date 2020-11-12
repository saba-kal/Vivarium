using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator
{
    public GameObject GenerateCharacter(CharacterGenerationProfile characterProfile, bool isEnemy)
    {
        var characterData = GenerateCharacterData(characterProfile);
        characterData.Attributes = new AttributesGenerator().GenerateAttributes(characterProfile.AttributeProfile);
        characterData.Weapon = new WeaponGenerator().GenerateWeapon(characterProfile.WeaponProfile);
        characterData.Shield = new ShieldGenerator().GenerateShield(characterProfile.ShieldProfile);

        var characterGameObject = new GameObject(characterData.Name);

        var characterController = characterGameObject.AddComponent<CharacterController>();
        characterController.Id = characterData.Id;
        characterController.Character = characterData;
        characterController.IsEnemy = isEnemy;

        characterGameObject.AddComponent<MoveController>();

        CreateHealthBar(characterGameObject, characterProfile);

        if (isEnemy)
        {
            characterGameObject.tag = Constants.ENEMY_CHAR_TAG;
            characterGameObject.AddComponent<AIController>();
        }
        else
        {
            characterGameObject.tag = Constants.PLAYER_CHAR_TAG;
        }

        AddActionHandlingToGameObject(characterGameObject, characterData, isEnemy);
        GenerateCharacterModel(characterGameObject, characterProfile);

        return characterGameObject;
    }

    private Character GenerateCharacterData(CharacterGenerationProfile characterProfile)
    {
        var character = new Character();
        character.Id = Guid.NewGuid().ToString();
        character.Name = characterProfile.PossibleNames[UnityEngine.Random.Range(0, characterProfile.PossibleNames.Count)];
        character.MaxHealth = UnityEngine.Random.Range(characterProfile.MinMaxHealth, characterProfile.MaxMaxHealth);
        character.MoveRange = UnityEngine.Random.Range(characterProfile.MinMoveRange, characterProfile.MaxMoveRange);
        character.NavigableTiles = characterProfile.NavigableTiles;

        return character;
    }

    private void CreateHealthBar(
        GameObject characterGameObject,
        CharacterGenerationProfile characterProfile)
    {
        var healthController = characterGameObject.AddComponent<HealthController>();

        var healthBarObject = GameObject.Instantiate(characterProfile.HealthBarPrefab);
        healthBarObject.transform.SetParent(characterGameObject.transform);

        var healthBars = healthBarObject.GetComponentsInChildren<HealthBar>();
        foreach (var healthbar in healthBars)
        {
            if (healthbar.isShieldBar)
            {
                healthController.ShieldBar = healthbar;
            }
            else
            {
                healthController.HealthBar = healthbar;
            }
        }
    }

    private void AddActionHandlingToGameObject(
        GameObject characterGameObject,
        Character characterData,
        bool isEnemy)
    {
        var isPlayer = !isEnemy;

        foreach (var action in characterData.Weapon.Actions)
        {
            ActionController actionController;
            ActionViewer actionViewer = null;

            switch (action.ControllerType)
            {
                case ActionControllerType.GiantLazer:
                    actionController = characterGameObject.AddComponent<GiantLazerActionController>();
                    if (isPlayer)
                    {
                        //TODO: create an action viewer for the giant laser so that maybe a player character can use it.
                        actionViewer = characterGameObject.AddComponent<ActionViewer>();
                    }
                    break;
                case ActionControllerType.Projectile:
                    actionController = characterGameObject.AddComponent<ProjectileActionController>();
                    if (isPlayer)
                    {
                        actionViewer = characterGameObject.AddComponent<ActionViewer>();
                    }
                    break;
                case ActionControllerType.KnockBack:
                    actionController = characterGameObject.AddComponent<KnockBackActionController>();
                    if (isPlayer)
                    {
                        actionViewer = characterGameObject.AddComponent<ActionViewer>();
                    }
                    break;
                case ActionControllerType.SwitchPosition:
                    actionController = characterGameObject.AddComponent<SwitchPositionActionController>();
                    if (isPlayer)
                    {
                        actionViewer = characterGameObject.AddComponent<ActionViewer>();
                    }
                    break;
                case ActionControllerType.Default:
                default:
                    actionController = characterGameObject.AddComponent<ActionController>();
                    if (isPlayer)
                    {
                        actionViewer = characterGameObject.AddComponent<ActionViewer>();
                    }
                    break;
            }

            actionController.ActionReference = action;
            if (isPlayer)
            {
                actionViewer.ActionReference = action;
            }
        }
    }

    private void GenerateCharacterModel(
        GameObject characterGameObject,
        CharacterGenerationProfile characterProfile)
    {
        var characterModelPrefab = characterProfile.PossibleModels[UnityEngine.Random.Range(0, characterProfile.PossibleModels.Count)];

        // creates the prefab and model onto the scene
        var prefabInstance = GameObject.Instantiate(characterModelPrefab, characterGameObject.transform);

        // adds animator to the model of the INSTANCE of the character
        Animator animator = prefabInstance.gameObject.AddComponent<Animator>() as Animator;
        animator.runtimeAnimatorController = characterProfile.AnimationController;
    }
}
