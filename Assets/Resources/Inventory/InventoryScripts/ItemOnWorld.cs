using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory inventoryPlayer;
    public bool assign;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            AddNewItem();
            if (!assign)
                Destroy(gameObject);
        }
    }

    public void AddNewItem()
    {
        if (!inventoryPlayer.itemList.Contains(thisItem))
        {
            inventoryPlayer.itemList.Add(thisItem);
        }
        else
        {
            thisItem.itemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
}
