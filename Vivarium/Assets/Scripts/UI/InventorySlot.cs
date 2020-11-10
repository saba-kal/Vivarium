using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public delegate void SlotClick(Item item, InventorySlot inventorySlot);
    public static event SlotClick OnSlotClick;

    public Image Icon;
    public Button Button;
    public TextMeshProUGUI Count;
    public GameObject EquipOverlay;

    private InventoryItem _inventoryItem;
    private CharacterController _selectedCharacter;

    private void Start()
    {
        Clear();
        Button.onClick.AddListener(() =>
        {
            OnSlotClick?.Invoke(_inventoryItem?.Item, this);
        });
    }

    public void SetItem(InventoryItem inventoryItem, CharacterController selectedCharacter)
    {
        _inventoryItem = inventoryItem;
        _selectedCharacter = selectedCharacter;
        UpdateItemDisplay();
    }

    public void UpdateItemDisplay()
    {
        if (_inventoryItem == null || InventoryManager.GetCharacterItem(_selectedCharacter.Id, _inventoryItem.Item.Id) == null)
        {
            Clear();
            return;
        }

        Icon.sprite = _inventoryItem.Item.Icon;
        Icon.gameObject.SetActive(true);
        Button.interactable = true;
        Icon.gameObject.SetActive(true);
        Count.text = _inventoryItem.Count.ToString();

        if ((_inventoryItem.Item.Type == ItemType.Shield || _inventoryItem.Item.Type == ItemType.Weapon) &&
            (_selectedCharacter.Character.Weapon.Id == _inventoryItem.Item.Id || _selectedCharacter.Character.Shield.Id == _inventoryItem.Item.Id))
        {
            EquipOverlay.SetActive(true);
        }
        else
        {
            EquipOverlay.SetActive(false);
        }
    }

    public void Clear()
    {
        _inventoryItem = null;
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        Button.interactable = false;
        Icon.gameObject.SetActive(false);
        Count.text = "0";
        EquipOverlay.SetActive(false);
    }
}
