using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour {
	private int NUM_NEIGHBORS;

	private Vector3[] DIRECTIONS;

	private float TIME_SINCE_CHECK;
	private float CHECK_TIME = .1f;

	private float _RADIUS = .5f;
	private int MAX_FLOWERS = 100;

	// Use this for initialization
	void Start () {
		NUM_NEIGHBORS = 0;

		DIRECTIONS = new Vector3[6];
		DIRECTIONS [0] = Vector3.up;
		DIRECTIONS [1] = Vector3.down;
		DIRECTIONS [2] = Vector3.right;
		DIRECTIONS [3] = Vector3.left;
		DIRECTIONS [4] = Vector3.forward;
		DIRECTIONS [5] = Vector3.back;

		TIME_SINCE_CHECK = 0f;
		_RADIUS *= transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (TIME_SINCE_CHECK >= CHECK_TIME){
			CheckNeighbors ();
			TIME_SINCE_CHECK = 0f;
		}

		TIME_SINCE_CHECK += Time.deltaTime;
	}

	void CheckNeighbors(){
		NUM_NEIGHBORS = 0;

		for (int i = 0; i < DIRECTIONS.Length; i++) {
			NUM_NEIGHBORS += Physics.OverlapSphere (transform.position + DIRECTIONS [i]*_RADIUS*2f, _RADIUS*.75f).Length;
		}

		Life (NUM_NEIGHBORS);
	}

	void Life(int n){
		if (n < 1)
			GameObject.Destroy (gameObject);
		else if (n > 2)
			GameObject.Destroy (gameObject);
		else if(GameObject.FindGameObjectsWithTag("flower").Length < MAX_FLOWERS){
			int _dir = Random.Range (0, 6);

			if (Physics.OverlapSphere (transform.position + DIRECTIONS [_dir]*_RADIUS*2f, _RADIUS*.75f).Length == 0)
				Instantiate (gameObject, transform.position + DIRECTIONS[_dir]*_RADIUS*2f, transform.rotation);
		}
	}
}
