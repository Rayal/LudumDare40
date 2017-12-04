using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeModeController : MonoBehaviour
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
	public int levelTime = 15;
	public int maxKittenCount = 50;

	private GameManager gameManager;
	private List<GameObject> kittens;
	private GameObject boardHolder;
	private int startTime;
	private int lastSpawnTime;
	private CathouseController cathouseController;

	private bool setupDone = false;

	public void EndLevel ()
	{
		if (!setupDone)
			return;
		foreach (GameObject kitten in kittens)
		{
			Destroy (kitten);
		}
		gameManager.LevelOver ();
		gameManager.EndArcade ();
		setupDone = false;
	}

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
		kittens.Add (pc);
	}

	private void SpawnKitten ()
	{
		GameObject kitten = Instantiate (kittenPrefab, getKittenSpawnPoint (), Quaternion.identity) as GameObject;
		kittens.Add (kitten);
		kitten.transform.SetParent (boardHolder.transform);
	}

	public void Setup ()
	{
		cathouseController = cathouse.GetComponent <CathouseController> ();
		this.gameManager = GetComponent <GameManager> ();
		boardHolder = new GameObject ();
		boardHolder.transform.SetParent (this.transform);
		this.kittens = new List<GameObject> ();

		SpawnPC ();
		for (int i = 0; i < kittenCount; i++)
		{
			SpawnKitten ();
		}

		startTime = (int)Time.fixedTime;
		lastSpawnTime = startTime;

		cathouseController.caughtKittens = 0;
		setupDone = true;
	}

	private void UpdateText ()
	{
		if (!setupDone)
			return;
		kittenText.text = string.Format ("Kittens Found\n{0}", cathouseController.caughtKittens);
		infoText.text = string.Format ("You will lose if there are more than {0} kittens on the level", maxKittenCount);
	}

	private void CheckEnd ()
	{
		if (!setupDone)
			return;
		if (kittens.Count > maxKittenCount)
		{
			EndLevel ();
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}

	void FixedUpdate ()
	{
		if (!setupDone)
			return;
		if (Time.fixedTime - lastSpawnTime > levelTime)
		{
			for (int i = 0; i < kittenCount; i++)
			{
				SpawnKitten ();
			}
			lastSpawnTime = (int)Time.fixedTime;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateText ();
		CheckEnd ();
	}

	void LateUpdate ()
	{
		if (!setupDone)
			return;
		int elapsedTime = (int)Time.fixedTime - startTime;
		timeText.text = string.Format ("{0}:{1}", elapsedTime / 60, elapsedTime % 60);

		if (Time.fixedTime - lastSpawnTime >= levelTime * 0.6)
		{
			int waveTime = levelTime - ((int)Time.fixedTime - lastSpawnTime);
			infoText.text += string.Format ("\nNext wave in {0}s", waveTime);
		}
	}
}
