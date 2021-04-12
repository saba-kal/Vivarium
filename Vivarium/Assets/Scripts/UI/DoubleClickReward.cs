using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles different click amounts on the rewards screen.
/// </summary>
public class DoubleClickReward : MonoBehaviour, IPointerClickHandler
{
    public RewardsUIController RewardsUIController;

    /// <summary>
    /// Checks if a reward was clicked once, twice or multiple times.
    /// </summary>
    /// <param name="eventData">Used for finding out how many clicks occur.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (clickCount == 1)
        {
            OnSingleClick();
        }
        else if (clickCount == 2)
        {
            OnDoubleClick();
        }
        else if (clickCount > 2)
        {
            OnMultiClick();
        }

    }

    void OnSingleClick()
    {
        Debug.Log("Single Click");
    }

    void OnDoubleClick()
    {
        RewardsUIController.DoubleClicked();
    }

    void OnMultiClick()
    {
        Debug.Log("Multiple Clicks");
    }
}
