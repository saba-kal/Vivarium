using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public Button _button;
    public void Start()
    {
        _button = GameObject.Find("Continue").GetComponent<Button>();
        _button.interactable = false;
    }

}