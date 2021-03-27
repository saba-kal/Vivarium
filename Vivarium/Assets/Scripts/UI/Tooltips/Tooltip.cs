using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TooltipViewPrefab;
    public float TooltipCleanupInterval = 0.3f;

    private GameObject _activeTooltip;
    private TooltipType _type;
    private Item ItemToShow;
    private Action ActionToShow;
    private Character CharacterToShow;
    private bool _mouseIsHoveringOverElement = false;
    private Canvas _canvas;
    private float _timeSinceLastCleanup = 0f;

    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

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

        _timeSinceLastCleanup += Time.deltaTime;
        if (_timeSinceLastCleanup > TooltipCleanupInterval)
        {
            CleanupOrphanTooltips();
            _timeSinceLastCleanup = 0;
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

    private void CleanupOrphanTooltips()
    {
        //TODO: figure out how to detect orphan tooltips. This does not work currently.
        return;

        Debug.Log(_activeTooltip?.GetInstanceID());

        var tooltipView = _activeTooltip?.GetComponentInChildren<TooltipView>();

        foreach (Transform child in TooltipContainer.Instance.transform)
        {
            if (tooltipView != null)
            {
                var childTooltipView = child.GetComponentInChildren<TooltipView>();
                if (childTooltipView != null && tooltipView.Id != childTooltipView.Id)
                {
                    Destroy(child.gameObject);
                }
                //Debug.Log(child.name);
                //Destroy(child.gameObject);
            }
        }
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
}