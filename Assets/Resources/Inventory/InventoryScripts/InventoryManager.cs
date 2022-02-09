using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory myBag;

    /* Create and show the prefabs of inventory on the GridGroup module */
    public GameObject slotGrid;
    public Slot slotPrefab;
    public Text itemInformation;
    public Slot itemChosen;
    public GameObject doorCave;
    public GameObject triggerEnterNextLevel;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);

        /* Binding the newItem's father with slotGrid to create it into the children of slotGrid */
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform, false);

        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }

    //====================================================================================================================
    // Refresh new itemes of inventory when collided:
    // 1.Destroy itemes in the inventory when refresh
    // 2.Readd itemes into inventory:
    //   it will add new item when collide but only increase number of itemes if it exist already with everytime refreshes
    //=====================================================================================================================
    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            /* if (instance.slotGrid.transform.childCount == 0)
                break; */
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }

    /* Select the item when clicked */
    public static void ChoseItem(Slot item)
    {
        instance.itemChosen = item;
    }

    /* "Can click" if item greater than 0 and it's consumable -> decrease the number */
    public static void UseItem()
    {
        ItemFunction();
        if (instance.itemChosen.slotItem.itemHeld > 0 && instance.itemChosen.slotItem.comsumables)
            instance.itemChosen.slotItem.itemHeld -= 1;
        if (instance.itemChosen.slotItem.itemHeld <= 0)
        {
            instance.itemChosen = null;
        }

    }

    public static void ItemFunction()
    {
        string nameItemChosen = instance.itemChosen.slotItem.itemName;
        if (nameItemChosen == "HealthPotion")
            PlayerController.currentHP += 10;
        if (nameItemChosen == "Key")
            instance.KeyUse();
    }

    private void KeyUse()
    {
        doorCave.GetComponent<AudioSource>().Play();
        Destroy(doorCave, 1.3f);
        triggerEnterNextLevel.SetActive(true);
    }
}
