using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator
{
    private Grid<Tile> _grid;

    public CharacterGenerator(Grid<Tile> grid)
    {
        _grid = grid;
    }

    public GameObject GenerateCharacter(CharacterGenerationProfile characterProfile, bool isEnemy)
    {
        var characterData = GenerateCharacterData(characterProfile);
        characterData.Attributes = new AttributesGenerator().GenerateAttributes(characterProfile.AttributeProfile);
        characterData.Weapon = new WeaponGenerator().GenerateWeapon(characterProfile.WeaponProfile);

        var characterGameObject = new GameObject(characterData.Name);

        var characterController = characterGameObject.AddComponent<CharacterController>();
        characterController.Character = characterData;
        characterController.IsEnemy = isEnemy;

        characterGameObject.AddComponent<MoveController>();

        if (isEnemy)
        {
            characterGameObject.tag = Globals.Instance.EnemyTag;
            characterGameObject.AddComponent<AIController>();
        }
        else
        {
            characterGameObject.tag = Globals.Instance.PlayerCharacterTag;
        }

        AddActionHandlingToGameObject(characterGameObject, characterData, isEnemy);

        return characterGameObject;
    }

    private Character GenerateCharacterData(CharacterGenerationProfile characterProfile)
    {
        var character = new Character();
        character.Id = Guid.NewGuid();
        character.Name = characterProfile.PossibleNames[UnityEngine.Random.Range(0, characterProfile.PossibleNames.Count)];
        character.MaxHealth = UnityEngine.Random.Range(characterProfile.MinMaxHealth, characterProfile.MaxMaxHealth);
        character.MoveRange = UnityEngine.Random.Range(characterProfile.MinMoveRange, characterProfile.MaxMoveRange);
        character.NavigableTiles = characterProfile.NavigableTiles;

        return character;
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

            //TODO: add animations and particle affects to the action controller.
        }
    }
}
