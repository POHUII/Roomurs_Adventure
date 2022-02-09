using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }

    public void SignItemWhenClicked()
    {
        InventoryManager.ChoseItem(this);
    }

    public void ClickItemUse()
    {
        InventoryManager.UseItem();
        InventoryManager.RefreshItem();
    }
}
