using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraCommand : ICommand
{
    public IEnumerator Execute()
    {
        var _mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        _mainCamera.GetComponent<CameraFollower>().lockCamera();
        yield return null;
    }
}
