using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject PC;

	private Vector3 distance;

	// Use this for initialization
	void Start ()
	{
		distance = PC.transform.position - transform.position;
		Debug.Log (distance);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = PC.transform.position + distance;
	}
}
