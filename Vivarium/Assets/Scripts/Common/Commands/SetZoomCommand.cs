using System.Collections;
using UnityEngine;

/// <summary>
/// Command to set the Zoom of the camera
/// </summary>
public class SetZoomCommand : ICommand
{
    private float _zoom;
    /// <summary>
    /// Command to set the Zoom of the camera
    /// </summary>
    public SetZoomCommand(
        float zoom)
    {
        _zoom = zoom;
    }

    public IEnumerator Execute()
    {
        var _mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];
        GameObject zoomCamera = _mainCamera.GetComponent<MasterCameraScript>().GetCameraZoomer();
        zoomCamera.transform.localPosition = new Vector3(0, 0, 0);
        zoomCamera.GetComponent<CameraZoomer>().setZoom(_zoom);
        yield return null;

    }

}
