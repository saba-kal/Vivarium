using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickReward : MonoBehaviour, IPointerClickHandler
{
    public RewardsUIController RewardsUIController;
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
