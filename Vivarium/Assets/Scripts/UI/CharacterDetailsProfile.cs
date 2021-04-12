using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using static InventorySlot;

/// <summary>
/// Handles the character info screen when characters are selected in a level.
/// </summary>
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

    private void Start()
    {
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

    /// <summary>
    /// Changes the character info screen.
    /// </summary>
    public void UpdateDisplay()
    {
        if (_characterController != null)
        {
            DisplayCharacter(_characterController);
        }
    }

    /// <summary>
    /// Shows character information and inventory for whatever the current character is.
    /// </summary>
    /// <param name="characterController">The character who's info will be displayed.</param>
    public void DisplayCharacter(CharacterController characterController)
    {
        Outline.gameObject.SetActive(false);
        ErrorsText.gameObject.SetActive(false);
        _characterController = characterController;
        Icon.sprite = _characterController.Character.Portrait;

        InventoryView.Display(_characterController);
        DisplayStats();
    }

    private void DisplayStats()
    {
        StatsText.text = $"HP: {_characterController.Character.MaxHealth:n0}\n" +
            $"ATK: {_characterController.Character.AttackDamage:n0}\n" +
            $"MV: {_characterController.Character.MoveRange:n0}";
    }

    /// <summary>
    /// Changes the callback for beginning to drag an item.
    /// </summary>
    /// <param name="dragBegin">Event where an inventory slot starts being dragged.</param>
    public void SetOnDragBeginCallback(SlotDragBegin dragBegin)
    {
        _onSlotDragBegin = dragBegin;
    }

    /// <summary>
    /// Changes the callback for ending a drag of an item.
    /// </summary>
    /// <param name="dragEnd">Event where an inventory slot stops being dragged.</param>
    public void SetOnDragEndCallback(SlotDragEnd dragEnd)
    {
        _onSlotDragEnd = dragEnd;
    }

    /// <summary>
    /// Changes the callback for dropping an item.
    /// </summary>
    /// <param name="slotDrop">Event where an inventory slot is dropped.</param>
    public void SetOnDropCallback(SlotDrop slotDrop)
    {
        _onSlotDrop = slotDrop;
    }

    /// <summary>
    /// Gets the current character controller for the profile.
    /// </summary>
    public CharacterController GetCharacter()
    {
        return _characterController;
    }

    /// <summary>
    /// Shows a semi transparent outline for the character profile screen.
    /// </summary>
    public void ShowHighlight()
    {
        Outline.color = HighlightColor;
        Outline.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the outline for the character profile screen.
    /// </summary>
    public void HideHighlight()
    {
        Outline.gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows an error message where appropriate on the profile screen..
    /// </summary>
    /// <param name="error">The text displayed for the error message.</param>
    public void ShowError(string error)
    {
        Outline.color = ErrorColor;
        Outline.gameObject.SetActive(true);
        ErrorsText.gameObject.SetActive(true);
        ErrorsText.text = error;
    }

    /// <summary>
    /// Hides errors from the profile like having no weapon equipped.
    /// </summary>
    public void HideError()
    {
        Outline.gameObject.SetActive(false);
        ErrorsText.gameObject.SetActive(false);
    }
}
