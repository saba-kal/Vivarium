using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryInitializer : MonoBehaviour
{
    public List<Item> startingItems;
    private void Start()
    {
        var turnSystemManager = TurnSystemManager.Instance;
        if (turnSystemManager?.PlayerController?.PlayerCharacters != null)
        {
            foreach (var playerCharacterController in turnSystemManager.PlayerController.PlayerCharacters)
            {
                if (playerCharacterController?.Character?.Weapon != null)
                {
                    InventoryManager.PlaceCharacterItem(playerCharacterController.Id, playerCharacterController.Character.Weapon);
                }
                if (playerCharacterController?.Character?.Shield != null)
                {
                    InventoryManager.PlaceCharacterItem(playerCharacterController.Id, playerCharacterController.Character.Shield);
                }
                foreach (var startingItem in startingItems)
                {
                    var consumable = (Consumable)startingItem;
                    for (var i = 0; i < consumable.charges; i++)
                    {
                        InventoryManager.PlaceCharacterItem(playerCharacterController.Id, startingItem);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Unable to initialize inventory because either the turn system manager, player controller, or player characters are null.");
        }
    }
}
