using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	private GameObject _followedObj;

	private int _on;

	private float _move_Speed = 1f;

	private Vector3 _init_pos;

	private float _min_Z;
	private float _max_Z;

	private float _min_Y;
	private float _max_Y;

	// Use this for initialization
	void Start () {
		//toggle camera on/off
		_on = 2;

		_init_pos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			_on *= -1;
		}

		GetComponent<Camera> ().depth = _on - 1;

		//transform.localPosition = Vector3.Lerp(transform.localPosition, 
	}


}
