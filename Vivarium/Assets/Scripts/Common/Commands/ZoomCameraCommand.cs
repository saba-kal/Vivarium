using System.Collections;
using UnityEngine;

/// <summary>
/// Command to adjust the zoom of the camera
/// </summary>
public class ZoomCameraCommand : ICommand
{
    private float _zoom;
    private GameObject _mainCamera;

    /// <summary>
    /// Command to adjust the the zoom of the camera
    /// </summary>
    /// <param name="zoom">Rotation for Zoom container</param>
    public ZoomCameraCommand(float zoom)
    {
        _zoom = zoom;
    }

    public IEnumerator Execute()
    {
        float tiltAroundX = 50;
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);

        TurnSystemManager.Instance?.PlayerController.DeselectCharacter();



        _mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];
        _mainCamera.GetComponent<MasterCameraScript>().ResetZoom();

        var CameraZoomer = _mainCamera.GetComponent<MasterCameraScript>().GetCameraZoomer();
        CameraZoomer.transform.localPosition = new Vector3(0, 0, 0);
        CameraZoomer.GetComponent<CameraZoomer>().setZoom(_zoom);

        //_mainCamera.transform.rotation = _mainCameraRotation;

        //GameObject zoomCamera = _mainCamera.GetComponent<MasterCameraScript>().GetCameraZoomer();
        //zoomCamera.transform.rotation = target;
        //zoomCamera.transform.localPosition = new Vector3(0, 0, 0);
        yield return null;

    }

}
