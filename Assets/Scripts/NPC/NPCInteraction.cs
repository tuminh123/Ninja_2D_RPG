using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private NPCDialogue dialogueToShow;
    public NPCDialogue DialogueToShow => dialogueToShow;

    [SerializeField] private GameObject interactionBox;

    private bool dialogueStarted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.SetNPCSelected(this);
            interactionBox.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.SetNPCSelected(null);
            DialogueManager.Instance.CloseDialoguePanel();
            interactionBox.SetActive(false);
        }
    }

}
