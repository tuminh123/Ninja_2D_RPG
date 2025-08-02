using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image quantityImage;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;
    [SerializeField] private Transform contan;
    private int index;
    public int Index => index;

    //click slot event
    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(index);
    }

    //Setup slot item
    public void SetIndex(int index)
    {
        this.index = index;
    }
    public void UpdateSlot(InventoryItem item)
    {
        itemIcon.sprite = item.icon;
        itemQuantityTMP.text = item.quantity.ToString();
        itemIcon.SetNativeSize();
    }

    public void ShowSlotInformation(bool value)
    {
        itemIcon.gameObject.SetActive(value);
        quantityImage.gameObject.SetActive(value);
        contan.gameObject.SetActive(value);
    }
}
