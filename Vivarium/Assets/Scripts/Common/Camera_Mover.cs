using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mover : MonoBehaviour
{
    private bool isCameraLock;
    public float speed;

    private void Start()
    {
        isCameraLock = false;
    }

    // Update is called once per frame
    void Update()
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

    public void lockCameraMover()
    {
        isCameraLock = true;
    }

    public void unlockCameraMover()
    {
        isCameraLock = false;
    }
}
