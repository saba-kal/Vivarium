using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// Handles tooltip logic for multiple UI elements.
/// </summary>
public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    public GameObject TooltipViewPrefab;

    private List<Tooltip> _tooltips = new List<Tooltip>();
    private GameObject _activeTooltip;

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

    void Start()
    {
        _activeTooltip = Instantiate(TooltipViewPrefab, TooltipContainer.Instance.transform);
        _activeTooltip.transform.localPosition = Vector3.zero;
        _activeTooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var activeTooltipExists = false;
        foreach (var tooltip in _tooltips)
        {
            if (tooltip.CanDisplayData())
            {
                tooltip.SetActive(_activeTooltip);
                activeTooltipExists = true;
                break;
            }
        }

        _activeTooltip.SetActive(activeTooltipExists);
    }

    /// <summary>
    /// Registers tooltip so that it can display info. Unregistered tooltips will be ignored.
    /// </summary>
    /// <param name="tooltip">The tooltip to register.</param>
    public void Register(Tooltip tooltip)
    {
        _tooltips.Add(tooltip);
    }

    /// <summary>
    /// Deregisters tooltip so that it no longer displays info.
    /// </summary>
    /// <param name="tooltip">The tooltip to deregister.</param>
    public void Deregister(Tooltip tooltip)
    {
        var newTooltipList = new List<Tooltip>();
        foreach (var existingTooltip in _tooltips)
        {
            if (existingTooltip != tooltip)
            {
                newTooltipList.Add(existingTooltip);
            }
        }

        _tooltips = newTooltipList;
    }
}
