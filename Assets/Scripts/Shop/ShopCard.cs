using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI buyAmount;

    private ShopItem item;
    private int quantity;
    private float initCost;
    private float currentCost;

    private void Update()
    {
        buyAmount.text = quantity.ToString();
        itemCost.text = currentCost.ToString();
    }

    public void ConfigShopCard(ShopItem shopItem)
    {
        item = shopItem;
       
        itemIcon.sprite = shopItem.item.icon;
        itemName.text = shopItem.item.nameItem;
        itemCost.text = shopItem.cost.ToString();
        quantity = 1;

        initCost = shopItem.cost;
        currentCost = shopItem.cost;
    }
    public void Add()
    {
        float buyCost = initCost * (quantity+1);
        if(CoinManager.Instance.Coins >= buyCost)
        {
            quantity++;
            currentCost = initCost * quantity;
        }
    }
    public void Remove()
    {
        if (quantity == 1) return;
        quantity--;
        currentCost = initCost * quantity;
    }
    //buy item
    public void BuyItem()
    {
        if(CoinManager.Instance.Coins >= currentCost)
        {
            Inventory.Instance.AddItem(item.item, quantity);
            CoinManager.Instance.RemoveCoins(currentCost);
            quantity = 1;
            currentCost = initCost;
        }
        else
        {
            Debug.Log("Not enough coins to buy this item.");
        }
    }
}
