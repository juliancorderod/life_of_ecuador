using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPeople : MonoBehaviour {
	public GameObject pr_rotator;

	[Range(0, 25)]
	public int _numRotators;


	private GameObject[] rotators;

	// Use this for initialization
	void Start () {
		rotators = new GameObject[_numRotators];

		for (int i = 0; i < _numRotators; i++)
			rotators [i] = Instantiate (pr_rotator, transform.position, transform.rotation, transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
