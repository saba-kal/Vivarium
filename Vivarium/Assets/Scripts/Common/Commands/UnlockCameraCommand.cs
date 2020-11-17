using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCameraCommand : ICommand
{
    public IEnumerator Execute()
    {
        var _mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];
        _mainCamera.GetComponent<CameraFollower>().unlockCamera();
        _mainCamera.GetComponent<CameraFollower>().HighlightDiscOff();
        yield return null;
    }
}
