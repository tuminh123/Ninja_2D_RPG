using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private GameContent gameContent;
    [SerializeField] private int inventorySize = 24;
    public int InventorySize => inventorySize;

    [SerializeField] private InventoryItem[] inventoryItems;
    public InventoryItem[] InventoryItems => inventoryItems;

    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY";

    [Header("Testing")]
    public InventoryItem testItem;


    protected override void Awake()
    {
        base.Awake();
        inventoryItems = new InventoryItem[inventorySize];
    }
    
    private void Start()
    {
       
        VerifyItemsForDraw();
        LoadInventory();
        //SaveGame.Delete(INVENTORY_KEY_DATA); // Uncomment this line to reset inventory for testing purposes-if don't want to save item data in inventory
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(testItem, 3);
            DebugPrintInventory();
        }
    }
    #region Inventory function
    //use item
    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStack(index);
        }
        SaveInventory();
    }
    //Remove item
    public void RemoveItem(int index)
    {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);

        SaveInventory();
    }
    //Equip item
    public void EquipItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].itemType != ItemType.weapon) return;
        inventoryItems[index].EquipItem();
    }
    #endregion

    //decrease item
    private void DecreaseItemStack(int index)
    {
        inventoryItems[index].quantity--;
        if (inventoryItems[index].quantity <= 0)
        {
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], index);
        }
    }

    //add item
    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0) return;
        //add itemFormConten stack
        List<int> itemIndexes = CheckItemStock(item.id);//Find all slots containing items with the same id
        if (item.isStackable && itemIndexes.Count > 0)//Item can be stacked and already exists in inventory
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = item.maxStack;
                if (inventoryItems[index].quantity < maxStack)
                {
                    inventoryItems[index].quantity += quantity;
                    if (inventoryItems[index].quantity > maxStack)
                    {
                        int dif = inventoryItems[index].quantity - maxStack;
                        inventoryItems[index].quantity = maxStack;
                        AddItem(item, dif);//recursive
                    }
                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    SaveInventory();
                    return;
                }
            }
        }
        //If item can't be stacked or there's no more stack space
        int quantityToAdd = quantity > item.maxStack ? item.maxStack : quantity;
        AddItemFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0)
        {
            AddItem(item, remainingAmount);
        }
        SaveInventory();
    }
    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    //check index of item and take index of item in slot
    private List<int> CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].id == itemID)
            {
                itemIndexes.Add(i);
            }
        }
        return itemIndexes;
    }

    private void VerifyItemsForDraw()
    {
        Debug.Log("Start");
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InventoryUI.Instance.DrawItem(null, i);
            }
        }
    }

    //Handle crafting item with materials form inventory
    //Get current quantity item in inventory

    public int GetItemCurrentStock(string itemID)
    {
        List<int> indexes = CheckItemStock(itemID);
        int currentStock = 0;
        foreach (int index in indexes)
        {
            if (inventoryItems[index].id == itemID)
            {
                currentStock += inventoryItems[index].quantity;
            }
        }
        return currentStock;
    }
    public void ConsumeItem(string itemID)
    {
        List<int> indexes = CheckItemStock(itemID);
        if (indexes.Count > 0)
        {
            //DecreaseItemStack(indexes[^1]);
            DecreaseItemStack(indexes[indexes.Count - 1]);
        }
    }

    //Save and load Inventory

    //Get item exits in game content
    private InventoryItem ItemExitsInGameContent(string id)
    {
        for (int i = 0; i < gameContent.gameItems.Length; i++)
        {
            if (gameContent.gameItems[i].id == id)
            {
                return gameContent.gameItems[i];
            }
        }
        return null;
    }
    private void LoadInventory()
    {
        if(SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);
            for (int i = 0; i < inventorySize; i++)
            {
                if (loadData.itemContent[i] != null )
                {
                    InventoryItem itemFormConten = ItemExitsInGameContent(loadData.itemContent[i]);
                    if (itemFormConten != null)
                    {
                        inventoryItems[i] = itemFormConten.CopyItem();
                        inventoryItems[i].quantity = loadData.itemQuantity[i];
                        InventoryUI.Instance.DrawItem(inventoryItems[i], i);
                    }
                }
                else
                {
                    inventoryItems[i] = null;
                }
            }
        }
    }

    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData();
        saveData.itemContent = new string[inventorySize];
        saveData.itemQuantity = new int[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                saveData.itemContent[i] = null;
                saveData.itemQuantity[i] = 0;
            }
            else
            {
                saveData.itemContent[i] = inventoryItems[i].id;
                saveData.itemQuantity[i] = inventoryItems[i].quantity;
            }
        }
        SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }

    //Debug item
    public void DebugPrintInventory()
    {
        Debug.Log("---- INVENTORY STATUS ----");
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            InventoryItem item = inventoryItems[i];
            if (item == null)
            {
                Debug.Log($"Slot {i}: Empty");
            }
            else
            {
                Debug.Log($"Slot {i}: {item.name} (ID: {item.id}) x{item.quantity}");
            }
        }
        Debug.Log("--------------------------");
    }

}
