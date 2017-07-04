using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computerController : MonoBehaviour {

	public GameObject flowerObj;

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectsWithTag("flower").Length == 0){

			Instantiate(flowerObj,transform.position,Quaternion.identity);

		}
		
	}
}
