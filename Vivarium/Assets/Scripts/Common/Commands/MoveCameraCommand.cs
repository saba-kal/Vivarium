using System.Collections;
using UnityEngine;

public class MoveCameraCommand : ICommand
{
    private float _panSpeed;
    private Vector3 _currentLocation;
    private Vector3 _destination;
    private GameObject _mainCamera;
    private GameObject _focusCharacter;
    private GameObject _cameraMover;
    private GameObject _camera;
    private System.Action _onMoveComplete;


    public MoveCameraCommand(
        Vector3 destination,
        float panSpeed,
        GameObject focusCharacter = null,
        System.Action onMoveComplete = null
        )
    {
        _panSpeed = panSpeed;
        _focusCharacter = focusCharacter;
        _destination = destination;
        _onMoveComplete = onMoveComplete;
    }

    public IEnumerator Execute()
    {
        _mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];
        _currentLocation = _mainCamera.transform.position;
        _cameraMover = _mainCamera.GetComponent<MasterCameraScript>().GetCameraMover();
        //_camera = _mainCamera.GetComponent<MasterCameraScript>().GetCameraZoomer();

        //_mainCamera.GetComponent<MasterCameraScript>().ResetZoom();

        _mainCamera.transform.rotation = Quaternion.identity;
        _mainCamera.GetComponent<MasterCameraScript>().lockCamera();
        //var centerOffset = Constants.CAMERA_FOLLOW_SKEW;
        var centerOffset = 0;
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

        while (arrived == false)
        {
            var step = _panSpeed * Time.deltaTime;
            _cameraMover.transform.position = Vector3.MoveTowards(_cameraMover.transform.position, new Vector3(destinationX, destinationY, destinationZ), step);
            var remainingDistance = calculateDistance(_destination, _cameraMover.transform.position);
            if (remainingDistance <= 0.01f)
            {
                arrived = true;
            }
            yield return null;
        }

        if (_focusCharacter != null)
        {
            _mainCamera.transform.SetParent(_focusCharacter.transform);
        }
        else
        {
            //_mainCamera.GetComponent<MasterCameraScript>().ResetCamera();
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
