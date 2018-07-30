using UnityEngine;

public class OptionScript1 : MonoBehaviour {
    [SerializeField]
    private OptionScript os;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // if player1 press '1'
        {
            os.OnPointerDown();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1)) // if player2 press '1'
        {
            os.OnPointerDown();
        }
    }
}
