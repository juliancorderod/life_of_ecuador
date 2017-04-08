using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.LeftArrow)){

			transform.eulerAngles += new Vector3(0f,-1 * Time.deltaTime * 15f,0f);

		}
		if(Input.GetKey(KeyCode.RightArrow)){

			transform.eulerAngles += new Vector3(0f,1 * Time.deltaTime * 15f,0f);

		}
		
	}
}
