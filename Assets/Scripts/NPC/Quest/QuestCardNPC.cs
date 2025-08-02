using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestCardNPC : QuestCard
{
    [SerializeField] private TextMeshProUGUI questRewardTMP;
    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        questRewardTMP.text = $"- {quest.goldReward} Gold\n" +
            $"- {quest.expReward} Exp\n" +
            $"- {quest.itemReward.quantity} {quest.itemReward.item.nameItem}";
    }

    public void AcceptQuest()
    {
        if (questToComplete == null) return;
        questToComplete.questAccepted = true;
        QuestManager.Instance.AcceptQuest(questToComplete);
        Destroy(gameObject);
    }
}
