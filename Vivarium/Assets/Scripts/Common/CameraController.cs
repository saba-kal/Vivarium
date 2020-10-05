using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class CameraController : MonoBehaviour
{

    public float PanSpeed = 20f;
    public float ZoomPositionSpeed = 10f;
    public float ZoomRotationSpeed = 50f;

    public Vector3 MinZoomPosition;
    public Vector3 MaxZoomPosition;
    public Vector3 MinZoomRotation;
    public Vector3 MaxZoomRotation;

    private float _currentZoomPercent = 0;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            position.z += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.z -= PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= PanSpeed * Time.deltaTime;
        }

        transform.position = position;

        CalculateZoom();
    }

    private void CalculateZoom()
    {
        _currentZoomPercent += -Input.GetAxis("Mouse ScrollWheel");
        _currentZoomPercent = Mathf.Clamp(_currentZoomPercent, 0f, 1f);

        var zoomPosition = Vector3.Lerp(MinZoomPosition, MaxZoomPosition, _currentZoomPercent);
        _camera.transform.localPosition = Vector3.Slerp(_camera.transform.localPosition, zoomPosition, ZoomPositionSpeed * Time.deltaTime);

        var zoomRotation = Quaternion.Lerp(Quaternion.Euler(MinZoomRotation), Quaternion.Euler(MaxZoomRotation), _currentZoomPercent);
        _camera.transform.localRotation = Quaternion.Slerp(_camera.transform.localRotation, zoomRotation, ZoomRotationSpeed * Time.deltaTime);
    }
}
