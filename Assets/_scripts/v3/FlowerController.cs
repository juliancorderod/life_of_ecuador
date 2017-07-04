using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour {
	public bool _inComputer;

	private GameObject _computer;
	// Use this for initialization
	void Start () {
		_computer = GameObject.Find ("computer");

		if (_computer != null) {
			transform.parent = _computer.transform;

			_inComputer = InComputer ();
		} else
			_inComputer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_inComputer) {
			if (transform.localScale.x < .5f) {
				transform.localScale = Vector3.one * .05f;
				transform.parent = _computer.transform;
			}
		} else {
			if (transform.localScale.x >= .5f) {
				transform.localScale = Vector3.one;
				transform.parent = null;
			}
		}
	}

	bool InComputer(){
		if (Mathf.Abs (transform.localPosition.x) >= .5f || Mathf.Abs (transform.localPosition.z) >= .5f || Mathf.Abs (transform.localPosition.y) >= .5f)
			return false;
		else
			return true;
	}

	void OnCollisionEnter(Collision col){
		if (transform.tag != "active") {
			if (col.collider.gameObject.name == "computer") {
				GameObject.Destroy (gameObject);
			}
		}
	}
}
