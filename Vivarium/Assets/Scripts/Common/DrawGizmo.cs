using UnityEngine;
using System.Collections;

public class DrawGizmo : MonoBehaviour
{
    public Color GizmoColor = Color.red;
    public float Size = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawSphere(transform.position, Size);
    }
}
