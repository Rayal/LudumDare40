using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeButtonController : MonoBehaviour
{
	
	private GameManager gameManager;

	void OnMouseUp ()
	{
		gameManager.StartArcade ();
		gameObject.SetActive (false);
	}

	// Use this for initialization
	void Awake ()
	{
		gameManager = transform.parent.parent.GetComponent <GameManager> ();
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
