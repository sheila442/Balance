using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventorySript : MonoBehaviour {

	public bool didGetKey = false;
	public bool didGetKey2 = false;

	public Sprite sprite1;
	public Sprite sprite2; 
	[SerializeField] GameObject keyImage;


	void OnTriggerEnter2D (Collider2D other) {

		GameObject firstSprite = GameObject.FindGameObjectWithTag ("firstsprite");
		GameObject secondSprite = GameObject.FindGameObjectWithTag ("secondsprite");


		if (other.tag == "Key") {
			didGetKey = true;
			Destroy (other.gameObject);
			if (firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite1 && firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite2) {
				GameObject.FindGameObjectWithTag ("firstsprite").GetComponent<SpriteRenderer> ().sprite = sprite1;
			}
				
			if (firstSprite.GetComponent<SpriteRenderer> ().sprite == sprite2) {
				GameObject.FindGameObjectWithTag ("secondsprite").GetComponent<SpriteRenderer> ().sprite = sprite1;
			}
		}


		if (other.tag == "Key2") {
			didGetKey2 = true;
			Destroy (other.gameObject);

			if (firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite1 && firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite2) {
				GameObject.FindGameObjectWithTag ("firstsprite").GetComponent<SpriteRenderer> ().sprite = sprite2;
			}

			if (firstSprite.GetComponent<SpriteRenderer> ().sprite == sprite1) {
				GameObject.FindGameObjectWithTag ("secondsprite").GetComponent<SpriteRenderer> ().sprite = sprite2;
			}
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
