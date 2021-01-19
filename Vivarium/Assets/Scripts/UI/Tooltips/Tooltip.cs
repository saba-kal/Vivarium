using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour
{
    public GameObject TooltipViewPrefab;

    private GameObject _activeTooltip;
    private TooltipType _type;
    private Item ItemToShow;
    private Action ActionToShow;

    // Update is called once per frame
    void Update()
    {
        if (ItemToShow == null && ActionToShow == null)
        {
            return;
        }

        var mouseIsHoveringOverElement = IsMouseHoveringOverElement();
        if (_activeTooltip == null && mouseIsHoveringOverElement)
        {
            _activeTooltip = Instantiate(TooltipViewPrefab, TooltipContainer.Instance.transform);
            _activeTooltip.transform.localPosition = Vector3.zero;
            DisplayTooltipInfo();
        }
        else if (_activeTooltip != null && !mouseIsHoveringOverElement)
        {
            Destroy(_activeTooltip);
        }

        if (_activeTooltip != null)
        {
            PositionTooltip();
        }
    }

    private bool IsMouseHoveringOverElement()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private void DisplayTooltipInfo()
    {
        switch (_type)
        {
            case TooltipType.Action:
                Debug.LogWarning("Action tooltips not implemented");
                break;
            case TooltipType.Item:
                var itemTooltipView = _activeTooltip.GetComponentInChildren<TooltipItemView>();
                if (itemTooltipView != null)
                {
                    itemTooltipView.DisplayItem(ItemToShow);
                }
                break;
        }
    }

    private void PositionTooltip()
    {
        var mousePosition = Input.mousePosition;
        var tooltipRectTransform = _activeTooltip.GetComponent<RectTransform>().rect;
        var xPosition = mousePosition.x + tooltipRectTransform.width / 2f;
        if (xPosition > Screen.width - tooltipRectTransform.width / 2f)
        {
            xPosition = mousePosition.x - tooltipRectTransform.width / 2f;
        }
        var yPosition = mousePosition.y - tooltipRectTransform.height / 2f;
        if (yPosition < tooltipRectTransform.height / 2f)
        {
            yPosition = mousePosition.y + tooltipRectTransform.height / 2f;
        }
        _activeTooltip.transform.position = new Vector3(xPosition, yPosition, 1f);
    }

    public void SetTooltipData(Item item)
    {
        _type = TooltipType.Item;
        ItemToShow = item;
    }

    public void SetTooltipData(Action action)
    {
        _type = TooltipType.Action;
        ActionToShow = action;
    }
}