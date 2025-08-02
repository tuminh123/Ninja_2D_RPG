using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public static event Action<InteractionType> OnExtraInteractionEvent;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcDialogueTMP;

    private NPCInteraction npcSelected;
    public NPCInteraction NPCSelected => npcSelected;

    private bool dialogueStarted;
    private Queue<string> dialogueQueue = new Queue<string>();

    private PlayerActions playerActions;

    protected override void Awake()
    {
        base.Awake();
        playerActions = new PlayerActions();
    }
    private void Start()
    {
        playerActions.Dialogue.Interact.performed += ctx => ShowDialogue();
        playerActions.Dialogue.Continue.performed += ctx => ContinueDialogue();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }
    public void SetNPCSelected(NPCInteraction npcSelected)
    {
        this.npcSelected = npcSelected;
    }

    //show dialogue
    private void LoadDialogueFromNPC()
    {
        if (npcSelected.DialogueToShow.dialogue.Length <= 0)
        {
            Debug.LogWarning("No dialogue to show for this NPC.");
            return;
        }

        foreach (string sentence in NPCSelected.DialogueToShow.dialogue)
        {
            dialogueQueue.Enqueue(sentence);
        }
    }
    private void ShowDialogue()
    {
        if(NPCSelected == null || dialogueStarted) return;
        dialoguePanel.SetActive(true);
        LoadDialogueFromNPC();
        npcIcon.sprite = NPCSelected.DialogueToShow.icon;
        npcNameTMP.text = NPCSelected.DialogueToShow.npcName;
        npcDialogueTMP.text = NPCSelected.DialogueToShow.greeting;
        dialogueStarted = true;
    }
    private void ContinueDialogue()
    {
        if(NPCSelected == null)
        {
            dialogueQueue.Clear();
            return;
        }
        if (dialogueQueue.Count <= 0)
        {
            CloseDialoguePanel();   
            dialogueStarted = false;
            if (npcSelected.DialogueToShow.hasInteraction)
            {
                OnExtraInteractionEvent?.Invoke(npcSelected.DialogueToShow.interactionType);
            }
            return;
        }
        npcDialogueTMP.text = dialogueQueue.Dequeue();
    }
    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
        dialogueStarted = false;
        dialogueQueue.Clear();
    }

}
