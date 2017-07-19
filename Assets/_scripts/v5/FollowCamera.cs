using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	private int _on;

	private float _y;

	void Awake(){
		GameObject.DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		//toggle camera on/off
		_on = 2;

		_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			_on *= -1;
		}

		GetComponent<Camera> ().depth = _on - 1;

		transform.position = new Vector3 (transform.position.x, _y, transform.position.z);
	}
}
