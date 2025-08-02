using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject lootPanel;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform lootButtonContainer;

    protected override void Awake()
    {
        base.Awake();
        lootPanel.SetActive(false);
    }

    public void ShowLoot(EnemyLoot enemyLoot)
    {
        lootPanel.SetActive(true);
        if(lootPanelWithItems())
        {
            for (int i = 0; i < lootButtonContainer.childCount; i++)
            {
                Destroy(lootButtonContainer.GetChild(i).gameObject);
            }
        }
        foreach (DropItem itemDrop in enemyLoot.Items)
        {
            if(itemDrop.pickedItem) continue; // Skip already picked items
            LootButton lootButton = Instantiate(lootButtonPrefab, lootButtonContainer);
            lootButton.ConfigLootButton(itemDrop);
        }
    }
    public void ClosePanel()
    {
        lootPanel.SetActive(false);
    }

    private bool lootPanelWithItems()
    {
        return lootButtonContainer.childCount > 0;
    }
}
