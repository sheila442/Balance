using UnityEngine;

public class OptionScript2 : MonoBehaviour {
    [SerializeField]
    private OptionScript os;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) // if player1 press '2'
        {
            os.OnPointerDown();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2)) // if player2 press '2'
        {
            os.OnPointerDown();
        }
    }
}
