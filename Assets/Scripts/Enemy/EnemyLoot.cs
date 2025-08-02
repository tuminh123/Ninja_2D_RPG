using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float expDrop;
    public float ExpDrop => expDrop;

    [SerializeField] private DropItem[] dropItems;
    [SerializeField] private List<DropItem> items;
    public List<DropItem> Items => items;

    [Header("Debug test")]
    public InventoryItem item1;
    public InventoryItem item2;
    public InventoryItem item3;

    private void Start()
    {
        this.LoadDropItems();
    }
    #region Test
    private void Reset()
    {
        //TestDrop();
    }

    private void TestDrop()
    {
        expDrop = 50f;

        item1 = Resources.Load<InventoryItem>("HealthPotion");
        item2 = Resources.Load<InventoryItem>("ManaPotion");
        item3 = Resources.Load<InventoryItem>("MagicBook");

        DropItem itemOne = new DropItem { itemName = "Health Potion-80%", item = item1, quantity = 5,dropChance = 80,pickedItem = false};
        DropItem itemTwo = new DropItem { itemName = "Mana Potion-60%", item = item2, quantity = 5,dropChance = 60,pickedItem = false};
        DropItem itemThree = new DropItem { itemName = "Book Spell-20%", item = item3, quantity = 2,dropChance = 20,pickedItem = false};

        dropItems = new DropItem[] { itemOne, itemTwo, itemThree };
        LoadDropItems();
    }
    #endregion
    private void LoadDropItems()
    {
        items = new List<DropItem>();
        foreach (DropItem item in dropItems)
        {
            float dropChance  = Random.Range(0f, 100f);
            if(dropChance <= item.dropChance)
            {
                items.Add(item);
            }
        }
    }
}
[System.Serializable]
public class DropItem
{
    [Header("Config")]
    public string itemName;
    public InventoryItem item;
    public int quantity;
    [Header("Drop Chance")]
    public float dropChance;
    public bool pickedItem;
}
