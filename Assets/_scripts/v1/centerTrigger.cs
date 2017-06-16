using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerTrigger : MonoBehaviour {

	public GameObject songScript;


	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter(){
		songScript.GetComponent<songScript>().muteSong();


	}
	void OnTriggerExit(){
		songScript.GetComponent<songScript>().unMuteSong();


	}
}