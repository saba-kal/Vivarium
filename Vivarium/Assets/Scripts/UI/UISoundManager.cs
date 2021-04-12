using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Plays sound effects for various UI interactions.
/// </summary>
public class UISoundManager : MonoBehaviour
{
    private SoundManager _soundManager;

    void OnEnable()
    {
        InventorySlot.OnSlotClick += OnInventorySlotClick;
        InventoryUIController.OnEquipClick += PlayEquip;
        InventoryUIController.OnConsumeClick += PlayConsume;
        UnitInspectionController.OnActionClick += OnActionClick;
        UnitInspectionController.OnMoveClick += OnMoveClick;
    }

    void OnDisable()
    {
        InventorySlot.OnSlotClick -= OnInventorySlotClick;
        InventoryUIController.OnEquipClick -= PlayEquip;
        InventoryUIController.OnConsumeClick -= PlayConsume;
        UnitInspectionController.OnActionClick -= OnActionClick;
        UnitInspectionController.OnMoveClick -= OnMoveClick;
    }

    // Use this for initialization
    void Start()
    {
        _soundManager = SoundManager.GetInstance();
    }
    
    private void OnInventorySlotClick(InventorySlot inventorySlot)
    {
        _soundManager.Play(Constants.BUTTON_CLICK_SOUND);
    }

    private void OnMoveClick()
    {
        _soundManager.Play(Constants.BUTTON_CLICK_SOUND);
    }

    private void OnActionClick(Action inventorySlot)
    {
        _soundManager.Play(Constants.BUTTON_CLICK_SOUND);
    }
    /// <summary>
    /// Plays the sound effect for clicking a button.
    /// </summary>
    public void PlayButtonClick()
    {
        _soundManager.Play(Constants.BUTTON_CLICK_SOUND);
    }
    /// <summary>
    /// Plays the sound effect for equipping an item.
    /// </summary>
    /// /// <param name="characterController">The character controller of the character from OnEquipClick.</param>
    public void PlayEquip(CharacterController characterController)
    {
        _soundManager.Play(Constants.EQUIP_SOUND);
    }
    /// <summary>
    /// Plays the consume sound effect.
    /// </summary>
    /// /// /// <param name="characterController">The character controller of the character from OnConsumeClick.</param>
    public void PlayConsume(CharacterController characterController)
    {
        _soundManager.Play(Constants.CONSUME_SOUND);
    }
    /// <summary>
    /// Internal play function for the UI sound manager. Currently unused.
    /// </summary>
    /// /// <param name="soundName">Plays the sound name</param>
    public void Play(string soundName)
    {
        _soundManager.Play(soundName);
    }
}
