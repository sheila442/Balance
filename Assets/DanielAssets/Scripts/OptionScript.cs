using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour {

    [SerializeField]
    private PlayerDialogueManager playerDialogueManager;

    //[HideInInspector]
    public AIDialogue AIDialogue;

    //[HideInInspector]
    public int reference;

    public void OnPointerDown()
    {
        playerDialogueManager.CloseOptions();
        AIDialogue.RecieveAnswer(reference);
    }
}
