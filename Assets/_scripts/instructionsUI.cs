using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionsUI : MonoBehaviour {

	public GameObject WASD, look, pick, examine, sprint, crouch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Space)){
			tutorialSlideshow();
		}

		
	}

	void tutorialSlideshow(){

	}
}
