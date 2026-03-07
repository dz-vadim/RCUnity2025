using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    public GameObject inventoryPanel, chestPanel;
    public GameObject invContent, chestContent;
    public ItemData[] items;
    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<GameObject> currentChestSlots = new List<GameObject>();

    private void Start()
    {
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);
    }

    private void CreateItem(int itemId, List<ItemData> itemList)
    {
        ItemData item = new ItemData(items[itemId].name, items[itemId].id,
            items[itemId].count, items[itemId].isUniq, items[itemId].description);

        if (!item.isUniq && item.count > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (item.id == itemList[i].id)
                {
                    itemList[i].count++;
                    break;
                }
                else if (i == itemList.Count - 1)
                {
                    itemList.Add(item);
                    break;
                }
            }
        }
        else if (item.isUniq || (!item.isUniq && item.count == 0))
        {
            itemList.Add(item);
        }
    }

    public void InstantiatingItem(ItemData itemData, Transform parent, List<GameObject> itemList)
    {
        
    }
}
