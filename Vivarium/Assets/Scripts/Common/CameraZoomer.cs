using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class CameraZoomer : MonoBehaviour
{
    private bool isCameraLock;
    //public float PanSpeed = 20f;
    public float cameraZoomSpeed;
    public float maxZoom;
    public float minZoom;
    //public float ZoomPositionSpeed = 10f;
    //public float ZoomRotationSpeed = 50f;

   //public Vector3 MinZoomPosition;
    //public Vector3 MaxZoomPosition;
    //public Vector3 MinZoomRotation;
    //public Vector3 MaxZoomRotation;

    private float _currentZoomPercent = 0;
    //private float _currentZPercent = 0;

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

    public void lockCameraZoom()
    {
        isCameraLock = true;
    }

    public void unlockCameraZoom()
    {
        isCameraLock = false;
    }
}
