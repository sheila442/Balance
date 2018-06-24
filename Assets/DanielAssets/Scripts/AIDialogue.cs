using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class AIDialogue : MonoBehaviour {

    [SerializeField]
    private string characterName;

    [SerializeField]
    [Tooltip("If true. Player will need to press 'E' to interact")]
    private bool pressToInteract;
    [SerializeField]
    [Tooltip("If true. Player won't need to press for the first time")]
    private bool pressAfterFirst;
    [SerializeField]
    [Tooltip("Starting text. (Default: 1)")]
    private int defaultReference = 1;

    [SerializeField]
    [Tooltip("Swifting trigger. (For advanced triggers, Default: 0)")]
    private int swiftingTrigger = 0;
    [SerializeField]
    [Tooltip("Swifting value. (For advanced triggers, Default: 0)")]
    private int swiftingValue = 0;
    [SerializeField]
    [Tooltip("Name change value. (For advanced triggers, Default: empty)")]
    private string nameChange = "";

    private bool isPlayerClose;
    private bool dialogueStarted = false;

    [SerializeField]
    [Tooltip("Entire dialogue is here")]
    private List<Dialogue> dialogue;
    [SerializeField]
    [Tooltip("Dialogue manager (GameObject inside canvas)")]
    private NPCDialogueManager npcDialogueManager;

    private void Update()
    {
        if (isPlayerClose) // if player is close
        {
            if (Input.GetKeyDown(KeyCode.E)) // if player press 'E'
            {
                if (!dialogueStarted) // if dialogue is not started
                {
                    StartDialogue();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // If player enters trigger starts Dialogue
    {
        if (other.tag == "Player") // if is player
        {
            isPlayerClose = true; // set the player close
            if (!pressToInteract || pressAfterFirst) // check if needs to interact
            {
                if (!dialogueStarted) // if the dialogue is not started
                {
                    StartDialogue(); // starts the dialogue
                }
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

    public void StartDialogue()
    {
        pressAfterFirst = false; // deactivate pressAfterFirst
        dialogueStarted = true; // set dialogue started
        npcDialogueManager.StartDialogue(dialogue, characterName, defaultReference, this); // Starting new Dialogue
    }

    public void EndDialogue()
    {
        dialogueStarted = false;
        npcDialogueManager.EndDialogue();  // Closes the NPC text
    }

    public void RecieveAnswer(int reference)
    {
        if (swiftingTrigger != 0)
        {
            if (reference == swiftingTrigger)
            {
                defaultReference = swiftingValue;
                if (nameChange != "")
                {
                    characterName = nameChange;
                }
            }
        }
        //npcDialogueManager.DisplayNextSentence(); // When recieving a answer we must start a new Dialogue (Will change for adding sentences...)
        EndDialogue();
        npcDialogueManager.StartDialogue(dialogue, characterName, reference, this); // Start a new Dialogue
    }
}

/*
[CustomEditor(typeof(AIDialogue))]
public class AIDialogueEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as AIDialogue;

        myScript.pressToInteract = EditorGUILayout.Toggle("Press 'E' to Interact", myScript.pressToInteract);

        if (myScript.pressToInteract)
        {
            myScript.pressToInteractAfterFirst = EditorGUILayout.Toggle("Press to interact after first time", myScript.pressToInteractAfterFirst);
        }
        myScript.defaultReference = EditorGUILayout.IntField("Default Reference", myScript.defaultReference);
        GUILayoutOption paras = new GUILayoutOption()
        myScript.dialogue = EditorGUILayout.ObjectField(myScript.dialogue, typeof(Dialogue), paras) as Dialogue;
        myScript.npcDialogueManager = EditorGUILayout.ObjectField(myScript.npcDialogueManager, typeof(NPCDialogueManager), false) as NPCDialogueManager;
    }
}
*/