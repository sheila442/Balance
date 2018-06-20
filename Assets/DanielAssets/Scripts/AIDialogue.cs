using System.Collections.Generic;
using UnityEngine;

public class AIDialogue : MonoBehaviour {

    [SerializeField]
    private string characterName;

    [Tooltip("Press 'E' to interact")]
    [SerializeField]
    private bool pressToInteract;

    private bool isPlayerClose;
    private bool dialogueStarted = false;

    [Tooltip("Entire dialogue is here")]
    [SerializeField]
    private List<Dialogue> dialogue;

    [Tooltip("Starting text. (Default: 1)")]
    [SerializeField]
    private int defaultReference = 1;

    [Tooltip("Dialogue manager (GameObject inside canvas)")]
    [SerializeField]
    private NPCDialogueManager npcDialogueManager;

    private void Update()
    {
        if (isPlayerClose) // if player is close
        {
            if (pressToInteract && Input.GetKeyDown(KeyCode.E) && !dialogueStarted) // if press to interact is needed; player press E and dialogue not started yet
            {
                dialogueStarted = true;
                npcDialogueManager.StartDialogue(dialogue, characterName, defaultReference, this); // Starting new Dialogue
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // If player enters trigger starts Dialogue
    {
        if (other.tag == "Player") // if is player
        {
            isPlayerClose = true;
            if (!pressToInteract && !dialogueStarted) // Check if interact (E) Key is needed and dialogue not started yet
            {
                dialogueStarted = true;
                npcDialogueManager.StartDialogue(dialogue, characterName, defaultReference, this); // Starting new Dialogue
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // If player exits trigger area close the dialogue
    {
        if (other.tag == "Player")
        {
            isPlayerClose = false;
            EndDialogue(); // Must call AIDialogue end instead of npcDialogueManager
        }
    }

    public void EndDialogue()
    {
        dialogueStarted = false;
        npcDialogueManager.EndDialogue();  // Closes the NPC text
    }

    public void RecieveAnswer(int reference)
    {
        //npcDialogueManager.DisplayNextSentence(); // When recieving a answer we must start a new Dialogue (Will change for adding sentences...)
        npcDialogueManager.StartDialogue(dialogue, characterName, reference, this); // Start a new Dialogue
    }
}
