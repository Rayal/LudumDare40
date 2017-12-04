using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EController : MonoBehaviour
{
	public float scaleX = 0.5f;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (transform.parent.localScale.x < 0)
		{
			Vector3 scale = transform.localScale;
			scale.x = -Mathf.Abs (scale.x);
			transform.localScale = scale;
		}
	}
}
