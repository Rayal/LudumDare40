using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class PlatformController : MonoBehaviour
{
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	//private Animator anim;
	private Rigidbody2D rb2d;
	private BoxCollider2D boxCollider;

	// Use this for initialization
	void Awake ()
	{
		//anim = GetComponent <Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		boxCollider = groundCheck.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded)
		{
			Debug.Log ("Jump");
			jump = true;
		}
	}

	void FixedUpdate ()
	{
		//Debug.Log ("JUpdate");
		float h = Input.GetAxis ("Horizontal");
		if (h != 0.0f)
			Debug.Log (h);
		//anim.SetFloat ("Speed", Mathf.Abs (h));

		if (h * rb2d.velocity.x < maxSpeed)
		{
			rb2d.AddForce (Vector2.right * h * moveForce);
		}
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
		{
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		if (h > 0 && !facingRight)
		{
			Flip ();
		}
		if (h < 0 && facingRight)
		{
			Flip ();
		}
		if (jump)
		{
			Debug.Log ("Jumping");
			//anim.SetTrigger ("Jump");
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
