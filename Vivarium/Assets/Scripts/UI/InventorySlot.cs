using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void SlotClick(InventorySlot inventorySlot);
    public delegate void SlotDragBegin(InventorySlot inventorySlot);
    public delegate void SlotDragEnd(InventorySlot inventorySlot);
    public delegate void SlotDrag(InventorySlot inventorySlot);
    public delegate void SlotDrop(InventorySlot dropSlot, InventorySlot droppedSlot);
    public static event SlotClick OnSlotClick;
    public static event SlotDrag OnSlotDrag;

    public Image Icon;
    public Button Button;
    public TextMeshProUGUI Count;
    public GameObject EquipOverlay;
    public GameObject SlotHighlight;
    public int Index;

    private InventoryItem _inventoryItem;
    private CharacterController _selectedCharacter;
    private SlotClick _onSlotClick;
    private SlotDragBegin _onSlotDragBegin;
    private SlotDragEnd _onSlotDragEnd;
    private SlotDrop _onSlotDrop;
    private Canvas _canvas;
    private GameObject _duplicateIcon;
    private bool _highlightOccupiedSlots = false;
    private bool _highlightEnabled = true;

    private void Start()
    {
        Button.onClick.AddListener(() =>
        {
            _onSlotClick?.Invoke(this); //Scoped to single inventory slot.
            OnSlotClick?.Invoke(this); //Global
        });
        _canvas = GameObject.FindGameObjectWithTag(Constants.CANVAS_TAG)?.GetComponent<Canvas>();
        SlotHighlight.SetActive(false);
    }

    public void SetItem(InventoryItem inventoryItem, CharacterController selectedCharacter = null)
    {
        _inventoryItem = inventoryItem;
        _selectedCharacter = selectedCharacter;
        if (_duplicateIcon != null)
        {
            Destroy(_duplicateIcon);
        }
        UpdateItemDisplay();
        SetTooltip();
    }

    /// <summary>
    /// Sets callback for when an inventory slot is clicked.
    /// </summary>
    /// <param name="onClick">The callback to set.</param>
    public void SetOnClickCallback(SlotClick onClick)
    {
        _onSlotClick = onClick;
    }

    /// <summary>
    /// Sets callback for when dragging of an inventory slot begins.
    /// </summary>
    /// <param name="dragBegin">The callback to set.</param>
    public void SetOnDragBeginCallback(SlotDragBegin dragBegin)
    {
        _onSlotDragBegin = dragBegin;
    }

    /// <summary>
    /// Sets callback for when dragging of an inventory slot ends.
    /// </summary>
    /// <param name="dragEnd">The callback to set.</param>
    public void SetOnDragEndCallback(SlotDragEnd dragEnd)
    {
        _onSlotDragEnd = dragEnd;
    }

    /// <summary>
    /// Sets callback for when an inventory slot drops on another inventory slot.
    /// </summary>
    /// <param name="slotDrop">The callback to set.</param>
    public void SetOnDropCallback(SlotDrop slotDrop)
    {
        _onSlotDrop = slotDrop;
    }

    /// <summary>
    /// Gets the inventory item associated with this slot.
    /// </summary>
    /// <returns>The <see cref="InventoryItem"/> associated with this slot.</returns>
    public InventoryItem GetItem()
    {
        return _inventoryItem;
    }

    /// <summary>
    /// Updates the display of this slot.
    /// </summary>
    public void UpdateItemDisplay()
    {
        if (_inventoryItem == null)
        {
            Clear();
            return;
        }

        if (_selectedCharacter != null && InventoryManager.GetCharacterItem(_selectedCharacter.Id, _inventoryItem.Item.Id, _inventoryItem.InventoryPosition) == null ||
            _selectedCharacter == null && InventoryManager.GetPlayerItem(_inventoryItem.Item.Id, _inventoryItem.InventoryPosition) == null)
        {
            Clear();
            return;
        }

        Icon.sprite = _inventoryItem.Item.Icon;
        Icon.gameObject.SetActive(true);
        Button.interactable = true;
        Icon.gameObject.SetActive(true);
        Count.text = _inventoryItem.Count.ToString();

        _duplicateIcon = Instantiate(Icon.gameObject, transform);
        _duplicateIcon.transform.SetSiblingIndex(Icon.transform.GetSiblingIndex());
    }

    public void DisplayEquipOverlay()
    {
        EquipOverlay.SetActive(true);
    }

    public void HideEquipOverlay()
    {
        EquipOverlay.SetActive(false);
    }

    public void Clear()
    {
        _inventoryItem = null;
        Icon.sprite = null;
        Icon.gameObject.SetActive(false);
        Button.interactable = false;
        Icon.gameObject.SetActive(false);
        Count.text = "0";
        EquipOverlay.SetActive(false);
        if (_duplicateIcon != null)
        {
            Destroy(_duplicateIcon);
        }
    }

    /// <inheritdoc cref="IBeginDragHandler.OnBeginDrag(PointerEventData)"/>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_inventoryItem == null)
        {
            return;
        }

        _onSlotDragBegin?.Invoke(this);
        eventData.selectedObject = gameObject;

        if (_canvas != null)
        {
            Icon.transform.SetParent(_canvas.transform);

            var canvasGroup = Icon.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = false;

            if (!_inventoryItem.Item.CanBeStacked || _inventoryItem.Count < 2)
            {
                _duplicateIcon.SetActive(false);
            }
            Count.text = _inventoryItem.Count.ToString();
        }
        else
        {
            Debug.LogError("Error ordering UI component on drag because canvas object was null");
        }
    }

    /// <inheritdoc cref="IDragHandler.OnDrag(PointerEventData)"/>
    public void OnDrag(PointerEventData eventData)
    {
        if (_inventoryItem == null)
        {
            return;
        }

        Icon.transform.position = Input.mousePosition;
        OnSlotDrag?.Invoke(this);
        var tooltip = GetComponent<Tooltip>();
        if (tooltip != null)
        {
            tooltip.HideTooltip();
        }
    }

    /// <inheritdoc cref="IEndDragHandler.OnEndDrag(PointerEventData)"/>
    public void OnEndDrag(PointerEventData eventData)
    {
        ResetIcon(Icon, transform);
        _onSlotDragEnd?.Invoke(this);
    }

    /// <inheritdoc cref="IDropHandler.OnDrop(PointerEventData)"/>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.selectedObject != null)
        {
            var droppedSlot = eventData.selectedObject.GetComponent<InventorySlot>();

            ResetIcon(droppedSlot.Icon, droppedSlot.transform);
            _onSlotDrop?.Invoke(this, droppedSlot);
        }
    }

    private void ResetIcon(Image icon, Transform parent)
    {
        icon.transform.SetParent(parent);
        icon.transform.localPosition = Vector3.zero;

        var canvasGroup = icon.gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            Destroy(canvasGroup);
        }
    }

    /// <summary>
    /// Gets the character controller this inventory slot belongs to.
    /// </summary>
    /// <returns><see cref="CharacterController">.</returns>
    public CharacterController GetCharacter()
    {
        return _selectedCharacter;
    }

    private void SetTooltip()
    {
        var tooltip = GetComponent<Tooltip>();
        if (tooltip != null && _inventoryItem != null)
        {
            tooltip.SetTooltipData(_inventoryItem.Item);
        }
    }

    /// <inheritdoc cref="IPointerEnterHandler.OnPointerExit(PointerEventData)"/>
    public void OnPointerExit(PointerEventData eventData)
    {
        SlotHighlight.SetActive(false);
    }

    /// <inheritdoc cref="IPointerEnterHandler.OnPointerEnter(PointerEventData)"/>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_highlightEnabled)
        {
            return;
        }

        if (eventData.selectedObject != null)
        {
            if (_highlightOccupiedSlots)
            {
                SlotHighlight.SetActive(true);
            }
            else if (_inventoryItem == null)
            {
                SlotHighlight.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Enables/disables whether or not to show highlight when dragging items on top of inventory slot.
    /// </summary>
    /// <param name="highlightEnabled">Boolean flag that determines whether or not to show the highlights.</param>
    public void SetHighlightEnabled(bool highlightEnabled)
    {
        _highlightEnabled = highlightEnabled;
    }

    /// <summary>
    /// Enables/disables whether or not to show highlight when dragging items on top other items.
    /// </summary>
    /// <param name="higlightOccupiedSlots">Boolean flag that determines whether or not to show the highlights.</param>
    public void SetHighlightOccupiedSlots(bool higlightOccupiedSlots)
    {
        _highlightOccupiedSlots = higlightOccupiedSlots;
    }
}
