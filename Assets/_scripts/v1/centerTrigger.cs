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
		GetComponent<AudioSource>().volume = Mathf.Clamp(GetComponent<AudioSource>().volume,0,0.25f);

		if(playHum){
			GetComponent<AudioSource>().volume += Time.deltaTime * 1.25f;


		}else{
			GetComponent<AudioSource>().volume -= Time.deltaTime * 1.25f;
		}


	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
		songScript.GetComponent<songScript>().muteSong();
		songScript.GetComponent<songScript>().motBlurOff();
		headBob.inJungle = false;
		playHum = true;
		}


	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
		songScript.GetComponent<songScript>().unMuteSong();
		songScript.GetComponent<songScript>().motBlurOn();
		headBob.inJungle = true;
		playHum = false;
		}

	}
}