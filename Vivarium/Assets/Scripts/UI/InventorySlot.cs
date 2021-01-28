using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public delegate void SlotClick(InventorySlot inventorySlot);
    public delegate void SlotDragBegin(InventorySlot inventorySlot);
    public delegate void SlotDragEnd(InventorySlot inventorySlot);
    public delegate void SlotDrag(InventorySlot inventorySlot);
    public static event SlotClick OnSlotClick;
    public static event SlotDrag OnSlotDrag;

    public Image Icon;
    public Button Button;
    public TextMeshProUGUI Count;
    public GameObject EquipOverlay;
    public int Index;

    private InventoryItem _inventoryItem;
    private CharacterController _selectedCharacter;
    private SlotClick _onSlotClick;
    private SlotDragBegin _onSlotDragBegin;
    private SlotDragEnd _onSlotDragEnd;
    private Canvas _canvas;
    private GameObject _duplicateIcon;

    private void Start()
    {
        Button.onClick.AddListener(() =>
        {
            _onSlotClick?.Invoke(this); //Scoped to single inventory slot.
            OnSlotClick?.Invoke(this); //Global
        });
        _canvas = GameObject.FindGameObjectWithTag(Constants.CANVAS_TAG)?.GetComponent<Canvas>();
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
    }

    public void AddOnClickCallback(SlotClick onClick)
    {
        _onSlotClick = onClick;
    }

    public void AddOnDragBeginCallback(SlotDragBegin dragBegin)
    {
        _onSlotDragBegin = dragBegin;
    }

    public void AddOnDragEndCallback(SlotDragEnd dragEnd)
    {
        _onSlotDragEnd = dragEnd;
    }

    public InventoryItem GetItem()
    {
        return _inventoryItem;
    }

    public void UpdateItemDisplay()
    {
        if (_inventoryItem == null)
        {
            Clear();
            return;
        }

        if (_selectedCharacter != null && InventoryManager.GetCharacterItem(_selectedCharacter.Id, _inventoryItem.Item.Id) == null ||
            _selectedCharacter == null && InventoryManager.GetPlayerItem(_inventoryItem.Item.Id) == null)
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        _onSlotDragBegin?.Invoke(this);
        if (_canvas != null)
        {
            Icon.transform.SetParent(_canvas.transform);
            if (!_inventoryItem.Item.CanBeStacked || _inventoryItem.Count < 1)
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

    public void OnDrag(PointerEventData eventData)
    {
        Icon.transform.position = Input.mousePosition;
        OnSlotDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Icon.transform.SetParent(transform);
        Icon.transform.localPosition = Vector3.zero;
        _onSlotDragEnd?.Invoke(this);
    }

    public CharacterController GetCharacter()
    {
        return _selectedCharacter;
    }
}
