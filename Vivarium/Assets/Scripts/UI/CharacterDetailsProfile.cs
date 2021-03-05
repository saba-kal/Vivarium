using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using static InventorySlot;

public class CharacterDetailsProfile : MonoBehaviour
{
    public int MaxItems = 3;
    public Image Icon;
    public TextMeshProUGUI StatsText;
    public GameObject InventoryContainer;
    public InventorySlot InventorySlotPrefab;
    public Image Outline;
    public TextMeshProUGUI ErrorsText;
    public Color HighlightColor = Color.green;
    public Color ErrorColor = Color.red;

    private SlotDragBegin _onSlotDragBegin;
    private SlotDragEnd _onSlotDragEnd;
    private CharacterController _characterController;
    private int _equippedWeaponIndex = -1;
    private int _equippedShieldIndex = -1;

    public void DisplayCharacter(CharacterController characterController)
    {
        Outline.gameObject.SetActive(false);
        ErrorsText.gameObject.SetActive(false);
        _characterController = characterController;
        Icon.sprite = _characterController.Character.Portrait;
        DisplayInventory();
        DisplayStats();
    }

    private void DisplayInventory()
    {
        var inventoryItems = InventoryManager.GetCharacterItems(_characterController.Id);
        for (var i = 0; i < MaxItems; i++)
        {
            var inventorySlot = Instantiate(InventorySlotPrefab, InventoryContainer.transform);
            if (i < inventoryItems.Count)
            {
                inventorySlot.SetItem(inventoryItems[i], _characterController);
                inventorySlot.AddOnDragBeginCallback((slot) =>
                {
                    _onSlotDragBegin?.Invoke(slot);
                });
                inventorySlot.AddOnDragEndCallback((slot) =>
                {
                    _onSlotDragEnd?.Invoke(slot);
                });

                if (_characterController.ItemIsEquipped(inventoryItems[i].Item))
                {
                    if (inventoryItems[i].Item.Type == ItemType.Weapon && _equippedWeaponIndex < 0)
                    {
                        _equippedWeaponIndex = i;
                        inventorySlot.DisplayEquipOverlay();
                    }
                    else if (inventoryItems[i].Item.Type == ItemType.Shield && _equippedShieldIndex < 0)
                    {
                        _equippedShieldIndex = i;
                        inventorySlot.DisplayEquipOverlay();
                    }
                    else
                    {
                        inventorySlot.HideEquipOverlay();
                    }
                }
                else
                {
                    inventorySlot.HideEquipOverlay();
                }
            }
            else
            {
                inventorySlot.SetItem(null);
            }
        }
    }

    private void DisplayStats()
    {
        StatsText.text = $"HP: {_characterController.Character.MaxHealth:n0}\n" +
            $"ATK: {_characterController.Character.AttackDamage:n0}\n" +
            $"MV: {_characterController.Character.MoveRange:n0}";
    }

    public void AddOnDragBeginCallback(SlotDragBegin dragBegin)
    {
        _onSlotDragBegin = dragBegin;
    }

    public void AddOnDragEndCallback(SlotDragEnd dragEnd)
    {
        _onSlotDragEnd = dragEnd;
    }

    public CharacterController GetCharacter()
    {
        return _characterController;
    }

    public bool ItemIsEquipped(Item item, int itemIndex)
    {
        if (_characterController == null)
        {
            return false;
        }

        var itemIsEquiped = _characterController.ItemIsEquipped(item);

        if (itemIsEquiped && item.Type == ItemType.Weapon && _equippedWeaponIndex != itemIndex)
        {
            return false;
        }
        else if (itemIsEquiped && item.Type == ItemType.Shield && _equippedShieldIndex != itemIndex)
        {
            return false;
        }

        return itemIsEquiped;
    }

    public void ShowHighlight()
    {
        Outline.color = HighlightColor;
        Outline.gameObject.SetActive(true);
    }

    public void HideHighlight()
    {
        Outline.gameObject.SetActive(false);
    }

    public void ShowError(string error)
    {
        Outline.color = ErrorColor;
        Outline.gameObject.SetActive(true);
        ErrorsText.gameObject.SetActive(true);
        ErrorsText.text = error;
    }

    public void HideError()
    {
        Outline.gameObject.SetActive(false);
        ErrorsText.gameObject.SetActive(false);
    }
}
