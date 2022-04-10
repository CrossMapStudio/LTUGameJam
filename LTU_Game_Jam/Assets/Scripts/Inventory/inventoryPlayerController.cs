using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryPlayerController : MonoBehaviour
{
    private static Dictionary<string, CollectableData> inventory;

    //Do All UI Herer??
    public Transform inventoryUIContainer;
    public slot inventorySlot;

    public void Awake()
    {
        if (inventory == null)
            inventory = new Dictionary<string, CollectableData>();
    }

    public static void addItemToInventory(CollectableData data)
    {
        if (inventory == null)
            inventory = new Dictionary<string, CollectableData>();
        inventory.Add(data.itemID, data);
    }

    public static bool removeItemFromInventory(string ID)
    {
        if (inventory == null)
            inventory = new Dictionary<string, CollectableData>();
        if (inventory.ContainsKey(ID))
        {
            inventory.Remove(ID);
            return true;
        }
        return false;
    }

    public static bool checkInventory(string ID)
    {
        if (inventory == null)
            inventory = new Dictionary<string, CollectableData>();
        if (inventory.ContainsKey(ID))
        {
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUIContainer.gameObject.activeSelf)
            {
                closeInventory();
            }
            else
            {
                showInventory();
            }
        }
    }

    public void showInventory()
    {
        GameController.holdPlayer = true;
        if (inventory.Count != 0)
        {
            foreach (KeyValuePair<string, CollectableData> element in inventory)
            {
                var local = Instantiate(inventorySlot, inventoryUIContainer.position, Quaternion.identity, inventoryUIContainer);
                local.inventoryImage.sprite = element.Value.UIImage;
                local.inventoryName.text = element.Value.itemName;
            }
        }

        inventoryUIContainer.gameObject.SetActive(true);
    }

    public void closeInventory()
    {
        GameController.holdPlayer = false;
        for (int i = 0; i < inventoryUIContainer.childCount; i++)
        {
            Destroy(inventoryUIContainer.GetChild(i).gameObject);
        }
        inventoryUIContainer.gameObject.SetActive(false);
    }
}
