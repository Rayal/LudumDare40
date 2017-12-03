using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class KittenController : MonoBehaviour
{
	[HideInInspector] public bool facingRight = true;

	public GameObject frontEdgeDetection;

	public float moveForce = 120f;
	public float maxSpeed = 3f;

	public float floorDistance;
	public float idleTime = 5;
	public static float fixedTime;


	private bool grounded = false;
	private Rigidbody2D rb2d;
	private RaycastHit2D hitFront;

	private float h;
	private float timeLastRotation;
	private float onFloorTime = 0.0f;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		timeLastRotation = Time.fixedTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	//void OnDrawGizmos(){
		//Gizmos.color = grounded ? Color.green : Color.red;
		//Gizmos.DrawCube (frontEdgeDetection.transform.position, Vector3.one);
	//}

	void FixedUpdate ()
	{
		hitFront = Physics2D.Raycast (frontEdgeDetection.transform.position,
		                              Vector2.down,
										1 << LayerMask.NameToLayer ("Ground"));
		grounded = (hitFront != null) && (hitFront.distance < floorDistance);
		if (!grounded)
		{
			h = -h;
			timeLastRotation = Time.fixedTime;
		}
		else
		{
			if (Time.fixedTime - timeLastRotation > Random.Range (0, idleTime))
			{
				timeLastRotation = Time.fixedTime;
				h = Random.Range (-moveForce, moveForce);
			}
		}

		if (h * rb2d.velocity.x < maxSpeed)
		{
			rb2d.AddForce (Vector2.right * h * moveForce);
		}
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) // limit maximum speed
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

		if (grounded) {
			onFloorTime += Time.fixedDeltaTime;
		} else {
			onFloorTime = 0f;
		}

		if (onFloorTime > 3f) {
			transform.rotation = Quaternion.identity;
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
