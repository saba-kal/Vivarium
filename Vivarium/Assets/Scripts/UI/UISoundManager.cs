﻿using UnityEngine;
using System.Collections;

public class UISoundManager : MonoBehaviour
{
    private SoundManager _soundManager;

    void OnEnable()
    {
        InventorySlot.OnSlotClick += OnInventorySlotClick;
        InventoryUIController.OnEquipClick += PlayEquip;
        InventoryUIController.OnConsumeClick += PlayConsume;
        UIController.OnActionClick += OnActionClick;
    }

    void OnDisable()
    {
        InventorySlot.OnSlotClick -= OnInventorySlotClick;
        InventoryUIController.OnEquipClick -= PlayEquip;
        InventoryUIController.OnConsumeClick -= PlayConsume;
        UIController.OnActionClick -= OnActionClick;
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

    private void OnActionClick(Action inventorySlot)
    {
        _soundManager.Play(Constants.BUTTON_CLICK_SOUND);
    }

    public void PlayButtonClick()
    {
        _soundManager.Play(Constants.BUTTON_CLICK_SOUND);
    }

    public void PlayEquip()
    {
        _soundManager.Play(Constants.EQUIP_SOUND);
    }

    public void PlayConsume()
    {
        _soundManager.Play(Constants.CONSUME_SOUND);
    }

    public void Play(string soundName)
    {
        _soundManager.Play(soundName);
    }
}