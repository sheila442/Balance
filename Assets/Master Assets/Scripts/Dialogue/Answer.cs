using UnityEngine;

[System.Serializable]
public class Answer
{
    [Tooltip("This is the id sent back to the NPC (used to add behavior)")]
    public int reference;

    [Tooltip("Answer Text")]
    [TextArea(3, 10)]
    public string option;
}