using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class CharacterDetailsProfile : MonoBehaviour
{
    public int MaxItems = 3;
    public Image Icon;
    public TextMeshProUGUI StatsText;
    public GameObject InventoryContainer;
    public InventorySlot InventorySlotPrefab;

    public void DisplayCharacter(CharacterController characterController)
    {
        Icon.sprite = characterController.Character.Portrait;
        var inventoryItems = InventoryManager.GetCharacterItems(characterController.Id);

        for (var i = 0; i < MaxItems; i++)
        {
            var inventorySlot = Instantiate(InventorySlotPrefab, InventoryContainer.transform);
            if (i < inventoryItems.Count)
            {
                inventorySlot.SetItem(inventoryItems[i], characterController);
            }
            else
            {
                inventorySlot.SetItem(null);
            }
        }
    }
}
