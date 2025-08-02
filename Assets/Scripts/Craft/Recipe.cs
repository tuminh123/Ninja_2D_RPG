using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe 
{
    public string recipeName;
    [Header("Item 1")]
    public InventoryItem item1;
    public int item1Amount;
    [Header("Item 2")]
    public InventoryItem item2;
    public int item2Amount;
    [Header("Final Item")]
    public InventoryItem finalItem;
    public int finalItemAmount;
}
