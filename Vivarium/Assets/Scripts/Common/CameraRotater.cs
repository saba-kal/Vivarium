using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    //public float speed;
    public float rotateSpeed;
    private bool isCameraLock;

    //public float PanSpeed = 20f;
    //public float ZoomPositionSpeed = 10f;
    //public float ZoomRotationSpeed = 50f;
    //private float _currentZoomPercent = 0;
    //public Vector3 MinZoomPosition;
    //public Vector3 MaxZoomPosition;
    //public Vector3 MinZoomRotation;
    //public Vector3 MaxZoomRotation;


    private void Start()
    {
        isCameraLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCameraLock)
        {
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            }

            //if (Input.GetKey(KeyCode.R))
            //{
            //    transform.Rotate(-rotateSpeed * Time.deltaTime, 0, 0);
            //}
            //if (Input.GetKey(KeyCode.T))
            //{
            //    transform.Rotate(rotateSpeed * Time.deltaTime, 0,0);
            //}
        }
    }

    public void lockCameraRotater()
    {
        isCameraLock = true;
    }

    public void unlockCameraRotater()
    {
        isCameraLock = false;
    }
}
