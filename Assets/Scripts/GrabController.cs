using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{

	public float maxGrabTime = 1f;
	private float grabTime = 0f;

	private GameObject mouthFull = null;
	private Transform kittenParent = null;

	private void catchKitten ()
	{
		if (mouthFull == null)
			return;
		Debug.Log ("Catch Kitten");
		mouthFull.GetComponent <KittenController> ().caught = true;
		kittenParent = mouthFull.transform.parent;
		mouthFull.transform.SetParent (transform);
		mouthFull.GetComponent<SpriteRenderer> ().color = Color.cyan;
	}

	private void releaseKitten ()
	{
		if (kittenParent == null)
			return;
		Debug.Log ("Release Kitten");
		mouthFull.transform.SetParent (kittenParent);
		kittenParent = null;
		mouthFull.GetComponent <KittenController> ().caught = false;
		mouthFull.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (mouthFull == null && other.CompareTag ("Kitten"))
		{
			mouthFull = other.gameObject;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject == mouthFull)
		{
			releaseKitten ();
			mouthFull = null;
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.E) && mouthFull != null)
		{
			if (grabTime == 0f)
			{
				catchKitten ();
				grabTime = Time.fixedTime;
			}
		}
		if (grabTime != 0f &&
		    Time.fixedTime - grabTime > maxGrabTime &&
		    mouthFull != null)
		{
			releaseKitten ();
			grabTime = 0f;
		}
	}

	void LateUpdate ()
	{
		if (mouthFull != null)
		{
			transform.Find ("E").gameObject.SetActive (true);
		}
		else
		{
			transform.Find ("E").gameObject.SetActive (false);
		}
	}
}
