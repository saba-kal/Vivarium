using System.Collections;
using UnityEngine;

public class RotateCameraCommand : ICommand
{
    private Quaternion _mainCameraRotation;
    private Quaternion _zoomCameraRotation;
    private GameObject _mainCamera;


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
