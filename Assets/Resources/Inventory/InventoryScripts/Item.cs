using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;

    [TextArea]
    public string itemInfo;

    /* Mark this item is concumbale or not[Comsumables:Coins,health_potion...] | nonexpendable itemes:Paper,Key... */
    public bool comsumables;
}
