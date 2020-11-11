using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class CameraController : MonoBehaviour
{
    private bool isCameraLock;
    public float PanSpeed = 20f;
    public float ZoomPositionSpeed = 10f;
    public float ZoomRotationSpeed = 50f;

    public Vector3 MinZoomPosition;
    public Vector3 MaxZoomPosition;
    public Vector3 MinZoomRotation;
    public Vector3 MaxZoomRotation;
   // public Vector3 MinRotate;
    //public Vector3 MaxRotate;

    private float _currentZoomPercent = 0;
    //private float _currentRotatePercent = 0;

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
            ApplyScrollWheel();
        }
    }

    private void ApplyScrollWheel()
    {
        _currentZoomPercent += -Input.GetAxis("Mouse ScrollWheel");
        _currentZoomPercent = Mathf.Clamp(_currentZoomPercent, 0f, 1f);

        CalculateZoom(_currentZoomPercent);
    }

    private void CalculateZoom(float zoomPercent)
    {
        var zoomPosition = Vector3.Lerp(MinZoomPosition, MaxZoomPosition, zoomPercent);
        _camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition, zoomPosition, ZoomPositionSpeed * Time.deltaTime);

        var zoomRotation = Quaternion.Lerp(Quaternion.Euler(MinZoomRotation), Quaternion.Euler(MaxZoomRotation), zoomPercent);
        _camera.transform.localRotation = Quaternion.Slerp(_camera.transform.localRotation, zoomRotation, ZoomRotationSpeed * Time.deltaTime);
    }

    public void setZoom(float inputZoom)
    {
        CalculateZoom(inputZoom);
        _currentZoomPercent = inputZoom;
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
