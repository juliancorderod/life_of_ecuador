using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cullingTurnOffObj : MonoBehaviour {

	List<GameObject> allObjectsWithTag = new List<GameObject>();

	GameObject[] objectWithTag;

	public GameObject player;

	// Use this for initialization
	void Start () {

		objectWithTag = GameObject.FindGameObjectsWithTag ("memory");

		allObjectsWithTag.AddRange (objectWithTag);
	
	}
	
	// Update is called once per frame
	void Update () {

		foreach(GameObject objectWithTag in allObjectsWithTag){

			if(Vector3.Distance(objectWithTag.transform.position, player.transform.position) >= 30f){
				objectWithTag.SetActive(false);
			} else{
				objectWithTag.SetActive(true);

			}

		}

	}
}
