using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TooltipViewPrefab;

    private GameObject _activeTooltip;
    private TooltipType _type;
    private Item ItemToShow;
    private Action ActionToShow;
    private Character CharacterToShow;
    private bool _mouseIsHoveringOverElement = false;
    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        TooltipManager.Instance.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanDisplayData() || _activeTooltip == null)
        {
            HideTooltip();
            return;
        }

        DisplayTooltipInfo();
        PositionTooltip();
    }

    void OnDestroy()
    {
        TooltipManager.Instance.Deregister(this);
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
            case TooltipType.Character:
                tooltipView.DisplayCharacter(CharacterToShow);
                break;
        }
    }

    private void PositionTooltip()
    {
        var mousePosition = Input.mousePosition;
        var tooltipRectTransform = _activeTooltip.GetComponent<RectTransform>();
        var width = tooltipRectTransform.rect.width * _canvas.scaleFactor;
        var height = tooltipRectTransform.rect.height * _canvas.scaleFactor;

        var xPosition = mousePosition.x + width / 2f;
        if (xPosition > Screen.width - width / 2f)
        {
            xPosition = mousePosition.x - width / 2f;
        }
        var yPosition = mousePosition.y - height / 2f;
        if (yPosition < height / 2f)
        {
            yPosition = mousePosition.y + height / 2f;
        }

        tooltipRectTransform.transform.position = new Vector2(xPosition, yPosition);
    }

    public void HideTooltip()
    {
        _activeTooltip = null;
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

    public void SetTooltipData(Character character)
    {
        _type = TooltipType.Character;
        CharacterToShow = character;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIsHoveringOverElement = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIsHoveringOverElement = false;
    }

    public bool CanDisplayData()
    {
        return (ItemToShow != null || ActionToShow != null) &&
            _mouseIsHoveringOverElement;
    }

    public void SetActive(GameObject activeTooltip)
    {
        _activeTooltip = activeTooltip;
    }
}