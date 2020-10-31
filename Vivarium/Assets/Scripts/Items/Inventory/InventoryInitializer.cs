﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryInitializer : MonoBehaviour
{

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
            }
        }
        else
        {
            Debug.LogError("Unable to initialize inventory because either the turn system manager, player controller, or player characters are null.");
        }
    }
}
