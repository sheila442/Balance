using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogueManager : MonoBehaviour {

    [Tooltip("First chat baloon")]
    [SerializeField]
    private Text option1;
    [Tooltip("First chat button")]
    [SerializeField]
    private OptionScript optionScriptBtn1;
    [Tooltip("Second chat baloon")]
    [SerializeField]
    private Text option2;
    [Tooltip("Second chat button")]
    [SerializeField]
    private OptionScript optionScriptBtn2;
    [Tooltip("Second chat baloon")]
    [SerializeField]
    private Text option3;
    [Tooltip("Third chat button")]
    [SerializeField]
    private OptionScript optionScriptBtn3;
    [SerializeField]
    private Animator animator;
    
    public void SetOptions(List<Answer> answers, AIDialogue aiDialogue)
    {
        if (answers.Count > 2) // if has 3 answers
        {
            optionScriptBtn1.gameObject.SetActive(true); // show first button
            optionScriptBtn2.gameObject.SetActive(true); // show second button
            optionScriptBtn3.gameObject.SetActive(true); // show third button

            option1.text = answers[0].option; // FIRST button with FIRST answer
            optionScriptBtn1.reference = answers[0].reference;
            optionScriptBtn1.AIDialogue = aiDialogue;
            
            option2.text = answers[1].option; // FIRST answer with SECOND button
            optionScriptBtn2.reference = answers[1].reference;
            optionScriptBtn2.AIDialogue = aiDialogue;
            
            option3.text = answers[2].option; // THIRD answer with THIRD button
            optionScriptBtn3.reference = answers[2].reference;
            optionScriptBtn3.AIDialogue = aiDialogue;
        }
        else
        if (answers.Count > 0) // if has 1 answer
        {
            optionScriptBtn1.gameObject.SetActive(false); // hide first button
            optionScriptBtn3.gameObject.SetActive(true); // show second button
            optionScriptBtn3.gameObject.SetActive(false); // hide third button

            option2.text = answers[0].option; // SECOND button with FIRST answer
            optionScriptBtn2.reference = answers[0].reference;
            optionScriptBtn2.AIDialogue = aiDialogue;
        }
        else // if has no answers
        {
            CloseOptions(); // close the options
        }
    }
    
    public void CloseOptions()
    {
        animator.SetBool("isOpen", false); // close options
        optionScriptBtn1.gameObject.SetActive(false);
        optionScriptBtn2.gameObject.SetActive(false); // hide all buttons
        optionScriptBtn3.gameObject.SetActive(false);
        this.enabled = false;
    }

    public void OpenOptions()
    {
        animator.SetBool("isOpen", true); // open options
    }
}
