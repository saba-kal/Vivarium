using System.Collections;
using UnityEngine;

/// <summary>
/// Command load the camera's save state
/// </summary>
public class LoadCameraStateCommand : ICommand
{
    /// <summary>
    /// Command to load the camera's save state
    /// </summary>
    public LoadCameraStateCommand()
    {

    }

    public IEnumerator Execute()
    {
        var mainCamera = GameObject.FindGameObjectsWithTag("MasterCamera")[0];
        mainCamera.GetComponent<MasterCameraScript>().loadCameraState();

        yield return null;
    }

}
