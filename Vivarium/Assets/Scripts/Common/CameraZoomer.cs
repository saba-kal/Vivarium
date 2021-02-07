using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class CameraZoomer : MonoBehaviour
{
    private bool isCameraLock;
    public float PanSpeed = 20f;
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
            //ApplyScrollWheel();
            _currentZoomPercent = Input.GetAxis("Mouse ScrollWheel");
            _currentZoomPercent = Mathf.Clamp(_currentZoomPercent, 0f, -1f);
            if (transform.position.y < 20 && transform.position.y >= 0)
            {
                transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * 10);
            }
            if (transform.position.y > 20)
            {
                while (transform.position.y > 20)
                {
                    transform.Translate(Vector3.forward);
                }

            }
            if (transform.position.y < 0)
            {
                while (transform.position.y < 0)
                {
                    transform.Translate(Vector3.forward * -1);
                }
            }

        }
        //CalculateZoom(_currentZoomPercent);
    }

    //private void ApplyScrollWheel()
    //{
    //    _currentZoomPercent += -Input.GetAxis("Mouse ScrollWheel");
    //    _currentZoomPercent = Mathf.Clamp(_currentZoomPercent, 0f, 1f);

    //}

    //private void CalculateZoom(float zoomPercent)
    //{
    //    var calculatedY = zoomPercent * 20;
    //    //transform.position = new Vector3(transform.position.x, calculatedY, transform.position.z);
    //    transform.position = new Vector3(transform.position.x, calculatedY, transform.position.z);


    //}

    public void setZoom(float inputZoom)
    {
        //CalculateZoom(inputZoom);
        _currentZoomPercent = -inputZoom;
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
