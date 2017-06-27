﻿using System.Collections;
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

		GetComponent<AudioSource>().volume = Mathf.Clamp(GetComponent<AudioSource>().volume,0,0.35f);

		if(playSongBG){
			
			GetComponent<AudioSource>().volume += Time.deltaTime * 0.05f;


		}else{
			GetComponent<AudioSource>().volume -= Time.deltaTime * 0.75f;

		}



	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "player"){
			songScript.GetComponent<songScriptv2>().unMuteSong();
			playSongBG = true;

		
		}

	}
	void OnTriggerExit(Collider col){
		if(col.gameObject.name == "player"){
			songScript.GetComponent<songScriptv2>().muteSong();
			playSongBG = false;
		}

	}
}