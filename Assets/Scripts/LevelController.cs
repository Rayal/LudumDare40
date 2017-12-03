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
	public GameObject cathouse;

	private List<GameObject> kittens;
	private GameObject boardHolder;
	private int startTime;
	private int levelTime;

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
	}

	void Update ()
	{
		CathouseController cathouseController = cathouse.GetComponent <CathouseController> ();
		kittenText.text = string.Format ("Kittens\n{0}\n\nFound\n{1}", kittenCount, cathouseController.caughtKittens);
	}

	void FixedUpdate ()
	{
		if (boardHolder.transform.GetChild (0).tag == "Player" ||
		    Time.fixedTime - startTime > levelTime)
		{
			Destroy (boardHolder);
		}
	}
}
