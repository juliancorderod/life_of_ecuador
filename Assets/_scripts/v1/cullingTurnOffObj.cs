using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cullingTurnOffObj : MonoBehaviour {

	List<GameObject> allObjectsWithTag = new List<GameObject>();

	GameObject[] objectWithTag;
	GameObject[] antsObj;

	public GameObject player;

	public float distance;

	// Use this for initialization
	void Start () {

		objectWithTag = GameObject.FindGameObjectsWithTag ("memory");
		antsObj = GameObject.FindGameObjectsWithTag ("ant");

		allObjectsWithTag.AddRange (objectWithTag);
		allObjectsWithTag.AddRange (antsObj);
	
	}
	
	// Update is called once per frame
	void Update () {

		foreach(GameObject objectWithTag in allObjectsWithTag){

			if(Vector3.Distance(objectWithTag.transform.position, player.transform.position) >= distance){
				objectWithTag.SetActive(false);
			} else{
				objectWithTag.SetActive(true);

			}

		}
			

	}
}
