using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	private GameObject _followedObj;

	private int _on;

	private float _move_Speed = 1f;



	// Use this for initialization
	void Start () {
		//toggle camera on/off
		_on = 2;


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			_on *= -1;
		}

		GetComponent<Camera> ().depth = _on - 1;
	}


}
