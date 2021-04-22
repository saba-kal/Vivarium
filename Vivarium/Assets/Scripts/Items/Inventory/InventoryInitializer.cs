using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Initializes the inventories of the player characters and gives them starting items
/// </summary>
public class InventoryInitializer : MonoBehaviour
{
    public List<InventoryItem> StartingItems;

    /// <summary>
    /// Places the starting items and shield in the player character inventories
    /// </summary>
    /// <param name="placeEquippedItems">A bool to check if the starter items are placed in the player inventories</param>
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
