using UnityEngine;

public class OptionScript3 : MonoBehaviour {
    [SerializeField]
    private OptionScript os;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3)) // if player1 press '3'
        {
            os.OnPointerDown();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3)) // if player2 press '3'
        {
            os.OnPointerDown();
        }
    }
}
