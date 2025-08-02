using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    weapon,
    potion,
    scroll,
    ingredients,
    treasure
}
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [Header("Config")]
    public string id;
    public string nameItem;
    public Sprite icon;
    [TextArea] public string description;

    [Header("Info")]
    public ItemType itemType;
    public bool isConsumable;
    public bool isStackable;
    public int maxStack;

    [HideInInspector] public int quantity;

    public InventoryItem CopyItem()
    {
        InventoryItem instance = Instantiate(this);
        return instance;
    }
    public virtual bool UseItem()
    {
        return true;
    }
    public virtual void EquipItem()
    {

    }
    public virtual void RemoveItem()
    {
        
    }

}
