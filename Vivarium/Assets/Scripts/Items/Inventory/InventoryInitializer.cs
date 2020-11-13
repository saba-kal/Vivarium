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
            foreach (var playerCharacterController in turnSystemManager.PlayerController.PlayerCharacters)
            {
                if (placeEquippedItems && playerCharacterController?.Character?.Weapon != null)
                {
                    InventoryManager.PlaceCharacterItem(playerCharacterController.Id, playerCharacterController.Character.Weapon);
                }
                if (placeEquippedItems && playerCharacterController?.Character?.Shield != null)
                {
                    InventoryManager.PlaceCharacterItem(playerCharacterController.Id, playerCharacterController.Character.Shield);
                }
                if (StartingItems != null)
                {
                    foreach (var inventoryItem in StartingItems)
                    {
                        for (var i = 0; i < inventoryItem.Count; i++)
                        {
                            InventoryManager.PlaceCharacterItem(playerCharacterController.Id, inventoryItem.Item);

                            //TODO: figure out a better system for shields.
                            if (inventoryItem.Item.Type == ItemType.Shield && playerCharacterController.Character.Shield == null)
                            {
                                playerCharacterController.Equip(inventoryItem.Item);
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
}
