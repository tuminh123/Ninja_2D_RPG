using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCardPlayer : QuestCard
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private TextMeshProUGUI goldRewardTMP;
    [SerializeField] private TextMeshProUGUI expRewardTMP;

    [Header("Item")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    [Header("Quest Completed")]
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject rewardsPanel;
    private void OnEnable()
    {
        QuestCompletedCheck();
    }
    private void Update()
    {
        statusTMP.text = $"Status\n{questToComplete.currentStatus}/{questToComplete.questGoal}";
    }

    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        statusTMP.text = $"Status\n{quest.currentStatus}/{quest.questGoal}";
        goldRewardTMP.text = quest.goldReward.ToString();
        expRewardTMP.text = quest.expReward.ToString();
        //item
        itemIcon.sprite = quest.itemReward.item.icon;
        itemQuantityTMP.text = quest.itemReward.quantity.ToString();
    }
    public void ClaimQuest()
    {
        GameManager.Instance.AddPlayerExp(questToComplete.goldReward);
        Inventory.Instance.AddItem(questToComplete.itemReward.item, questToComplete.itemReward.quantity);
        CoinManager.Instance.AddCoins(questToComplete.goldReward);
        gameObject.SetActive(false);
    }
    private void QuestCompletedCheck()
    {
        if (questToComplete.questCompleted)
        {
            claimButton.SetActive(true);
            rewardsPanel.SetActive(false);
        }
    }
}
