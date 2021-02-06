using UnityEngine;
using System.Collections;

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
