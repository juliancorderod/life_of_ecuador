using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class songScript : MonoBehaviour {

	bool mute = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(mute){
			GetComponent<AudioSource>().volume -= Time.deltaTime * 1.25f;
		}else{
			GetComponent<AudioSource>().volume += Time.deltaTime * 0.25f;
		}
		
	}

	public void muteSong(){

		mute = true;
	}

	public void unMuteSong(){

		mute = false;
	}

	public void motBlurOn(){
		Camera.main.GetComponent<MotionBlur>().enabled = true;

	}
	public void motBlurOff(){
		Camera.main.GetComponent<MotionBlur>().enabled = false;

	}

}
