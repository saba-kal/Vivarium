using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeUIController : MonoBehaviour
{
    public Button TradeButton;
    public GameObject ItemsContainer1;
    public GameObject ItemsContainer2;
    public GameObject SelectedInventorySlotOverlayPrefab;
    public TextMeshProUGUI ItemDescription;

    private List<InventorySlot> _tradeItems1 = new List<InventorySlot>();
    private List<InventorySlot> _tradeItems2 = new List<InventorySlot>();

    void OnEnable()
    {
        InventorySlot.OnSlotClick += OnInventorySlotClick;
    }

    void OnDisable()
    {
        InventorySlot.OnSlotClick -= OnInventorySlotClick;
    }

    private void Start()
    {
        _tradeItems1 = ItemsContainer1.GetComponentsInChildren<InventorySlot>().ToList();
        _tradeItems2 = ItemsContainer1.GetComponentsInChildren<InventorySlot>().ToList();
    }

    public void Display(CharacterController character1, CharacterController character2)
    {
        foreach (var inventorySlot in _tradeItems1.Concat(_tradeItems2))
        {
            inventorySlot.Clear();
        }

        PopulateTradeItems(_tradeItems1, character1);
        PopulateTradeItems(_tradeItems2, character2);
    }

    private void PopulateTradeItems(List<InventorySlot> tradeItems, CharacterController characterController)
    {
        var characterInventory = InventoryManager.GetCharacterItems(characterController.Id);
        for (var i = 0; i < tradeItems.Count && i < characterInventory.Count; i++)
        {
            tradeItems[i].SetItem(characterInventory[i], characterController);
            if (ItemIsEquipped(characterInventory[i].Item, characterController))
            {
                tradeItems[i].DisplayEquipOverlay();
            }
            else
            {
                tradeItems[i].HideEquipOverlay();
            }
        }
    }

    private bool ItemIsEquipped(Item item, CharacterController characterController)
    {
        return (item.Type == ItemType.Shield || item.Type == ItemType.Weapon) &&
            (characterController.Character.Weapon?.Id == item.Id || characterController.Character.Shield?.Id == item.Id);
    }

    private void OnInventorySlotClick(InventorySlot inventorySlot)
    {
        if (inventorySlot?.GetItem()?.Item == null)
        {
            Debug.LogError("Selected trade item is null.");
            return;
        }

        UpdateTradeButton();

        var overlay = Instantiate(SelectedInventorySlotOverlayPrefab, inventorySlot.transform);
        ItemDescription.text = inventorySlot.GetItem().Item.Name;
    }

    private void UpdateTradeButton()
    {

    }
}