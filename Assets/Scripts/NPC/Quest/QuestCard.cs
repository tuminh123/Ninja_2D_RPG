using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI questNameTMP;
    [SerializeField] private TextMeshProUGUI questDescriptionTMP;

    protected Quest questToComplete;
    public Quest QuestToComplete => questToComplete;

    public virtual void ConfigQuestUI(Quest quest)
    {
        questToComplete = quest;
        questNameTMP.text = quest.questName;
        questDescriptionTMP.text = quest.description;
    }
}
