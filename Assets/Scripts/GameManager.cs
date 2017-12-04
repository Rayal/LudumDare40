using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	enum GameChoice
	{
		New,
		Next,
		Retry}

	;

	private LevelController levelScript;
	private ArcadeModeController arcadeScript;
	private GameObject canvas;
	private GameObject titleScreen;
	private Text levelText;

	private bool levelStarted = false;
	private int gameLevel = 1;
	private float levelTime;
	private int kittens;

	//private Sprite wonScreen;
	//private Sprite lostScreen;

	private GameChoice gameChoice = GameChoice.New;

	public void LevelOver ()
	{
		canvas.SetActive (false);
		titleScreen.SetActive (true);
	}

	public void StartArcade ()
	{
		levelStarted = true;
		arcadeScript.Setup ();
		titleScreen.SetActive (false);
		canvas.SetActive (true);
		levelText.text = "";
	}

	public void StartGame ()
	{
		levelStarted = true;
		kittens = (int)Random.Range (gameLevel, (float)5 * gameLevel);
		levelTime = Random.Range ((float)0.8 * kittens, (float)1.2 * kittens);
		levelScript.Setup ((int)(levelTime * 30), kittens);
		titleScreen.SetActive (false);
		canvas.SetActive (true);
		levelText.text = string.Format ("Level: {0}", gameLevel);
	}

	public void EndArcade ()
	{
		levelStarted = false;
		gameChoice = GameChoice.New;
		gameLevel = 1;
	}

	public void LostLevel ()
	{
		levelStarted = false;
		gameChoice = GameChoice.Retry;
		gameLevel = 1;
	}

	public void WonLevel ()
	{
		//titleScreen.GetComponent <SpriteRenderer>().sprite = ;
		levelStarted = false;
		gameChoice = GameChoice.Next;
		gameLevel++;
	}

	private void CreateTitleScreen ()
	{
		foreach (Transform child in titleScreen.transform)
		{
			child.gameObject.SetActive (false);
		}
		titleScreen.transform.Find ("PLAY").gameObject.SetActive (true);
		titleScreen.transform.Find ("ARCADE").gameObject.SetActive (true);
		titleScreen.transform.Find ("EXIT").gameObject.SetActive (true);
	}

	private void CreateNextScreen ()
	{
		foreach (Transform child in titleScreen.transform)
		{
			child.gameObject.SetActive (false);
		}
		titleScreen.transform.Find ("NEXT").gameObject.SetActive (true);
		titleScreen.transform.Find ("EXIT").gameObject.SetActive (true);
	}

	private void CreateRetryScreen ()
	{
		foreach (Transform child in titleScreen.transform)
		{
			child.gameObject.SetActive (false);
		}
		titleScreen.transform.Find ("RETRY").gameObject.SetActive (true);
		titleScreen.transform.Find ("ARCADE").gameObject.SetActive (true);
		titleScreen.transform.Find ("EXIT").gameObject.SetActive (true);
	}

	// Use this for initialization
	void Awake ()
	{
		canvas = transform.Find ("Canvas").gameObject;
		titleScreen = transform.Find ("TitleScreen").gameObject;
		titleScreen.SetActive (true);
		levelScript = GetComponent<LevelController> ();
		arcadeScript = GetComponent <ArcadeModeController> ();
		levelText = canvas.transform.Find ("LevelText").gameObject.GetComponent <Text> ();
		//wonScreen = Resources.Load ("Sprites/won_silt)
	}

	void LateUpdate ()
	{
		if (levelStarted)
		{
			return;
		}
		titleScreen.SetActive (true);
		if (gameChoice == GameChoice.New)
		{
			CreateTitleScreen ();
		}
		else
		{
			if (gameChoice == GameChoice.Next)
				CreateNextScreen ();
			else
				CreateRetryScreen ();
		}
	}
}
