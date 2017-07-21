using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	private GameObject _followedObj;

	private float _move_Speed = 1f;



	// Use this for initialization
	void Start () {
		_followedObj = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Camera> ().nearClipPlane = Mathf.Max (.01f, Vector3.Distance (transform.position, _followedObj.transform.position) - 2f);
	}


}
