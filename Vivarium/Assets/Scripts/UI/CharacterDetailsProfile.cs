using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using static InventorySlot;

public class CharacterDetailsProfile : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI StatsText;
    public InventoryItemsView InventoryView;
    public Image Outline;
    public TextMeshProUGUI ErrorsText;
    public Color HighlightColor = Color.green;
    public Color ErrorColor = Color.red;

    private SlotDragBegin _onSlotDragBegin;
    private SlotDragEnd _onSlotDragEnd;
    private SlotDrop _onSlotDrop;

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
        InventoryView.Display(_characterController);
        InventoryView.SetOnDragBeginCallback((slot) =>
        {
            _onSlotDragBegin?.Invoke(slot);
        });
        InventoryView.SetOnDragEndCallback((slot) =>
        {
            _onSlotDragEnd?.Invoke(slot);
        });
        InventoryView.SetOnDropCallback((dropSlot, droppedSlot) =>
        {
            _onSlotDrop?.Invoke(dropSlot, droppedSlot);
        });
    }

    private void DisplayStats()
    {
        StatsText.text = $"HP: {_characterController.Character.MaxHealth:n0}\n" +
            $"ATK: {_characterController.Character.AttackDamage:n0}\n" +
            $"MV: {_characterController.Character.MoveRange:n0}";
    }

    public void SetOnDragBeginCallback(SlotDragBegin dragBegin)
    {
        _onSlotDragBegin = dragBegin;
    }

    public void SetOnDragEndCallback(SlotDragEnd dragEnd)
    {
        _onSlotDragEnd = dragEnd;
    }

    public void SetOnDropCallback(SlotDrop slotDrop)
    {
        _onSlotDrop = slotDrop;
    }

    public CharacterController GetCharacter()
    {
        return _characterController;
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
