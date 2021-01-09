using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnKeyPress : MonoBehaviour
{
    public GameObject ObjectToActivate;
    public KeyCode KeyPressed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyPressed))
        {
            ObjectToActivate.SetActive(!ObjectToActivate.activeSelf);
        }
    }
}
