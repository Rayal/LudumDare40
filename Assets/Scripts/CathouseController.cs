﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CathouseController : MonoBehaviour
{

	[HideInInspector]public int caughtKittens;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Kitten")
		{
			caughtKittens++;
			Debug.Log (string.Format ("Kitten Caught {0}", caughtKittens));
			Destroy (other.gameObject);
		}
	}
}
