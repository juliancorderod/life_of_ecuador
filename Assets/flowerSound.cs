using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerSound : MonoBehaviour {

	FlowerController flowerController;
	GameOfLife gameOfLife;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {

		flowerController = GetComponent<FlowerController>();
		gameOfLife = GetComponent<GameOfLife>();
		audioSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {

		audioSource.pitch = gameOfLife.NUM_NEIGHBORS;

		Debug.Log(gameOfLife.NUM_NEIGHBORS);

	}
}
