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
                if (StartingItems != null)
                {
                    foreach (var inventoryItem in StartingItems)
                    {
                        var inventoryItemCopy = InventoryItem.Copy(inventoryItem);
                        inventoryItemCopy.InventoryPosition = -1;

                        InventoryManager.PlaceCharacterItem(characterController.Id, inventoryItemCopy);

                        //TODO: figure out a better system for shields.
                        if (inventoryItemCopy.Item.Type == ItemType.Shield && characterController.Character.Shield == null)
                        {
                            characterController.Equip(inventoryItemCopy);
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
