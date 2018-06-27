using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueManager : MonoBehaviour
{
    [Tooltip("Name Text Object")]
    [SerializeField]
    private Text nameText;

    [Tooltip("Dialogue Text Object")]
    [SerializeField]
    private Text dialogueText;

    [Tooltip("Canvas Animator")]
    [SerializeField]
    private Animator animator;

    [Tooltip("Player Dialogue Manager Script (Drag Object)")]
    [SerializeField]
    private PlayerDialogueManager playerDialogueManager;

    private Queue<string> sentences; // The Queue of sentences to be recieved

    private AIDialogue _aiDialogue;
    private int _reference; // Global refer to the references
    private List<Dialogue> _dialogue; // Global refer to the Dialogue List

    [Tooltip("Continue button Text")]
    [SerializeField]
    private Text continueText;
    
    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>(); // Inniciate the setences
    }

    public void StartDialogue(List<Dialogue> dialogue, string name, int reference, AIDialogue aiDialogue) // Starts the Dialogue (Recieves the list of dialogues, name of character, reference (default 1), NPC AIDialogue Script)
    {
        playerDialogueManager.enabled = true;
        animator.SetBool("isOpen", true); // Opening Text in Game
        nameText.text = name; // Setting the NPC name
        sentences.Clear(); // Clearing all the setences
        continueText.text = "Next..."; // Setting the Button for Next (Default) Can be modified by editor if needed
        _dialogue = dialogue; // Setting the global dialogue
        _aiDialogue = aiDialogue;
        for (int i = 0; i < _dialogue.Count; i++) // Looping all the Dialogues
        {
            if (_dialogue[i].reference == reference) // Checking reference
            {
                _reference = i; // Setting reference
            }
        }
        
        foreach (string sentence in _dialogue[_reference].sentences) // Looping every sentence related to the reference
        {
            sentences.Enqueue(sentence); // Enqueuing the sentences
        }

        DisplayNextSentence(); // Displaying the next sentence
    }

    public void DisplayNextSentence()
    {
        //there's a better call with == 1 but it needs to be closed differently (needs deep research)

        if (sentences.Count == 0) // check before to close
        {
            SendEndDialogue(); // Ends Dialogue
            return;
        }

        string sentence = sentences.Dequeue(); //Dequeuing the next sentence (this removes it from the Queue)
        StopAllCoroutines(); // Stop all coroutines before staring
        StartCoroutine(TypeSentence(sentence)); // Start the typing effect coroutine

        if (sentences.Count == 0) // If there are no more sentences
        {
            continueText.text = "Quit"; // Change the button text to Quit
            if (_dialogue[_reference].answers.Count > 0) // If there are answers
            {
                playerDialogueManager.SetOptions(_dialogue[_reference].answers, _aiDialogue); // Setting the options for the player
                playerDialogueManager.OpenOptions(); // Send the answers to the player
            }
        }
    }

    IEnumerator TypeSentence(string sentence) // Adds of typing effect
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void RecieveEndDialogue()
    {
        animator.SetBool("isOpen", false); // Set isOpen false for the animator
        playerDialogueManager.CloseOptions(); // Call the same function for the player
    }

    public void SendEndDialogue()
    {
        animator.SetBool("isOpen", false); // Set isOpen false for the animator
        _aiDialogue.RecieveEndDialogue();
        playerDialogueManager.CloseOptions(); // Call the same function for the player
    }
}
