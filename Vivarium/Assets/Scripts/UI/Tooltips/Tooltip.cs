using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Displays hover text for UI elements
/// </summary>
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TooltipViewPrefab;
    public string TextToShow;
    public TooltipType Type;

    private GameObject _activeTooltip;
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

        switch (Type)
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
            case TooltipType.Text:
                tooltipView.DisplayGenericText(TextToShow);
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

    /// <summary>
    /// Hides tool tip popup
    /// </summary>
    public void HideTooltip()
    {
        _activeTooltip = null;
        _mouseIsHoveringOverElement = false;
    }

    /// <summary>
    /// Defines the item for the tool tip to describe
    /// </summary>
    /// <param name="item">The item that the tool tip will describe</param>
    public void SetTooltipData(Item item)
    {
        Type = TooltipType.Item;
        ItemToShow = item;
    }

    /// <summary>
    /// Defines the action the tool tip will describe
    /// </summary>
    /// <param name="action">The action that the tool tip will describe</param>
    public void SetTooltipData(Action action)
    {
        Type = TooltipType.Action;
        ActionToShow = action;
    }

    /// <summary>
    /// Defines the character the tool tip will describe
    /// </summary>
    /// <param name="character">The character the tool tip will describe</param>
    public void SetTooltipData(Character character)
    {
        Type = TooltipType.Character;
        CharacterToShow = character;
    }

    public void SetTooltipData(string text)
    {
        Type = TooltipType.Text;
        TextToShow = text;
    }

    /// <summary>
    /// Detects if the mouse is pointing at a UI element
    /// </summary>
    /// <param name="eventData">The mouse point event</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIsHoveringOverElement = true;
    }

    /// <summary>
    /// Detects if the mouse is no longer pointing at a UI element
    /// </summary>
    /// <param name="eventData">the mouse point event</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIsHoveringOverElement = false;
    }

    /// <summary>
    /// Sees if the tool tip can display the data
    /// </summary>
    /// <returns>Returns a bool depending on if the tool tip can display data</returns>
    public bool CanDisplayData()
    {
        return (ItemToShow != null || ActionToShow != null || !string.IsNullOrWhiteSpace(TextToShow)) &&
            _mouseIsHoveringOverElement;
    }

    /// <summary>
    /// Defines the gameobject the tool tip UI will appear on
    /// </summary>
    /// <param name="activeTooltip"></param>
    public void SetActive(GameObject activeTooltip)
    {
        _activeTooltip = activeTooltip;
    }
}