using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int levelTime = 0;
	public int kittens = 0;
	private LevelController levelScript;
	private GameObject canvas;
	public GameObject titleScreen;

	public void LevelOver ()
	{
		foreach (Transform child in canvas.transform)
		{
			child.gameObject.SetActive (false);
		}
		titleScreen.SetActive (true);
	}

	// Use this for initialization
	void Awake ()
	{
		canvas = transform.parent.Find ("Canvas").gameObject;
		titleScreen = transform.parent.Find ("TitleScreen").gameObject;
		titleScreen.SetActive (false);
		levelScript = GetComponent<LevelController> ();
		levelScript.Setup (levelTime, kittens);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
