using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slowly rotates object over time about an axis.
/// </summary>
public class RotateOverTime : MonoBehaviour
{
    public Vector3 RotationAxis = new Vector3(0, 0, 1);
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        var axis = RotationAxis.normalized;
        transform.Rotate(axis * RotationSpeed * Time.deltaTime);
    }
}
