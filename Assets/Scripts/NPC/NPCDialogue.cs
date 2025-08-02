using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    None,
    Talk,
    Shop,
    Quest,
    Craft
}

[CreateAssetMenu(menuName ="NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [Header("Info")]
    public string npcName;
    public Sprite icon;

    [Header("Interaction")]
    public bool hasInteraction;
    public InteractionType interactionType;

    [Header("Dialogue")]
    public string greeting;
    [TextArea] public string[] dialogue;

}
