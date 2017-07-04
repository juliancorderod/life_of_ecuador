using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerSound : MonoBehaviour {

	FlowerController flowerController;
	GameOfLife gameOfLife;
	AudioSource audioSource;

	public SpriteRenderer flowerSide1, flowerSide2;

	// Use this for initialization
	void Start () {

		flowerController = GetComponent<FlowerController>();
		gameOfLife = GetComponent<GameOfLife>();
		audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

		if(flowerController._inComputer){
			audioSource.pitch = gameOfLife.NUM_NEIGHBORS;
			audioSource.volume = 0.1f;
		}else{
			audioSource.pitch = gameOfLife.NUM_NEIGHBORS + Random.Range(0.5f,1.5f);
			audioSource.volume = 0.5f;
		}
	}
}
