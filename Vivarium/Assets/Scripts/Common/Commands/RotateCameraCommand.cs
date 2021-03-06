﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Command to rotate the camera
/// </summary>
public class RotateCameraCommand : ICommand
{
    private Quaternion _mainCameraRotation;
    private Quaternion _zoomCameraRotation;
    private GameObject _mainCamera;

    /// <summary>
    /// Command to rotate the camera
    /// </summary>
    /// <param name="mainCameraRotation">Rotation for the main camera</param>
    /// <param name="zoomCameraRotation">Rotation for Zoom container</param>
    public RotateCameraCommand(
        Quaternion mainCameraRotation,
        Quaternion zoomCameraRotation
        )
    {
        _mainCameraRotation = mainCameraRotation;
        _zoomCameraRotation = zoomCameraRotation;
    }

    public IEnumerator Execute()
    {
        _mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];
        _mainCamera.transform.rotation = _mainCameraRotation;

        GameObject zoomCamera = _mainCamera.GetComponent<MasterCameraScript>().GetCameraZoomer();
        zoomCamera.transform.rotation = _zoomCameraRotation;
        yield return null;

    }

}
