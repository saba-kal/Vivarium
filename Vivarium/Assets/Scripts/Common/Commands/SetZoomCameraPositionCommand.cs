using System.Collections;
using UnityEngine;

/// <summary>
/// Command to set the position of the Camera Zoomer
/// </summary>
public class SetZoomCameraPositionCommand : ICommand
{
    private Vector3 _position;
    private GameObject _mainCamera;

    /// <summary>
    /// Command to set the position of the Camera Zoomer
    /// </summary>
    /// <param name="zoom">Rotation for Zoom container</param>
    public SetZoomCameraPositionCommand(Vector3 position)
    {
        _position = position;
    }

    public IEnumerator Execute()
    {
        float tiltAroundX = 50;
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);

        TurnSystemManager.Instance?.PlayerController.DeselectCharacter();



        _mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];

        var CameraZoomer = _mainCamera.GetComponent<MasterCameraScript>().GetCameraZoomer();
        //CameraZoomer.transform.localPosition = new Vector3(0, 0, 0);
        CameraZoomer.transform.localPosition = _position;

        yield return null;

    }

}
