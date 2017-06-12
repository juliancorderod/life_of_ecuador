using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockHandles : MonoBehaviour {

	public GameObject clockHandle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		clockHandle.transform.Rotate(0f,1f *Time.deltaTime,0f);
		
	}
}
