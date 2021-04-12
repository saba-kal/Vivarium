using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used for disabling buttons from being interacted with.
/// </summary>
public class DisableButton : MonoBehaviour
{
    public Button _button;
    public void Start()
    {
        _button = GameObject.Find("Continue").GetComponent<Button>();
        _button.interactable = false;
    }

}