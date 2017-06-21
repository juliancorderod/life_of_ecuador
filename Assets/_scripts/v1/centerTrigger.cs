using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerTrigger : MonoBehaviour {

	public GameObject songScript;

	bool playHum = false;

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {

		if(playHum){
			GetComponent<AudioSource>().volume += Time.deltaTime * 1.25f;


		}else{
			GetComponent<AudioSource>().volume -= Time.deltaTime * 1.25f;
		}


	}

	void OnTriggerEnter(){
		songScript.GetComponent<songScript>().muteSong();
		songScript.GetComponent<songScript>().motBlurOff();
		headBob.inJungle = false;
		playHum = true;


	}
	void OnTriggerExit(){
		songScript.GetComponent<songScript>().unMuteSong();
		songScript.GetComponent<songScript>().motBlurOn();
		headBob.inJungle = true;
		playHum = false;

	}
}