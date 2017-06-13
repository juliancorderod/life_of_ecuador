using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneTransition : MonoBehaviour {

	public float transitionSpeed;

	// Use this for initialization
	void Start () {
		GetComponent<Image>().color = Color.black;
		
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Image>().color -= new Color(0f,0f,0f,transitionSpeed * Time.deltaTime);


		
	}
}
