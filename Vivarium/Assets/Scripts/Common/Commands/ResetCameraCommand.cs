using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraCommand : ICommand
{
    GameObject _mainCamera;
    public ResetCameraCommand(
    GameObject mainCamera
    )
    {
        _mainCamera = mainCamera;
    }
    public IEnumerator Execute()
    {
        _mainCamera.GetComponent<MasterCameraScript>().ResetCamera();
        yield return null;
    }
}
