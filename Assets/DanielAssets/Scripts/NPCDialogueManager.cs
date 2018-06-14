using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    
    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerDialogueManager playerDialogueManager;

    [SerializeField]
    private Queue<string> sentences;

    private int _reference;
    private List<Dialogue> _dialogue;

    [SerializeField]
    private Text continueText;



    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(List<Dialogue> dialogue, string name, int reference, AIDialogue aiDialogue)
    {
        animator.SetBool("isOpen", true);
        nameText.text = name;
        sentences.Clear();
        continueText.text = "Next...";
        _dialogue = dialogue;

        for (int i = 0; i < _dialogue.Count; i++)
        {
            if (_dialogue[i].reference == reference)
            {
                _reference = i;
            }
        }

        playerDialogueManager.SetOptions(_dialogue[_reference].answers, aiDialogue);

        foreach (string sentence in _dialogue[_reference].sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //there's a better call with == 1 but it needs to be closed differently

        if (sentences.Count == 0) // check before to close
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if (sentences.Count == 0)
        {
            continueText.text = "Quit";
            if (_dialogue[_reference].answers.Count > 0)
            {
                playerDialogueManager.OpenOptions();
            }
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        playerDialogueManager.CloseOptions();
    }
}
