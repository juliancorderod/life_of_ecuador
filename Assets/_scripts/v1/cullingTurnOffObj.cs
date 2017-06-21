using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cullingTurnOffObj : MonoBehaviour {


	GameObject[] objectWithTag;
	GameObject[] antsObj;

	public GameObject player;

	public float distance;

	bool addStuff = true;

	// Use this for initialization
	void Start () {

//		objectWithTag = GameObject.FindGameObjectsWithTag ("memory");
//		antsObj = GameObject.FindGameObjectsWithTag ("ant");
//
//		allObjectsWithTag.AddRange (objectWithTag);
//		allObjectsWithTag.AddRange (antsObj);
	
	}
	
	// Update is called once per frame
	void Update () {
		if(addStuff){
			objectWithTag = GameObject.FindGameObjectsWithTag ("memory");
			antsObj = GameObject.FindGameObjectsWithTag ("ant");


			addStuff = false;
		}


		for(int i = 0; i < objectWithTag.Length; i++){

			if(Vector3.Distance(objectWithTag[i].transform.position, player.transform.position) >= distance){
				objectWithTag[i].SetActive(false);
			} else{
				objectWithTag[i].SetActive(true);
			}

		}

		for(int i = 0; i < antsObj.Length; i++){

			if(Vector3.Distance(antsObj[i].transform.position, player.transform.position) >= distance){
				antsObj[i].SetActive(false);
			} else{
				antsObj[i].SetActive(true);
			}

		}
			

	}
}
