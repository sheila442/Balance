using UnityEngine;

[System.Serializable]
public class Answer
{
    public int reference;
    
    [TextArea(3, 10)]
    public string option;
}