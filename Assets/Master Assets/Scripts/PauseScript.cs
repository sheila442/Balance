using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	public Transform Panel;
	public Transform Canvas;

	bool isPresent = false;

	void Update () {

        if(Input.GetButtonDown("Menu") || Input.GetButtonDown("Menu P2")){
			if (isPresent == false) {
                Panel.gameObject.SetActive(true);
				isPresent = true;
				Panel.transform.position = new Vector2(Canvas.position.x, Canvas.position.y);    
				Time.timeScale = 0;
				Cursor.visible = true;


			} else {
				isPresent = false;
                Panel.gameObject.SetActive(false);
				Time.timeScale = 1;
				Cursor.visible = false;

			}
		}
	}
}
