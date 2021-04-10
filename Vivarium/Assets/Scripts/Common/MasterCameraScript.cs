﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        ResetCamera();
        //rayCastPivot();
        isCameraLock = false;
        focusCharacters = GameObject.FindGameObjectsWithTag("PlayerCharacter");
    }


    void Update()
    {

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

    public void refreshFocusCharacters()
    {
        focusCharacters = GameObject.FindGameObjectsWithTag("PlayerCharacter");
    }

    public void checkFocusOnExistingCharacter(KeyCode keycode, int characterIndex)
    {
        if (Input.GetKeyDown(keycode))
        {
            if (characterIndex >= 0 && characterIndex < focusCharacters.Length)
            {
                ChangeFocus(focusCharacters[characterIndex]);
            }
        }
    }



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


    private GameObject pivotTile;

    public void rayCastPivot()
    {
        int layer_mask = LayerMask.GetMask("FocusPlane");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layer_mask))
        {

            if (!(GameObject.ReferenceEquals(pivotTile, hit.transform.gameObject)))
            {
                this.transform.position = new Vector3(hit.transform.position.x, this.transform.position.y, hit.transform.position.z);
                pivotTile = hit.transform.gameObject;
            }
        }
    }


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

    public void RemoveFocus()
    {
        this.gameObject.transform.parent = null;
    }

    public void ResetZoom()
    {
        CameraZoomer.GetComponent<CameraZoomer>().setZoom(resetZoomOut);
    }


    public void saveCameraPosition()
    {
        //var newTransform = new Transform();
        previousMasterCameraPosition = this.transform.position;
        previousMasterCameraRotation = this.transform.rotation;
        previousZoomPosition = CameraZoomer.transform.position;
        previousZoomRotation = CameraZoomer.transform.rotation;
    }

    public void loadCameraPosition()
    {
        this.transform.position = previousMasterCameraPosition;
        //this.transform.rotation = previousMasterCameraPosition;

        CameraZoomer.transform.position = previousZoomPosition;
        //CameraZoomer.transform.rotation = previousZoomPosition;
    }


    public void EnterCameraFocusCommand(GameObject Character)
    {
        CommandController.Instance.ExecuteCommand(
        new WaitCommand()
        );
        CommandController.Instance.ExecuteCommand(
        new MoveCameraCommand(Character.transform.position, PanSpeed, Character)
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
        //loadCameraPosition();
        //CommandController.Instance.ExecuteCommand(
        //new MoveCameraCommand(new Vector3(0, 0, 0), PanSpeed)
        //);
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


    public void lockCamera()
    {
        isCameraLock = true;
        //CameraMover.GetComponent<Camera_Mover>().lockCameraMover();
        CameraZoomer.GetComponent<CameraZoomer>().lockCameraZoom();
        this.gameObject.GetComponent<CameraRotater>().lockCameraRotater();
    }


    public void unlockCamera()
    {
        isCameraLock = false;
        //CameraMover.GetComponent<Camera_Mover>().unlockCameraMover();
        CameraZoomer.GetComponent<CameraZoomer>().unlockCameraZoom();
        this.gameObject.GetComponent<CameraRotater>().unlockCameraRotater();
    }


    public GameObject GetCameraMover()
    {
        return CameraMover;
    }


    public GameObject GetCameraZoomer()
    {
        return CameraZoomer;
    }


}
