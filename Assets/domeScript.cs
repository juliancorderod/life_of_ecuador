using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class domeScript : MonoBehaviour {

	public GameObject songScript;

	bool playSongBG;
	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {

		GetComponent<AudioSource>().volume = Mathf.Clamp(GetComponent<AudioSource>().volume,0,0.4f);

		if(playSongBG){
			GetComponent<AudioSource>().volume += Time.deltaTime * 1.25f;


		}else{
			GetComponent<AudioSource>().volume -= Time.deltaTime * 1.25f;
		}



	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "player"){
			songScript.GetComponent<songScript>().unMuteSong();
			playSongBG = true;
			//songScript.GetComponent<songScript>().motBlurOn();
			//headBob.inJungle = true;
		}

	}
	void OnTriggerExit(Collider col){
		if(col.gameObject.name == "player"){
			songScript.GetComponent<songScript>().muteSong();
			playSongBG = false;
			//songScript.GetComponent<songScript>().motBlurOff();
			//headBob.inJungle = false;
		}

	}
}
