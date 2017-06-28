using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trianleScript : MonoBehaviour {

	public AudioClip triangleThump;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


				


		
	}

	public void playTriangleThump(){
		GetComponent<AudioSource>().clip = triangleThump;
		GetComponent<AudioSource>().volume = 1;
		GetComponent<AudioSource>().Play();
		//Debug.Log("playedDrop");
	}
}
