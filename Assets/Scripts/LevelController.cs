using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{

	[Serializable]
	public class Count
	{
		public int maximum = 3;
		public int minimum = 1;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	public Count levelBorders;
	public int levelHeight;
	public GameObject kittenPrefab;
	public Vector3 catSpawnPoint;
	public Count kittenCount;

	private List<GameObject> kittens;
	private Transform boardHolder;

	private Vector3 getKittenSpawnPoint ()
	{
		float x = Random.Range (levelBorders.minimum, levelBorders.maximum);
		float y = levelHeight + kittenPrefab.transform.localScale.y;
		return new Vector3 (x, y, 0f);
	}

	private void SpawnKitten ()
	{
		Vector3 spawnPoint;
		while (true)
		{
			spawnPoint = getKittenSpawnPoint ();
			if (Mathf.Abs (spawnPoint.x) - Mathf.Abs (catSpawnPoint.x) > 5)
			{
				break;
			}
		}
		GameObject kitten = Instantiate (kittenPrefab, spawnPoint, Quaternion.identity) as GameObject;
		kittens.Add (kitten);
		kitten.transform.SetParent (boardHolder);
	}

	public void Setup ()
	{
		int nKittens = Random.Range (kittenCount.minimum, kittenCount.maximum);
		kittens = new List<GameObject> ();

		for (int i = 0; i < nKittens; i++)
		{
			SpawnKitten ();
		}
		//GameObject instance = Instantiate (kittenPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		//instance.transform.SetParent (boardHolder);
	}
}
