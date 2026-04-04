using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class Slot : MonoBehaviour, 
                    IDragHandler, 
                    IEndDragHandler,
                    IPointerDownHandler,
                    IPointerUpHandler
{
    public ItemData itemData;
    private Transform _tempParentForSlots;
    private InventoryManager _inventoryManager;
    private PlayerController _playerController;
    private string _parentName;

    private void Start()
    {
        _tempParentForSlots = GameObject.Find("Canvas").transform;
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _playerController = GameObject.Find("Capsule").GetComponent<PlayerController>();
        _parentName = transform.parent.name;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _inventoryManager.descriptionPanel.SetActive(false);
        transform.SetParent(_tempParentForSlots);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float slotInventoryContentDistance =
                Vector3.Distance(transform.position, _inventoryManager.invContent.transform.position) ;
        float slotChestContentDistance = 
                Vector3.Distance(transform.position, _inventoryManager.chestContent.transform.position);
        if (slotInventoryContentDistance < slotChestContentDistance)
        {
            if (_parentName == "InventoryContent")
            {
                transform.SetParent(_inventoryManager.invContent.transform);
            }
            else
            {
                _inventoryManager.currentChestSlots.Remove(gameObject);
                _playerController.currentChestItems.Remove(itemData);
                AddToListOnEndDrag(_inventoryManager.inventorySlots, 
                    _playerController.inventoryItems, 
                    _inventoryManager.invContent.transform);
            }
        }
        else
        {
            if (_parentName == "ChestContent")
            {
                transform.SetParent(_inventoryManager.chestContent.transform);
            }
            else
            {
                _inventoryManager.inventorySlots.Remove(gameObject);
                _playerController.inventoryItems.Remove(itemData);
                AddToListOnEndDrag(_inventoryManager.currentChestSlots, 
                    _playerController.currentChestItems, 
                    _inventoryManager.chestContent.transform);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _inventoryManager.descriptionPanel.SetActive(true);
        _inventoryManager.descriptionPanel.transform.position = transform.position;
        if (itemData != null)
        {
            _inventoryManager.descriptionPanel.transform.Find("DesriptionText").GetComponent<Text>().text = 
                                                                                            itemData.description;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inventoryManager.descriptionPanel.SetActive(false);
    }

    //написати зверху using System.Collections.Generic;
    private void AddToListOnEndDrag(List<GameObject> slots, 
                                    List<ItemData> items, 
                                    Transform parent)
    {
        if(itemData == null) return;
        if (itemData.isUniq || slots.Count == 0)
        {
            slots.Add(gameObject);
            items.Add(itemData);
            transform.SetParent(parent);
            _parentName = transform.parent.name;
        }
        else if (!itemData.isUniq)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].GetComponent<Slot>().itemData.id == itemData.id)
                {
                    items[i].count += itemData.count;
                        slots[i].transform.Find("Value").GetComponent<Text>().text =
                        slots[i].GetComponent<Slot>().itemData.count.ToString();
                    Destroy(gameObject);
                    break;
                }
                else if (i == slots.Count - 1)
                {
                    slots.Add(gameObject);
                    items.Add(itemData);
                    transform.SetParent(parent);
                    _parentName = transform.parent.name;
                    break;
                }
            }
        }
    }
}
