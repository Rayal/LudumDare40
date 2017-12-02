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
	//	public Transform groundCheck;

	private bool grounded = false;
	//private Animator anim;
	private Rigidbody2D rb2d;
	//private BoxCollider2D boxCollider;
	private AudioSource jumpAudio;

	private Vector2 leftBorder, rightBorder;

	void Awake ()
	{
		// Initializing component variables.
		//anim = GetComponent <Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		//boxCollider = groundCheck.GetComponent<BoxCollider2D> ();
		jumpAudio = GetComponent <AudioSource> ();

		// Initializing useful static variables.
		Vector2 scale = transform.localScale / 2;
		leftBorder = (Vector2)transform.position - scale;
		scale.x = -scale.x;
		rightBorder = (Vector2)transform.position - scale;
		Debug.Log (string.Format ("{0}, {1}", leftBorder, rightBorder));
	}

	// Update is called once per frame
	void Update ()
	{
		Vector2 unit = Vector2.one;
		RaycastHit2D hitLeft = Physics2D.Raycast (leftBorder, -unit, 1 << LayerMask.NameToLayer ("Ground"));
		unit.x = -unit.x;
		RaycastHit2D hitRight = Physics2D.Raycast (rightBorder, unit, 1 << LayerMask.NameToLayer ("Ground"));
		grounded = hitLeft.distance < 0.1 || hitRight.distance < 0.1;
		if (grounded)
		{
			Debug.Log (string.Format ("{0}, {1}", hitLeft.collider, hitLeft.distance));
			Debug.Log (string.Format ("{0}, {1}", hitRight.collider, hitRight.distance));
		}
		//grounded = Physics2D.Linecast (transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded)
		{
			Debug.Log ("Jump");
			jump = true;
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.DrawRay (transform.position, rb2d.velocity * 2);
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
			rb2d.velocity = new Vector2 (Mathf.Clamp (rb2d.velocity.x, -maxSpeed, maxSpeed), rb2d.velocity.y);
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
			jumpAudio.Play ();
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
