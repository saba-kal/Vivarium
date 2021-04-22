using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

/// <summary>
/// Handles zooming in the camera
/// </summary>
public class CameraZoomer : MonoBehaviour
{
    private bool isCameraLock;
    public float cameraZoomSpeed;
    public float maxZoom;
    public float minZoom;

    private float _currentZoomPercent = 0;

    private Camera _camera;

    private void Start()
    {


        isCameraLock = false;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCameraLock)
        {
            _currentZoomPercent = Input.GetAxis("Mouse ScrollWheel");
            _currentZoomPercent = Mathf.Clamp(_currentZoomPercent, 0f, -1f);
            if (transform.position.y < maxZoom && transform.position.y >= minZoom)
            {
                //transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * 10);
                transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * cameraZoomSpeed);
            }


        }
        if (transform.position.y > maxZoom)
        {
            while (transform.position.y > maxZoom)
            {
                transform.Translate(Vector3.forward);
            }

        }
        if (transform.position.y < minZoom)
        {
            while (transform.position.y < minZoom)
            {
                transform.Translate(Vector3.forward * -1);
            }
        }
    }


    public void setZoom(float inputZoom)
    {
        transform.Translate(Vector3.forward * -inputZoom);
    }

    public float getCurrentZoomPercent()
    {
        return transform.position.y / maxZoom;
    }


    /// <summary>
    /// Locks the Camera Zoomer, prevents the Camera Zoomer from being affected by player input. 
    /// </summary>
    public void lockCameraZoom()
    {
        isCameraLock = true;
    }

    public void unlockCameraZoom()
    {
        isCameraLock = false;
    }
}
