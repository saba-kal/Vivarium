using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCameraCommand : ICommand
{
    /// <summary>
    /// Command to unlock the camera
    /// </summary>
    public IEnumerator Execute()
    {
        var _mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];
        _mainCamera.GetComponent<MasterCameraScript>().unlockCamera();
        yield return null;
    }
}
