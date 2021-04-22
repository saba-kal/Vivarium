using UnityEngine;
using System.Collections;

/// <summary>
/// Holds the tool tip. Attaches to game object.
/// </summary>
public class TooltipContainer : MonoBehaviour
{
    public static TooltipContainer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
