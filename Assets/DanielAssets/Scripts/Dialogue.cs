using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
    
    public int reference;

    [TextArea(3, 10)]
    public string[] sentences;
    
    public List<Answer> answers;
}
