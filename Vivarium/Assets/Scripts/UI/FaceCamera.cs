using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Places a camera in front of characters' faces. Used for the character info screen.
/// </summary>
public class FaceCamera : MonoBehaviour
{
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
