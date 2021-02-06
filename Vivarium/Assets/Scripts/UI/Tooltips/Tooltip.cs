using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TooltipViewPrefab;

    private GameObject _activeTooltip;
    private TooltipType _type;
    private Item ItemToShow;
    private Action ActionToShow;
    private bool _mouseIsHoveringOverElement = false;

    // Update is called once per frame
    void Update()
    {
        if (ItemToShow == null && ActionToShow == null)
        {
            return;
        }

        if (_activeTooltip == null && _mouseIsHoveringOverElement)
        {
            _activeTooltip = Instantiate(TooltipViewPrefab, TooltipContainer.Instance.transform);
            _activeTooltip.transform.localPosition = Vector3.zero;
            DisplayTooltipInfo();
        }
        else if (_activeTooltip != null && !_mouseIsHoveringOverElement)
        {
            HideTooltip();
        }

        if (_activeTooltip != null)
        {
            PositionTooltip();
        }
    }

    private void DisplayTooltipInfo()
    {
        var tooltipView = _activeTooltip.GetComponentInChildren<TooltipView>();
        if (tooltipView == null)
        {
            return;
        }

        switch (_type)
        {
            case TooltipType.Action:
                tooltipView.DisplayAction(ActionToShow);
                break;
            case TooltipType.Item:
                tooltipView.DisplayItem(ItemToShow);
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

    public void HideTooltip()
    {
        Destroy(_activeTooltip);
        _mouseIsHoveringOverElement = false;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIsHoveringOverElement = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIsHoveringOverElement = false;
    }
}