using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class MoveCameraCommand : ICommand
{
    private float _resetZoom;
    private float _extraSpeed;
    private Vector3 _currentLocation;
    private Vector3 _destination;
    private GameObject _mainCamera;
    private GameObject _focusCharacter;
    private GameObject _cameraMover;
    private GameObject _camera;
    private System.Action _onMoveComplete;


    public MoveCameraCommand(
    Vector3 destination,
    float extraSpeed,
    float resetZoom,
    GameObject focusCharacter = null,
    System.Action onMoveComplete = null)
    {
        _extraSpeed = extraSpeed;
        _focusCharacter = focusCharacter;
        _destination = destination;
        _onMoveComplete = onMoveComplete;
        _resetZoom = resetZoom;
    }

    public IEnumerator Execute()
    {
        _mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        _currentLocation = _mainCamera.transform.position;
        _cameraMover = _mainCamera.GetComponent<CameraFollower>().GetCameraMover();
        _camera = _mainCamera.GetComponent<CameraFollower>().GetCamera();

        _mainCamera.GetComponent<CameraFollower>().ResetZoom();

        _mainCamera.transform.rotation = Quaternion.identity;
        _mainCamera.GetComponent<CameraFollower>().lockCamera();
        var centerOffset = 5f; // connect this to camera
        _destination = new Vector3(_destination.x, _destination.y, _destination.z - centerOffset);
        var destinationX = _destination.x;
        var destinationZ = _destination.z;
        var destinationY = _currentLocation.y;


        var arrived = false;

        var distance = 0f;
        if (_focusCharacter != null)
        {
            distance = calculateDistance(_focusCharacter.transform.position, _currentLocation);
        }
        else
        {
            distance = calculateDistance(new Vector3(destinationX, destinationY, destinationZ), _currentLocation);
        }

        //var step = (_extraSpeed * distance) * Time.deltaTime;
        var step = _extraSpeed * Time.deltaTime;
        while (arrived == false)
        {
            _cameraMover.transform.position = Vector3.MoveTowards(_cameraMover.transform.position, new Vector3(destinationX, destinationY, destinationZ), step);
            var remainingDistance = calculateDistance(_destination, _cameraMover.transform.position);
            if (remainingDistance <= 0.01f)
            {
                arrived = true;
            }
            var decelerate = 0.02f; // remainingDistance;
            if (remainingDistance >= 3f && remainingDistance < 7f)
            {
                step = step - decelerate;
            }

            if (remainingDistance > 0.01f  && remainingDistance < 3f)
            {
                step = 0.07f;
            }

            if (step <= 0.07f)
            {
                step = 0.07f;
            }
            yield return null;
        }

        if (_focusCharacter != null)
        {
            _mainCamera.transform.SetParent(_focusCharacter.transform);
            //_mainCamera.transform.localPosition = new Vector3(0, 0, 0);
            //_cameraMover.transform.localPosition = new Vector3(0, 0, -centerOffset);
            //_camera.transform.localPosition = new Vector3(0, 0, 0);
            //_mainCamera.transform.rotation = Quaternion.identity;
            //_mainCamera.GetComponent<CameraFollower>().HighlightDiscOn();
        }
        else
        {
            _mainCamera.GetComponent<CameraFollower>().ResetCamera();
            //_mainCamera.transform.SetParent(null);
            //_mainCamera.transform.localPosition = new Vector3(0, 0, 0);
            //_cameraMover.transform.localPosition = new Vector3(0, 0, -centerOffset);
            //_camera.transform.localPosition = new Vector3(0, 0, 0);
            //_mainCamera.transform.rotation = Quaternion.identity;
        }
        _onMoveComplete?.Invoke();

    }

    private float calculateDistance(Vector3 destination, Vector3 currentLocation)
    {
        var destinationX = destination.x;
        var destinationZ = destination.z;


        var diffX = destinationX - currentLocation.x;
        var diffZ = destinationZ - currentLocation.z;

        var distance = Mathf.Sqrt((diffX * diffX) + (diffZ * diffZ));
        return distance;
    }
}
