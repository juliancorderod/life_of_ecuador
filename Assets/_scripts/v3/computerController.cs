using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computerController : MonoBehaviour {

	GameObject flowerObj;

	// Use this for initialization
	void Start () {

		flowerObj = Resources.Load<GameObject> ("_prefabs/stuff/flowers");

		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectsWithTag("flower").Length == 0){

			Instantiate(flowerObj,transform.position,Quaternion.identity);

		}
		
	}
}
