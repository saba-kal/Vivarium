using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private bool isCameraLock;

    public GameObject Camera_Mover;
    public GameObject Camera;
    public GameObject HighlightDisc;

    public float defaultZoom;
    public float resetZoom;
    public float gameTilt;
    public float ExtraCameraFollowSpeed;

    public GameObject[] focusCharacters;
    // Start is called before the first frame update
    void Start()
    {
        ResetCamera();
        isCameraLock = false;
        focusCharacters = GameObject.FindGameObjectsWithTag("PlayerCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCameraLock)
        {
            // check if something is in the queue, and run if there is

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeFocus(focusCharacters[0]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeFocus(focusCharacters[1]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeFocus(focusCharacters[2]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeFocus(focusCharacters[3]);
            }
            //if (Input.GetKeyDown(KeyCode.Alpha5))
            //{
            //    ChangeFocus(focusCharacters[4]);
            //}
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ResetCamera();
            }
        }
    }
    public void ResetCamera()
    {
        this.gameObject.transform.parent = null;
        this.transform.localPosition = new Vector3(0, 0, 0);
        Camera_Mover.transform.localPosition = new Vector3(0, 0, -14);
        Camera.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.rotation = Quaternion.identity;
        float tiltAroundX = -gameTilt;
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);
        Camera.transform.rotation = target;
        ResetZoom();

        TurnSystemManager.Instance?.PlayerController.DeselectCharacter();
    }

    public void ResetZoom()
    {
        Camera.GetComponent<CameraController>().setZoom(resetZoom);
    }

    public void EnterCameraFocusCommand(GameObject Character)
    {
        CommandController.Instance.ExecuteCommand(
        new WaitCommand()
        );
        CommandController.Instance.ExecuteCommand(
        new MoveCameraCommand(Character.transform.position, ExtraCameraFollowSpeed, resetZoom, Character)
        );
        CommandController.Instance.ExecuteCommand(
        new WaitCommand()
        );
    }

    public void CameraMoveToReset()
    {
        this.gameObject.transform.parent = null;
        CommandController.Instance.ExecuteCommand(
        new WaitCommand()
        );
        CommandController.Instance.ExecuteCommand(
        new MoveCameraCommand(new Vector3(0, 0, -14 + Constants.CAMERA_FOLLOW_SKEW), ExtraCameraFollowSpeed, 1)
        );
        CommandController.Instance.ExecuteCommand(
        new UnlockCameraCommand()
        );
    }

    public void ChangeFocus(GameObject character)
    {
        if (character != null) // for when character dies
        {
            this.gameObject.transform.SetParent(character.transform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            Camera_Mover.transform.localPosition = new Vector3(0, 0, -4);
            Camera.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.rotation = Quaternion.identity;
            Camera.GetComponent<CameraController>().setZoom(defaultZoom);

            var characterController = character.GetComponent<CharacterController>();
            if (characterController != null)
            {
                TurnSystemManager.Instance?.PlayerController.SelectCharacter(characterController);
            }
        }
    }

    public void lockCamera()
    {
        isCameraLock = true;
        Camera_Mover.GetComponent<Camera_Mover>().lockCameraMover();
        Camera.GetComponent<CameraController>().lockCameraZoom();
        this.gameObject.GetComponent<CameraRotater>().lockCameraRotater();
    }

    public void unlockCamera()
    {
        isCameraLock = false;
        Camera_Mover.GetComponent<Camera_Mover>().unlockCameraMover();
        Camera.GetComponent<CameraController>().unlockCameraZoom();
        this.gameObject.GetComponent<CameraRotater>().unlockCameraRotater();
    }

    public GameObject GetCameraMover()
    {
        return Camera_Mover;
    }

    public GameObject GetCamera()
    {
        return Camera;
    }

    public void HighlightDiscOn()
    {
        //HighlightDisc.GetComponent<Renderer>().enabled = true;
    }

    public void HighlightDiscOff()
    {
        //HighlightDisc.GetComponent<Renderer>().enabled = false;
    }


}
