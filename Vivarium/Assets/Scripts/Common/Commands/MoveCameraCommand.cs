using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class MoveCameraCommand : ICommand
{
    private float _resetZoom;
    private Vector3 _currentLocation;
    private Vector3 _destination;
    private GameObject _mainCamera;
    private GameObject _focusCharacter;
    private GameObject _cameraMover;
    private GameObject _camera;
    private System.Action _onMoveComplete;


    public MoveCameraCommand(
    GameObject mainCamera,
    GameObject focusCharacter,
    GameObject cameraMover,
    GameObject camera,
    Vector3 currentLocation,
    Vector3 destination,
    float resetZoom,
    System.Action onMoveComplete = null)
    {
        _mainCamera = mainCamera;
        _cameraMover = cameraMover;
        _camera = camera;
        _focusCharacter = focusCharacter;
        _destination = destination;
        _currentLocation = currentLocation;
        _onMoveComplete = onMoveComplete;
        _resetZoom = resetZoom;
    }

    public IEnumerator Execute()
    {
        //_camera.GetComponent<CameraController>().setZoom(1);
        _mainCamera.transform.rotation = Quaternion.identity;
        _mainCamera.GetComponent<CameraFollower>().lockCamera();
        var centerOffset = 5f;
        _destination = new Vector3(_destination.x, _destination.y, _destination.z - centerOffset);
        var destinationX = _destination.x;
        var destinationZ = _destination.z;
        var destinationY = _currentLocation.y;


        var diffX = destinationX - _currentLocation.x;
        var diffZ = destinationZ - _currentLocation.z;

        var arrived = false;

        var distance = calculateDistance(_destination, _currentLocation);
        //var step = distance * Time.deltaTime;
        var step = 0.04f;

        while (arrived == false)
        {
            _cameraMover.transform.position = Vector3.MoveTowards(_cameraMover.transform.position, new Vector3(destinationX, destinationY, destinationZ), step);
            var remainingDistance = calculateDistance(_destination, _cameraMover.transform.position);
            if (remainingDistance <= 0.01f)
            {
                arrived = true;
            }
            var decelerate = 0.0004f / remainingDistance;
            step = step - decelerate;
            if (step <= 0.015f)
            {
                step = 0.015f;
            }
            Debug.Log(remainingDistance);
            yield return null;
        }
        Debug.Log("SDFADSFSDFASDFSDF");
        _mainCamera.transform.SetParent(_focusCharacter.transform);
        _mainCamera.transform.localPosition = new Vector3(0, 0, 0);
        _cameraMover.transform.localPosition = new Vector3(0, 0, -centerOffset);
        _camera.transform.localPosition = new Vector3(0, 0, 0);
        _mainCamera.transform.rotation = Quaternion.identity;

        _onMoveComplete?.Invoke();

    }

    private float calculateDistance(Vector3 destination, Vector3 currentLocation)
    {
        var destinationX = destination.x;
        var destinationZ = destination.z;
        var destinationY = currentLocation.y;


        var diffX = destinationX - currentLocation.x;
        var diffZ = destinationZ - currentLocation.z;

        var distance = Mathf.Sqrt((diffX * diffX) + (diffZ * diffZ));
        return distance;
    }
}
