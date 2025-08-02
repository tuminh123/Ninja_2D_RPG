using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    private DropItem itemLoaded;
    public DropItem ItemLoaded => itemLoaded;
    
    public void ConfigLootButton(DropItem dropItem)
    {
        itemLoaded = dropItem;
        itemIcon.sprite = dropItem.item.icon;
        itemName.text = dropItem.item.nameItem;
        itemQuantity.text = dropItem.quantity.ToString();
    }

    public void CollectItem()
    {
        if (itemLoaded == null) return;
        Inventory.Instance.AddItem(itemLoaded.item, itemLoaded.quantity);
        itemLoaded.pickedItem = true;
        Destroy(gameObject);
    }

}
