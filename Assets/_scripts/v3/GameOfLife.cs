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
		DIRECTIONS [0] = Vector3.up;
		DIRECTIONS [1] = Vector3.down;
		DIRECTIONS [2] = Vector3.right;
		DIRECTIONS [3] = Vector3.left;
		DIRECTIONS [4] = Vector3.forward;
		DIRECTIONS [5] = Vector3.back;

		TIME_SINCE_CHECK = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.tag != "active") {
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
		if (n < 1 && GetComponent<FlowerController>()._inComputer)
			GameObject.Destroy (gameObject);
		else if (n > 2 && GetComponent<FlowerController>()._inComputer)
			GameObject.Destroy (gameObject);
		else if(GameObject.FindGameObjectsWithTag("flower").Length < MAX_FLOWERS){
			int _dir = Random.Range (0, 6);
			bool _inCPU = CheckIfInComputer (transform.position + DIRECTIONS [_dir] * _RADIUS * 2f * transform.localScale.x, _RADIUS * .75f * transform.localScale.x);
			int _length = 0;

			if (_inCPU)
				_length = 1;

			if (Physics.OverlapSphere (transform.position + DIRECTIONS [_dir] * _RADIUS * 2f * transform.localScale.x, _RADIUS * .75f * transform.localScale.x).Length == _length) {
				GameObject newF = Instantiate (pr_flower, transform.position + DIRECTIONS [_dir] * _RADIUS * 2f * transform.localScale.x, transform.rotation) as GameObject;
			}
			
		}
	}

	bool CheckIfInComputer(Vector3 _pos, float _rad){
		Collider[] _overlaps = Physics.OverlapSphere (_pos, _rad);

		bool _hitCPU = false;

		if(_overlaps.Length > 0){
			for (int i = 0; i < _overlaps.Length; i++) {
				if (_overlaps [i].name == "computer") {
					_hitCPU = true;
					break;
				}
			}
		}

		return _hitCPU;
	}
}
