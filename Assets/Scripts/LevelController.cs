using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
	public float levelLeftBorder;
	public float levelRightBorder;
	public float levelHeight;
	public GameObject kittenPrefab;
	public Vector3 catSpawnPoint;
	public int kittenCount;

	private List<GameObject> kittens;
	private Transform boardHolder;

	private Vector3 getKittenSpawnPoint ()
	{
		float y = levelHeight + kittenPrefab.transform.localScale.y;
		if (levelLeftBorder == levelRightBorder)
			return new Vector3 (levelRightBorder, y, 0f);
		float x = Random.Range (levelLeftBorder, levelRightBorder);
		return new Vector3 (x, y, 0f);
	}

	private void SpawnKitten ()
	{
		GameObject kitten = Instantiate (kittenPrefab, getKittenSpawnPoint (), Quaternion.identity) as GameObject;
		kittens.Add (kitten);
		kitten.transform.SetParent (boardHolder);
	}

	public void Setup ()
	{
		kittens = new List<GameObject> ();

		for (int i = 0; i < kittenCount; i++)
		{
			SpawnKitten ();
		}
		//GameObject instance = Instantiate (kittenPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		//instance.transform.SetParent (boardHolder);
	}
}
