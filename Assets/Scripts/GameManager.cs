using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public LevelController levelScript;

	// Use this for initialization
	void Awake ()
	{
		levelScript = GetComponent<LevelController> ();
		levelScript.Setup ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
