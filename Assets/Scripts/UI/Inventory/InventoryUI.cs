using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject inventoryPanel;
    [Space]
    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private InventorySlot currentSlot;
    public InventorySlot CurrentSlot => currentSlot;

    private List<InventorySlot> slotList = new List<InventorySlot>();
    
    private void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
        descriptionPanel.gameObject.SetActive(false);
        InitInventory();
    }

    private void OnEnable() {
        InventorySlot.OnSlotSelectedEvent += SlotSelectedCallBack;
    }
    private void OnDisable() {
        InventorySlot.OnSlotSelectedEvent -= SlotSelectedCallBack;
    }
    
    //select index slot item when click button slot
    private void SlotSelectedCallBack(int slotIndex)
    {
        currentSlot = slotList[slotIndex];
        ShowItemDescription(slotIndex);
    }

    #region Function Inventory
    //Button click event
    //use item when click button use item
    public void UseItem()
    {
        if (currentSlot == null) return;
        Inventory.Instance.UseItem(currentSlot.Index);
    }
    public void RemoveItem()
    {
        if (currentSlot == null) return;
        Inventory.Instance.RemoveItem(currentSlot.Index);
    }
    public void EquipItem()
    {
        if (currentSlot == null) return;
        Inventory.Instance.EquipItem(currentSlot.Index);
    }
    #endregion
    //Init inventory ui
    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            Debug.Log("Create");
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.SetIndex(i);
            slotList.Add(slot);
        }
    }

    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        if (item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }
    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf == false)
        {
            descriptionPanel.gameObject.SetActive(false);
            currentSlot = null;
        }
    }

    //Show item description
    public void ShowItemDescription(int index)
    {
        if(Inventory.Instance.InventoryItems[index] == null) return;
        descriptionPanel.gameObject.SetActive(true);
        icon.sprite = Inventory.Instance.InventoryItems[index].icon;
        itemName.text = Inventory.Instance.InventoryItems[index].nameItem;
        itemDescription.text = Inventory.Instance.InventoryItems[index].description;
    }
}
