using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Generates a character GameObject based on a given CharacterGenerationProfile.
/// </summary>
public class CharacterGenerator
{
    /// <summary>
    /// Generates a character GameObject based on a given CharacterGenerationProfile.
    /// </summary>
    /// <param name="characterProfile"><see cref="CharacterGenerationProfile"/> containing the information used to generate the character.</param>
    /// <param name="isEnemy">a bool representing whether or not the character is an enemy.</param>
    /// <returns>A GameObject with components containing the character data, <see cref="MoveController"/>, and <see cref="AIController"/>.</returns>
    public GameObject GenerateCharacter(CharacterGenerationProfile characterProfile, bool isEnemy)
    {
        var characterData = GenerateCharacterData(characterProfile);
        characterData.Attributes = new AttributesGenerator().GenerateAttributes(characterProfile.AttributeProfile);
        characterData.Weapon = new WeaponGenerator().GenerateWeapon(characterProfile.WeaponProfile);
        characterData.Shield = new ShieldGenerator().GenerateShield(characterProfile.ShieldProfile);

        var characterGameObject = new GameObject(characterData.Flavor.Name);

        var characterController = characterGameObject.AddComponent<CharacterController>();
        characterController.Id = characterData.Id;
        characterController.Character = characterData;
        characterController.IsEnemy = isEnemy;


        var weaponItem = new InventoryItem { Item = characterData.Weapon, Count = 1, InventoryPosition = 0 };
        InventoryManager.PlaceCharacterItem(characterController, weaponItem);
        characterController.Equip(weaponItem);
        if (characterData.Shield != null)
        {
            var shieldItem = new InventoryItem { Item = characterData.Shield, Count = 1, InventoryPosition = 1 };
            InventoryManager.PlaceCharacterItem(characterController, shieldItem);
            characterController.Equip(shieldItem);
        }

        characterGameObject.AddComponent<MoveController>();

        CreateHealthBar(characterGameObject, characterProfile, characterController);

        if (isEnemy)
        {
            characterGameObject.tag = Constants.ENEMY_CHAR_TAG;
            switch (characterData.Type)
            {
                case CharacterType.BeeHive:
                    characterGameObject.AddComponent<BeeHiveAIController>();
                    break;
                case CharacterType.QueenBee:
                    var queenBeeAi = characterGameObject.AddComponent<QueenBeeAIController>();
                    queenBeeAi.MaxSummons = characterProfile.MaxSummons;
                    queenBeeAi.StartingSummons = characterProfile.StartingSummons;
                    queenBeeAi.ActionsPerTurn = characterProfile.ActionsPerTurn;
                    queenBeeAi.StartSummonDelay = characterProfile.StartSummonDelay;
                    break;
                case CharacterType.Normal:
                default:
                    characterGameObject.AddComponent<AIController>();
                    break;
            }
        }
        else
        {
            characterGameObject.tag = Constants.PLAYER_CHAR_TAG;
        }

        AddActionHandlingToGameObject(characterGameObject, characterData, isEnemy);
        GenerateCharacterModel(characterController, characterProfile);

        return characterGameObject;
    }

    private Character GenerateCharacterData(CharacterGenerationProfile characterProfile)
    {
        var character = new Character();
        character.Id = Guid.NewGuid().ToString();
        character.Flavor = FlavorText.FromFlavorTextData(characterProfile.CharacterFlavorText);
        if (characterProfile.PossiblePortraits.Count > 0)
        {
            character.Portrait = characterProfile.PossiblePortraits[UnityEngine.Random.Range(0, characterProfile.PossiblePortraits.Count)];
        }
        character.MaxHealth = UnityEngine.Random.Range(characterProfile.MinMaxHealth, characterProfile.MaxMaxHealth);
        character.AttackDamage = UnityEngine.Random.Range(characterProfile.MinAttackDamage, characterProfile.MaxAttackDamage);
        character.MoveRange = UnityEngine.Random.Range(characterProfile.MinMoveRange, characterProfile.MaxMoveRange);
        character.NavigableTiles = characterProfile.NavigableTiles;
        character.AICharacterHeuristics = characterProfile.AICharacterHeuristics;
        character.CharacterLootTable = characterProfile.CharacterLootTable;
        character.MaxItems = characterProfile.MaxItems;
        character.Type = characterProfile.Type;
        character.CanMoveThroughCharacters = characterProfile.CanMoveThroughCharacters;
        character.Aggro = characterProfile.Aggro;
        character.unitType = characterProfile.unitType;

        return character;
    }

    private void CreateHealthBar(
        GameObject characterGameObject,
        CharacterGenerationProfile characterProfile,
        CharacterController characterController)
    {
        var healthController = characterGameObject.AddComponent<HealthController>();

        var healthBarObject = GameObject.Instantiate(characterProfile.HealthBarPrefab);
        healthBarObject.transform.SetParent(characterGameObject.transform);

        var healthBars = healthBarObject.GetComponentsInChildren<HealthBar>();
        var icons = healthBarObject.GetComponentsInChildren<HealthBarIcon>();
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

        foreach (var icon in icons)
        {
            icon.SetCharacterController(characterController);
        }
    }

    private void AddActionHandlingToGameObject(
        GameObject characterGameObject,
        Character characterData,
        bool isEnemy)
    {

        foreach (var action in characterData.Weapon.Actions)
        {
            ActionFactory.Create(characterGameObject, action, out var _, out var _);
        }
    }

    private void GenerateCharacterModel(
        CharacterController characterGameObject,
        CharacterGenerationProfile characterProfile)
    {
        var characterModelPrefab = characterProfile.PossibleModels[UnityEngine.Random.Range(0, characterProfile.PossibleModels.Count)];

        // creates the prefab and model onto the scene
        var prefabInstance = GameObject.Instantiate(characterModelPrefab, characterGameObject.transform);
        characterGameObject.Model = prefabInstance;

        // adds animator to the model of the INSTANCE of the character
        var modelObject = prefabInstance.gameObject.transform.GetChild(0).gameObject;

        if (modelObject.GetComponent<Animator>() == null)
        {
            var animator = modelObject.AddComponent<Animator>() as Animator;
            animator.runtimeAnimatorController = characterProfile.AnimationController;
        }

    }
}
