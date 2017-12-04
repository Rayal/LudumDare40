using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : MonoBehaviour
{

	private LevelController levelController;
	private ArcadeModeController arcadeController;

	void OnMouseUp ()
	{
		levelController.EndLevel (false);
		arcadeController.EndLevel ();
	}

	// Use this for initialization
	void Awake ()
	{
		levelController = transform.parent.GetComponent <LevelController> ();
		arcadeController = transform.parent.GetComponent <ArcadeModeController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
