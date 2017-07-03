using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class escToMenu : MonoBehaviour {

	float escTimer = 0;

	// Use this for initialization
	void Start () {
		GetComponent<Text>().color = new Color(0f,6f/255f,1f,0f);
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Escape)){
			escTimer +=Time.deltaTime;
			GetComponent<Text>().color += new Color(0f,0f,0f,Time.deltaTime *0.4f);
		} else {
			escTimer = 0f;
			GetComponent<Text>().color = new Color(0f,6f/255f,1f,0f);
		}


		if(escTimer >3f){
			GameObject g = GameObject.FindGameObjectWithTag ("Player");
			if (g != null)
				GameObject.Destroy (g);
			SceneManager.LoadScene ("_MENUSCREEN_v2");
		}

//		Debug.Log(escTimer);

	
		
	}
}
