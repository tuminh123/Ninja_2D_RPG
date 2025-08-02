using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    [Header("Info")]
    public string questName;
    public string questID;
    public int questGoal;

    [Header("Description")]
    [TextArea] public string description;

    [Header("Rewards")]
    public int goldReward;
    public int expReward;
    public QuestItemReward itemReward;

    public int currentStatus;
    public bool questCompleted;
    public bool questAccepted;

    public void AddProgress(int amount)
    {
        currentStatus += amount;
        if (currentStatus >= questGoal)
        {
            currentStatus = questGoal;
            QuestIsCompleted();
        }
    }

    private void QuestIsCompleted()
    {
        if (questCompleted)
        {
            return;
        }
        questCompleted = true;
    }

    public void ResetQuest()
    {
        currentStatus = 0;
        questCompleted = false;
        questAccepted = false;
    }
}
[System.Serializable]
public class  QuestItemReward
{
    public InventoryItem item;
    public int quantity;
}
