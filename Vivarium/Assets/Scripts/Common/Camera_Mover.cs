using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Moving the Camera in a horizontal and vertical direction
/// </summary>
public class Camera_Mover : MonoBehaviour
{
    private bool isCameraLock;
    public float speed;

    private void Start()
    {
        isCameraLock = false;
    }

    /// <summary>
    /// Checks for a keyboard input and moves the camera mover accordingly
    /// </summary>
    public void CheckMoveCamera()
    {
        if (!isCameraLock)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * speed * Time.deltaTime;
                // position.z += PanSpeed * Time.deltaTime;

            }
        }
    }


    /// <summary>
    /// Locks the Camera Mover, prevents the Camera Mover from being affected by player input. 
    /// </summary>
    public void lockCameraMover()
    {
        isCameraLock = true;
    }

    /// <summary>
    /// Unlocks the Camera Mover, allows the Camera Mover to be affected by player input. 
    /// </summary>
    public void unlockCameraMover()
    {
        isCameraLock = false;
    }
}
