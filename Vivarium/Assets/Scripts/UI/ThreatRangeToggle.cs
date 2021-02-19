﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ThreatRangeToggle : MonoBehaviour
{
    public delegate void ToggleChange(bool toggleValue);
    public static event ToggleChange OnToggleChange;

    private Toggle _toggle;

    void Start()
    {
        _toggle = GetComponent<Toggle>();
        if (_toggle != null)
        {
            _toggle.onValueChanged.AddListener((bool isOn) =>
            {
                OnToggleChange?.Invoke(isOn);
            });
        }
    }
}