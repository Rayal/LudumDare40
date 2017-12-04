using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
	public float levelLeftBorder;
	public float levelRightBorder;
	public float levelHeight;
	public GameObject kittenPrefab;
	public GameObject pcPrefab;
	public Vector3 catSpawnPoint;
	public int kittenCount;
	public Text kittenText;
	public Text timeText;
	public Text infoText;
	public GameObject cathouse;

	private GameManager gameManager;
	private List<GameObject> kittens;
	private GameObject boardHolder;
	private int startTime;
	private int levelTime;
	private CathouseController cathouseController;

	private bool setupDone = false;

	private Vector3 getKittenSpawnPoint ()
	{
		float y = levelHeight + kittenPrefab.transform.localScale.y;
		if (levelLeftBorder == levelRightBorder)
			return new Vector3 (levelRightBorder, y, 0f);
		float x = Random.Range (levelLeftBorder, levelRightBorder);
		return new Vector3 (x, y, 0f);
	}

	private void SpawnPC ()
	{
		GameObject pc = Instantiate (pcPrefab, catSpawnPoint, Quaternion.identity) as GameObject;
		pc.transform.SetParent (boardHolder.transform);
	}

	private void SpawnKitten ()
	{
		GameObject kitten = Instantiate (kittenPrefab, getKittenSpawnPoint (), Quaternion.identity) as GameObject;
		kittens.Add (kitten);
		kitten.transform.SetParent (boardHolder.transform);
	}

	public void Setup (int timeSeconds = 60, int kittens = 0)
	{
		cathouseController = cathouse.GetComponent <CathouseController> ();
		this.gameManager = GetComponent <GameManager> ();
		boardHolder = new GameObject ();
		boardHolder.transform.SetParent (this.transform);
		this.kittens = new List<GameObject> ();
		if (kittens > 0)
			kittenCount = kittens;
		for (int i = 0; i < kittenCount; i++)
		{
			SpawnKitten ();
		}
		SpawnPC ();
		startTime = (int)Time.fixedTime;
		levelTime = timeSeconds;
		cathouseController.caughtKittens = 0;
		setupDone = true;
		infoText.text = "Get all the kittens in the nest before the time runs out!";
	}

	private void EndLevel (bool playerWon)
	{
		Destroy (boardHolder);
		gameManager.LevelOver ();
		if (playerWon)
			gameManager.WonLevel ();
		else
			gameManager.LostLevel ();
	}

	void Update ()
	{
		if (!setupDone)
			return;
		kittenText.text = string.Format ("Kittens:{0}\nFound:{1}", kittenCount, cathouseController.caughtKittens);
	}

	void FixedUpdate ()
	{
		if (boardHolder == null || !setupDone)
		{
			return;
		}
		if (kittenCount == cathouseController.caughtKittens)//boardHolder.transform.GetChild (0).tag == "Player")
			EndLevel (true);
		else if (Time.fixedTime - startTime > levelTime)
				EndLevel (false);
	}

	void LateUpdate ()
	{
		if (!setupDone)
			return;
		int elapsedTime = (int)Time.fixedTime - startTime;
		int timeLeft = levelTime - elapsedTime;
		timeText.text = string.Format ("{0}:{1}", timeLeft / 60, timeLeft % 60);
	}
}
