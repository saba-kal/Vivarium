using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryInitializer : MonoBehaviour
{
    public List<InventoryItem> StartingItems;

    public void Initialize(bool placeEquippedItems)
    {
        var turnSystemManager = TurnSystemManager.Instance;
        if (turnSystemManager?.PlayerController?.PlayerCharacters != null)
        {
            foreach (var characterController in turnSystemManager.PlayerController.PlayerCharacters)
            {
                if ((placeEquippedItems || !characterController.gameObject.activeSelf)
                    && characterController?.Character?.Weapon != null)
                {
                    InventoryManager.PlaceCharacterItem(characterController.Id, characterController.Character.Weapon);
                }
                if ((placeEquippedItems || !characterController.gameObject.activeSelf)
                    && characterController?.Character?.Shield != null)
                {
                    InventoryManager.PlaceCharacterItem(characterController.Id, characterController.Character.Shield);
                }
                if (StartingItems != null)
                {
                    foreach (var inventoryItem in StartingItems)
                    {
                        for (var i = 0; i < inventoryItem.Count; i++)
                        {
                            InventoryManager.PlaceCharacterItem(characterController.Id, inventoryItem.Item);

                            //TODO: figure out a better system for shields.
                            if (inventoryItem.Item.Type == ItemType.Shield && characterController.Character.Shield == null)
                            {
                                characterController.Equip(inventoryItem.Item);
                            }
                        }
                    }
                }
            }
        }
        else if (!Application.isEditor)
        {
            Debug.LogError("Unable to initialize inventory because either the turn system manager, player controller, or player characters are null.");
        }
    }

    public void InitializeForEnemies()
    {
        var turnSystemManager = TurnSystemManager.Instance;
        if (turnSystemManager?.AIManager?.AICharacters != null)
        {
            foreach (var characterController in turnSystemManager.AIManager.AICharacters)
            {
                if (characterController?.Character?.Weapon != null)
                {
                    InventoryManager.PlaceCharacterItem(characterController.Id, characterController.Character.Weapon);
                }
                if (characterController?.Character?.Shield != null)
                {
                    InventoryManager.PlaceCharacterItem(characterController.Id, characterController.Character.Shield);
                }
            }
        }
    }
}
