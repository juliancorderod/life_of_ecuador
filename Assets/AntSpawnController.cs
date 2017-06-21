using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawnController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Rotate (Vector3.up, Random.Range (0f, 360f), Space.World);

		for (int i = transform.childCount-1; i >= 0; i--)
			transform.GetChild (i).parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
