using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3Script : MonoBehaviour {

	public bool didGetKey = false;
	public bool didGetKey2 = false;

	public Sprite sprite1;
	public Sprite sprite2; 
	[SerializeField] GameObject keyImage;


	void OnTriggerEnter2D (Collider2D other) {

		GameObject firstSprite = GameObject.FindGameObjectWithTag ("item3FirstSprite");
		GameObject secondSprite = GameObject.FindGameObjectWithTag ("ite3SecondSprite");


		if (other.tag == "ThirdItem1") {
			didGetKey = true;
			Destroy (other.gameObject);
			if (firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite1 && firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite2) {
				GameObject.FindGameObjectWithTag ("item3FirstSprite").GetComponent<SpriteRenderer> ().sprite = sprite1;
			}

			if (firstSprite.GetComponent<SpriteRenderer> ().sprite == sprite2) {
				GameObject.FindGameObjectWithTag ("item3SecondSprite").GetComponent<SpriteRenderer> ().sprite = sprite1;
			}
		}


		if (other.tag == "ThirdItem2") {
			didGetKey2 = true;
			Destroy (other.gameObject);

			if (firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite1 && firstSprite.GetComponent<SpriteRenderer> ().sprite != sprite2) {
				GameObject.FindGameObjectWithTag ("item3FirstSprite").GetComponent<SpriteRenderer> ().sprite = sprite2;
			}

			if (firstSprite.GetComponent<SpriteRenderer> ().sprite == sprite1) {
				GameObject.FindGameObjectWithTag ("item3SecondSprite").GetComponent<SpriteRenderer> ().sprite = sprite2;
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
