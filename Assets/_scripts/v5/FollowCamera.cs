using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	private int _on;

	private Vector3 _initLocal;
	private Vector3 _pos;

	private float _lerpSpeed = .5f;
	private GameObject _followedObj;

	void Awake(){
		GameObject.DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		//toggle camera on/off
		_on = 2;

		_initLocal = transform.localPosition;
		_followedObj = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			_on *= -1;
		}

		GetComponent<Camera> ().depth = _on - 1;

		//_pos = new Vector3(_initLocal.x, Mathf.Clamp(

		//transform.localPosition = Vector3.Lerp (transform.position, _pos, Time.deltaTime * _lerpSpeed);
	}
}
