using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour {
	public int NUM_NEIGHBORS;

	private Vector3[] DIRECTIONS;

	private float TIME_SINCE_CHECK;
	private float CHECK_TIME = .1f;

	private float _RADIUS = .5f;
	private int MAX_FLOWERS = 100;

	public GameObject pr_flower;

	// Use this for initialization
	void Start () {
		NUM_NEIGHBORS = 0;

		DIRECTIONS = new Vector3[6];
		DIRECTIONS [0] = transform.up;
		DIRECTIONS [1] = -transform.up;
		DIRECTIONS [2] = transform.right;
		DIRECTIONS [3] = -transform.right;
		DIRECTIONS [4] = transform.forward;
		DIRECTIONS [5] = -transform.forward;

		TIME_SINCE_CHECK = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent == null || transform.parent.name != "Main Camera") {
			if (TIME_SINCE_CHECK >= CHECK_TIME) {
				CheckNeighbors ();
				TIME_SINCE_CHECK = 0f;
			}
			TIME_SINCE_CHECK += Time.deltaTime;
		}
	}

	void CheckNeighbors(){
		NUM_NEIGHBORS = 0;

		for (int i = 0; i < DIRECTIONS.Length; i++) {
			if(Physics.OverlapSphere (transform.position + DIRECTIONS [i]*_RADIUS*2f * transform.localScale.x, _RADIUS*.75f* transform.localScale.x).Length > 0)
				NUM_NEIGHBORS += 1;
		}

		Life (NUM_NEIGHBORS);
	}

	void Life(int n){
		if (n < 1) {
			GameObject.Destroy (gameObject);
		} else if (n > 2) {
			GameObject.Destroy (gameObject);
		}
		else if(GameObject.FindGameObjectsWithTag("flower").Length < MAX_FLOWERS){
			int _dir = Random.Range (0, 6);

			if (Physics.OverlapSphere (transform.position + DIRECTIONS [_dir] * _RADIUS * 2f * transform.localScale.x, _RADIUS * .75f * transform.localScale.x).Length == 0) {
				GameObject newF = Instantiate (pr_flower, transform.position + DIRECTIONS [_dir] * _RADIUS * 2f * transform.localScale.x, transform.rotation) as GameObject;
			}
			
		}
	}
}
