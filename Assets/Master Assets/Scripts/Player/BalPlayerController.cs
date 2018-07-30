using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalPlayerController : MonoBehaviour {

    public LayerMask GroundLayer;

    [SerializeField]
    float Speed = 5.0f;
    [SerializeField]
    float JumpForce = 5.0f;

    Rigidbody2D Rb;
    BoxCollider2D PlCol;
    SpriteRenderer SpRd;
    GameObject CameraTarget;

    bool bIsGrounded;
    bool bWantsToJump;

	// Use this for initialization
	void Start ()
    {
        Rb = GetComponent<Rigidbody2D>();
        PlCol = GetComponent<BoxCollider2D>();
        SpRd = GetComponent<SpriteRenderer>();
        CameraTarget = transform.Find("Camera Target").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        bIsGrounded = PlCol.IsTouchingLayers(GroundLayer);

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Jump P2")) && bIsGrounded)
        {
            bWantsToJump = true;
        }
        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("INTERACT P1");
        }
        if (Input.GetButtonDown("Interact P2"))
        {
            Debug.Log("INTERACT P2");
        }
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("CANCEL P1");
        }
        if (Input.GetButtonDown("Cancel P2"))
        {
            Debug.Log("CANCEL P2");
        }
        if (Input.GetButtonDown("Menu"))
        {
            Debug.Log("MENU P1");
        }
        if (Input.GetButtonDown("Menu P2"))
        {
            Debug.Log("MENU P2");
        }
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float xInputP2 = Input.GetAxis("Horizontal P2");
        //float yInput = Input.GetAxis("Vertical");
        //float yInputP2 = Input.GetAxis("Vertical P2");
        
        if ((xInput == 0.0f && xInputP2 == 0.0f) || (xInput > 0.0f && xInputP2 < 0.0f) || (xInput < 0.0f && xInputP2 > 0.0f))
        {
            gameObject.transform.Translate(transform.right * 0.0f);
        }
        else
        {
            float xInputValue = (xInput > 0.0f || xInputP2 > 0.0f) ? (Mathf.Max(xInput, xInputP2)) : (Mathf.Min(xInput, xInputP2)); //Determines correct input value to apply
            SpRd.flipX = xInputValue > 0.0f ? false : true; //Flips player object to face correct direction
            //Vector3 temp = new Vector3(SpRd.flipX ? (CameraTarget.transform.position.x < 0.0f ? CameraTarget.transform.position.x * 1.0f : CameraTarget.transform.position.x * -1.0f) :
			//                                        (CameraTarget.transform.position.x >= 0.0f ? CameraTarget.transform.position.x * 1.0f : CameraTarget.transform.position.x * -1.0f), CameraTarget.transform.position.y, CameraTarget.transform.position.z);

            Vector3 temp = new Vector3((SpRd.flipX ? -2.0f : 2.0f), CameraTarget.transform.position.y, CameraTarget.transform.position.z);

            CameraTarget.transform.localPosition = temp;
            
            gameObject.transform.Translate(transform.right * xInputValue * Speed * Time.fixedDeltaTime);
        }

        if (bWantsToJump)
        {
            Rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        bWantsToJump = false;
    }
}
