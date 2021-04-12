using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Command to reset the camera
/// </summary>
public class ResetCameraCommand : ICommand
{
    GameObject _mainCamera;
    /// <summary>
    /// Command to reset the camera
    /// <summary>
    public ResetCameraCommand(
    GameObject mainCamera
    )
    {
        _mainCamera = mainCamera;
    }

    /// <summary>
    /// Resets camera to default position
    /// </summary>
    public IEnumerator Execute()
    {
        _mainCamera.GetComponent<MasterCameraScript>().ResetCamera();
        yield return null;
    }
}
