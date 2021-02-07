using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mover : MonoBehaviour
{
    private bool isCameraLock;
    public float speed;

    private void Start()
    {
        isCameraLock = false;
    }

    // Update is called once per frame
    public void CheckMoveCamera()
    {
        if (!isCameraLock)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
        }
    }


    public void lockCameraMover()
    {
        isCameraLock = true;
    }

    public void unlockCameraMover()
    {
        isCameraLock = false;
    }








    ////
    ////private bool isCameraLock;
    //public float PanSpeed = 20f;
    ////public float ZoomPositionSpeed = 10f;
    ////public float ZoomRotationSpeed = 50f;

    ////public Vector3 MinZoomPosition;
    ////public Vector3 MaxZoomPosition;
    ////public Vector3 MinZoomRotation;
    ////public Vector3 MaxZoomRotation;

    //private float _currentZoomPercent = 0;
    ////private float _currentZPercent = 0;


    //// Update is called once per frame
    //void Update()
    //{
    //    if (!isCameraLock)
    //    {
    //        ApplyScrollWheel();
    //    }
    //    CalculateZoom(_currentZoomPercent);
    //}

    //private void ApplyScrollWheel()
    //{
    //    _currentZoomPercent += -Input.GetAxis("Mouse ScrollWheel");
    //    _currentZoomPercent = Mathf.Clamp(_currentZoomPercent, 0f, 1f);

    //    //var calculatedY = _currentZoomPercent * 20;
    //    //transform.position = new Vector3(transform.position.x, calculatedY, -calculatedY);


    //}

    //private void CalculateZoom(float zoomPercent)
    //{
    //    var calculatedY = zoomPercent * 20;
    //    //transform.position = new Vector3(transform.position.x, calculatedY, transform.position.z);
    //    transform.position = new Vector3(transform.position.x, transform.position.y, -calculatedY);


    //    //var zoomPosition = Vector3.Lerp(MinZoomPosition, MaxZoomPosition, zoomPercent);
    //    //_camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition, zoomPosition, ZoomPositionSpeed * Time.deltaTime);

    //    //var zoomRotation = Quaternion.Lerp(Quaternion.Euler(MinZoomRotation), Quaternion.Euler(MaxZoomRotation), zoomPercent);
    //    //_camera.transform.localRotation = Quaternion.Slerp(_camera.transform.localRotation, zoomRotation, ZoomRotationSpeed * Time.deltaTime);
    //}

    //public void setZoom(float inputZoom)
    //{
    //    CalculateZoom(inputZoom);
    //    _currentZoomPercent = inputZoom;
    //}


    //public void lockCameraZoom()
    //{
    //    isCameraLock = true;
    //}

    //public void unlockCameraZoom()
    //{
    //    isCameraLock = false;
    //}
}
