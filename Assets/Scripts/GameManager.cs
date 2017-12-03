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
	private GameObject canvas;
	private GameObject titleScreen;
	private Text levelText;

	private bool gameStarted = false;
	private bool levelStarted = false;
	private int gameLevel = 1;
	private float levelTime;
	private int kittens;

	private GameChoice gameChoice = GameChoice.New;

	public void LevelOver ()
	{
		canvas.SetActive (false);
		titleScreen.SetActive (true);
	}

	public void StartGame ()
	{
		levelStarted = true;
		kittens = (int)Random.Range (gameLevel, (float)1.3 * gameLevel);
		levelTime = Random.Range ((float)1.3 * gameLevel, (float)1.5 * gameLevel);
		levelScript.Setup ((int)(levelTime * 60), kittens);
		titleScreen.SetActive (false);
		canvas.SetActive (true);
		levelText.text = string.Format ("Level: {0}", gameLevel);
	}

	public void LostLevel ()
	{
		levelStarted = false;
		gameChoice = GameChoice.Retry;
		gameLevel = 1;
	}

	public void WonLevel ()
	{
		levelStarted = false;
		gameChoice = GameChoice.Next;
		gameLevel++;
	}

	// Use this for initialization
	void Awake ()
	{
		canvas = transform.Find ("Canvas").gameObject;
		titleScreen = transform.Find ("TitleScreen").gameObject;
		titleScreen.SetActive (true);
		levelScript = GetComponent<LevelController> ();
		levelText = canvas.transform.Find ("LevelText").gameObject.GetComponent <Text> ();
	}

	void LateUpdate ()
	{
		if (levelStarted)
		{
			return;
		}
		GameObject button = null;
		titleScreen.SetActive (true);
		if (gameChoice != GameChoice.Retry)
			button = titleScreen.transform.Find ("PLAY").gameObject;
		else
			button = titleScreen.transform.Find ("RETRY").gameObject;
		button.SetActive (true);
	}
}
