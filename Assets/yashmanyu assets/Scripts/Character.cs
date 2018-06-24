using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Character : MonoBehaviour {

	public Rigidbody2D rb;					// Public. Accessible through Inspector. Must drag object/component into Script.

	public float jumpForce;					// Pubilc. Let's user update value through Inspector.

	// Handles movement and jumping
	public bool isGrounded;					// Used to see if character is on the floor, platform, etc...
	public LayerMask isGroundLayer;	// Used to list what is Ground
	public Transform groundCheck;		// Used to check ground collision
	public int speed;								// Used to give character a speed.
	public bool facingRight;
	// Handles animations
	public Animator anim;						// Used to access the animator controller
	int state;											// Used to change state of character

	// Use this for initialization
	void Start () {

		// Link component to variable to be used later.
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		// Check that GetComponent worked.
		// Works if there is a Rigidbody2D on the object the Script is linked to.

		if (!rb) // Is there no Rigidbody2D.
			Debug.Log ("No Rigidbody2D attached.");

		if (!anim) // Is there no Animator.
			Debug.Log ("No Animator attached.");
	
	}

	// Update is called once per frame
	void Update () {

		// Check if character is on ground
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.2f, isGroundLayer);

		// Check if Space is pressed.
		if (Input.GetButtonDown ("Jump") && isGrounded) {
			// Player wants to Jump.
			// Make character jump.
			Debug.Log("Jump");

			// Use Rigidbody to move character
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

	


		// Make character move left or right, if grounded
		// GetAxisRaw returns -1,0,1
		// GetAxis returns -1->1
		float moveValue = Input.GetAxisRaw ("Horizontal");
		 {
			Debug.Log (moveValue);

			// Make character move left or right.
			rb.velocity = new Vector2 (moveValue * speed, rb.velocity.y);

			// Change state parameter
			anim.SetFloat ("Move", Mathf.Abs (moveValue));
		}
		// Check if character should look Left or Right
		if ( 	(moveValue > 0 && !facingRight) ||
			(moveValue < 0 && facingRight))
			flip ();

		anim.SetBool ("IsGrounded", isGrounded);
	} // Closes Update()

	// Function to flip Character in direction it's moving
	void flip()
	{
		// Toggle facingRight variable
		facingRight = !facingRight;

		// Make a copy of old scale
		Vector3 scaleFactor = transform.localScale; // Scale is (1,1,1);

		// Flip the scale
		scaleFactor.x *= -1; // Changed to (-1,1,1)

		// Update the scale to flipped value
		transform.localScale = scaleFactor;
	}


}

	




