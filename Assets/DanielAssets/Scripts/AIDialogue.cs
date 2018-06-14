using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDialogue : MonoBehaviour {

    [SerializeField]
    private string characterName;
    
    [SerializeField]
    private List<Dialogue> dialogue;

    [SerializeField]
    private int defaultReference;

    [SerializeField]
    private NPCDialogueManager npcDialogueManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            npcDialogueManager.StartDialogue(dialogue, characterName, defaultReference, this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EndDialogue(); // Must call AIDialogue end instead of npcDialogueManager
        }
    }

    public void EndDialogue()
    {
        npcDialogueManager.EndDialogue(); 
    }

    public void RecieveAnswer(int reference)
    {
        //npcDialogueManager.DisplayNextSentence();
        npcDialogueManager.StartDialogue(dialogue, characterName, reference, this);
    }
}
