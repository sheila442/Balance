using UnityEngine;

public class OptionScript : MonoBehaviour {

    [Tooltip("Player Dialogue Manager Script (Drag Object)")]
    [SerializeField]
    private PlayerDialogueManager playerDialogueManager;

    [HideInInspector] // Used for Recieving/Debuging only
    public AIDialogue AIDialogue; // NPC AIDialogue Script

    [HideInInspector] // Used for Recieving/Debuging only
    public int reference; // Anwser ID reference

    public void OnPointerDown() // Called when Clicked
    {
        playerDialogueManager.CloseOptions(); // Close the player Dialogue
        AIDialogue.RecieveAnswer(reference); // Send the answer ID reference to the NPC AIDialogue Script
    }
}
