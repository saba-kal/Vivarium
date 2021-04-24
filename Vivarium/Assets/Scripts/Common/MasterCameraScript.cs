using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all the camera controls
/// </summary>
public class MasterCameraScript : MonoBehaviour
{
    private bool isCameraLock;

    public GameObject CameraMover;
    public GameObject CameraZoomer;

    public float focusZoomOut;
    public float resetZoomOut = 10;
    public float gameTilt;
    public float PanSpeed;

    public float moveSpeed;

    public GameObject[] focusCharacters;

    public Vector3 previousZoomPosition;
    public Quaternion previousZoomRotation;
    public Vector3 previousMasterCameraPosition;
    public Quaternion previousMasterCameraRotation;
    public Transform testTransform;

    public float maxForward;
    public float maxBackward;
    public float maxLeft;
    public float maxRight;

    void Start()
    {
        ResetCamera();
        //rayCastPivot();
        isCameraLock = false;
        focusCharacters = GameObject.FindGameObjectsWithTag("PlayerCharacter");
    }


    void Update()
    {
        if (this.gameObject.transform.position.z > maxForward)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, maxForward);
        }
        if (this.gameObject.transform.position.z < maxBackward)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, maxBackward);
        }
        if (this.gameObject.transform.position.x > maxRight)
        {
            this.gameObject.transform.position = new Vector3(maxRight, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        if (this.gameObject.transform.position.x < maxLeft)
        {
            this.gameObject.transform.position = new Vector3(maxLeft, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }


        if (!isCameraLock)
        {
            checkMoveCamera();
            checkFocusOnExistingCharacter(KeyCode.Alpha1, 0);
            checkFocusOnExistingCharacter(KeyCode.Alpha2, 1);
            checkFocusOnExistingCharacter(KeyCode.Alpha3, 2);
            checkFocusOnExistingCharacter(KeyCode.Alpha4, 3);
            checkFocusOnExistingCharacter(KeyCode.Alpha5, 4);
            checkFocusOnExistingCharacter(KeyCode.Alpha6, 5);
            checkFocusOnExistingCharacter(KeyCode.Alpha7, 6);
            checkFocusOnExistingCharacter(KeyCode.Alpha8, 7);
            checkFocusOnExistingCharacter(KeyCode.Alpha9, 8);

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ResetCamera();
            }
        }
    }

    /// <summary>
    /// Refreshes the list of player characters the camera can focus on
    /// </summary>
    public void refreshFocusCharacters()
    {
        focusCharacters = GameObject.FindGameObjectsWithTag("PlayerCharacter");
    }


    private void checkFocusOnExistingCharacter(KeyCode keycode, int characterIndex)
    {
        if (Input.GetKeyDown(keycode))
        {
            if (characterIndex >= 0 && characterIndex < focusCharacters.Length)
            {
                ChangeFocus(focusCharacters[characterIndex]);
            }
        }
    }


    /// <summary>
    /// Moves the camera based on the WASD input and the height of the camera. 
    /// </summary>
    public void checkMoveCamera()
    {
        var zoomPercent = CameraZoomer.GetComponent<CameraZoomer>().getCurrentZoomPercent();
        var panSpeed = moveSpeed * zoomPercent;
        //CameraMover.GetComponent<Camera_Mover>().CheckMoveCamera();
        if (!isCameraLock)
        {
            if (Input.GetKey(KeyCode.W))
            {

                this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime * zoomPercent;
                //rayCastPivot();

            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.position -= this.transform.forward * moveSpeed * Time.deltaTime * zoomPercent;
                //rayCastPivot();

            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position -= this.transform.right * moveSpeed * Time.deltaTime * zoomPercent;
                //rayCastPivot(); 

            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += this.transform.right * moveSpeed * Time.deltaTime * zoomPercent;
                //rayCastPivot();

            }
        }

    }


    /// <summary>
    /// Resets the camera to the default position and rotation
    /// </summary>
    public void ResetCamera()
    {
        RemoveFocus();
        this.transform.localPosition = new Vector3(0, 0, 0);
        CameraMover.transform.localPosition = new Vector3(0, 0, 0);
        CameraZoomer.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.rotation = Quaternion.identity;
        float tiltAroundX = gameTilt;
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);
        CameraZoomer.transform.rotation = target;
        ResetZoom();

        TurnSystemManager.Instance?.PlayerController.DeselectCharacter();
    }

    /// <summary>
    /// Unparents the focus gameobject from the camera
    /// </summary>
    public void RemoveFocus()
    {
        this.gameObject.transform.parent = null;
    }

    /// <summary>
    /// Resets the Camera zoom to the default zoom
    /// </summary>
    public void ResetZoom()
    {
        CameraZoomer.GetComponent<CameraZoomer>().setZoom(resetZoomOut);
    }

    /// <summary>
    /// saves the camera position
    /// </summary>
    public void saveCameraPosition()
    {
        //var newTransform = new Transform();
        previousMasterCameraPosition = this.transform.position;
        previousMasterCameraRotation = this.transform.rotation;
        previousZoomPosition = CameraZoomer.transform.position;
        previousZoomRotation = CameraZoomer.transform.rotation;
    }

    /// <summary>
    /// loads the saved camera position
    /// </summary>
    public void loadCameraPosition()
    {
        this.transform.position = previousMasterCameraPosition;

        CameraZoomer.transform.position = previousZoomPosition;
    }

    /// <summary>
    /// Adds the command to focus the camera on a character to the execution queue
    /// </summary>
    /// <param name="Character">The gameobject the camera will focus on during the command execution</param>
    public void EnterCameraFocusCommand(GameObject Character)
    {
        CommandController.Instance.ExecuteCommand(
        new WaitCommand(0.3f)
        );
        CommandController.Instance.ExecuteCommand(
        new MoveCameraCommand(Character.transform.position, PanSpeed, Character)
        );
        CommandController.Instance.ExecuteCommand(
        new WaitCommand(0.3f)
        );
    }


    /// <summary>
    /// Adds the command to reset the camera to the execution queue.
    /// </summary>
    public void CameraMoveToReset()
    {
        this.gameObject.transform.parent = null;
        CommandController.Instance.ExecuteCommand(
        new WaitCommand(0.3f)
        );

        CommandController.Instance.ExecuteCommand(
        new MoveCameraCommand(previousMasterCameraPosition, PanSpeed)
        );

        CommandController.Instance.ExecuteCommand(
            new RotateCameraCommand(previousMasterCameraRotation, previousZoomRotation)
        );
        CommandController.Instance.ExecuteCommand(
        new UnlockCameraCommand()
        );
    }


    /// <summary>
    /// Changes the focus of the camera to a game object
    /// </summary>
    /// <param name="character">The gameobject to be focused on</param>
    public void ChangeFocus(GameObject character)
    {
        if (character != null) // for when character dies
        {
            this.gameObject.transform.SetParent(character.transform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            CameraMover.transform.localPosition = new Vector3(0, 0, 0);
            CameraZoomer.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.rotation = Quaternion.identity;
            CameraZoomer.GetComponent<CameraZoomer>().setZoom(focusZoomOut);

            var characterController = character.GetComponent<CharacterController>();
            if (characterController != null)
            {
                TurnSystemManager.Instance?.PlayerController.SelectCharacter(characterController);
            }

            this.gameObject.transform.parent = null;
        }
    }


    /// <summary>
    /// Prevents the Camera from being affected by player input
    /// </summary>
    public void lockCamera()
    {
        isCameraLock = true;
        //CameraMover.GetComponent<Camera_Mover>().lockCameraMover();
        CameraZoomer.GetComponent<CameraZoomer>().lockCameraZoom();
        this.gameObject.GetComponent<CameraRotater>().lockCameraRotater();
    }


    /// <summary>
    /// Allows the Camera to be affected by player input
    /// </summary>
    public void unlockCamera()
    {
        isCameraLock = false;
        //CameraMover.GetComponent<Camera_Mover>().unlockCameraMover();
        CameraZoomer.GetComponent<CameraZoomer>().unlockCameraZoom();
        this.gameObject.GetComponent<CameraRotater>().unlockCameraRotater();
    }


    /// <summary>
    /// Returns the Camera Mover Object
    /// </summary>
    public GameObject GetCameraMover()
    {
        return CameraMover;
    }


    /// <summary>
    /// Returns the Camera Zoomer Object
    /// </summary>
    public GameObject GetCameraZoomer()
    {
        return CameraZoomer;
    }


}
