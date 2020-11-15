using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    //public float speed;
    public float rotateSpeed;
    private bool isCameraLock;

    private void Start()
    {
        isCameraLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCameraLock)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            }
        }
    }

    public void lockCameraRotater()
    {
        isCameraLock = true;
    }

    public void unlockCameraRotater()
    {
        isCameraLock = false;
    }
}
