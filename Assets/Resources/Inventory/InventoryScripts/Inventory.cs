using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Manage the inventory Class:
/// Create new inventory among different characters
///</summary>

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
