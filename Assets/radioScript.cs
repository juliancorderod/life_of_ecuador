using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioScript : MonoBehaviour {

	GameObject playerScript;

	bool playerHasUs = false;

	public AudioClip[] radioSongs;
	public AudioClip changingStationsAudio;
	AudioSource radioPlayer;
	int songIndex;

	// Use this for initialization
	void Start () {

		radioPlayer = GetComponent<AudioSource>();
		radioPlayer.Play();
		songIndex = Random.Range(0, radioSongs.Length);

		playerScript = GameObject.FindGameObjectWithTag("Player");

		
	}
	
	// Update is called once per frame
	void Update () {



		if(playerScript.GetComponent<player>().HeldObjectName == this.name)
			playerHasUs = true;
		else
			playerHasUs = false;


		if(playerHasUs){
			if(!radioPlayer.isPlaying){
				radioPlayer.Play();
				if(songIndex >= radioSongs.Length -1)
					songIndex = 0;
				else
					songIndex++;
			}
			radioPlayer.clip = changingStationsAudio;
			

		}else{
			if(!radioPlayer.isPlaying)
				radioPlayer.Play();
			radioPlayer.clip = radioSongs[songIndex];
		}
		


		//Debug.Log(songIndex);
		
	}
}
