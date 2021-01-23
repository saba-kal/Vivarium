using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public delegate void SlotClick(InventorySlot inventorySlot);
    public static event SlotClick OnSlotClick;

    public Image Icon;
    public Button Button;
    public TextMeshProUGUI Count;
    public GameObject EquipOverlay;
    public int Index;

    private InventoryItem _inventoryItem;
    private CharacterController _selectedCharacter;
    private SlotClick _onSlotClick;

    private void Start()
    {
        Button.onClick.AddListener(() =>
        {
            _onSlotClick?.Invoke(this); //Scoped to single inventory slot.
            OnSlotClick?.Invoke(this); //Global
        });
    }

    public void SetItem(InventoryItem inventoryItem, CharacterController selectedCharacter = null)
    {
        _inventoryItem = inventoryItem;
        _selectedCharacter = selectedCharacter;
        UpdateItemDisplay();
    }

    public void AddOnClickCallback(SlotClick onClick)
    {
        _onSlotClick = onClick;
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
}
