using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] quests;

    [Header("NPC Quest")]
    [SerializeField] private QuestCardNPC questCardNpcPrefab;
    [SerializeField] private Transform npcPanelContainer;

    [Header("Player Quest")]
    [SerializeField] private QuestCardPlayer questCardPlayerPrefab;
    [SerializeField] private Transform playerPanelContainer;

    private void Start()
    {
        LoadQuestsIntroNPCPanel();
    }

    private void OnEnable()
    {
        for (int i = 0; i <quests.Length; i++)
        {
            quests[i].ResetQuest();
        }
    }

    //Npc Quest
    private void LoadQuestsIntroNPCPanel()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            QuestCard npcCard =  Instantiate(questCardNpcPrefab, npcPanelContainer);
            npcCard.ConfigQuestUI(quests[i]);
        }
    }

    //Player Quest
    public void AcceptQuest(Quest quest)
    {
        QuestCardPlayer playerCard = Instantiate(questCardPlayerPrefab, playerPanelContainer);
        playerCard.ConfigQuestUI(quest);

        /*// Remove the questToUpdate from NPC panel
        foreach (Transform child in npcPanelContainer)
        {
            QuestCardNPC npcCard = child.GetComponent<QuestCardNPC>();
            if (npcCard != null && npcCard.QuestToComplete == questToUpdate)
            {
                Destroy(child.gameObject);
                break;
            }
        }*/
    }

    //quest stats handle
    public void AddProgress(string questId, int amount)
    {
        Quest questToUpdate = QuestExits(questId);
        if (questToUpdate == null) return;
        if (questToUpdate.questAccepted)
        {
            questToUpdate.AddProgress(amount);
        }
    }
    private Quest QuestExits(string questId)
    {
        foreach (Quest quest in quests)
        {
            if(quest.questID == questId)
            {
                return quest;
            }
        }
        return null;
    }
}
