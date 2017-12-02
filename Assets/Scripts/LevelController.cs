using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine = UnityEngine.Random;

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

	public GameObject kittenPrefab;
	public GameObject[] kittens;
	public Count kittenCount;

	private Transform boardHolder;

	public void Setup ()
	{
		//int nKittens = Random.Range (kittenCount.minimum, kittenCount.maximum);
		GameObject instance = Instantiate (kittenPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		instance.transform.SetParent (boardHolder);
	}
}
