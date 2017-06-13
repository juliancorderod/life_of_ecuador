using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.localEulerAngles  += new Vector3(Time.deltaTime,0f,0f);
		
	}
}
