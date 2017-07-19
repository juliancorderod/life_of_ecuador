using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraHolder : MonoBehaviour {
	private GameObject _followed;

	public float _angle;
	private float _lerpSpeed = 2f;
	private float _angleChange;

	float _x;

	void Awake(){
		_followed = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		if (transform.parent != null)
			transform.parent = null;

		_angle = DecideAngle ();
		_angleChange = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = _followed.transform.position;

		_x = Input.GetAxis ("Mouse X");


		if (Mathf.Abs (_x) < .1f)
			transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, _followed.transform.eulerAngles, Time.deltaTime * _lerpSpeed);
		else
			transform.Rotate (Vector3.up, _x * Time.deltaTime * 80f, Space.World);
	}

	float DecideAngle(){
		return (_followed.transform.eulerAngles.y - transform.eulerAngles.y);
	}
		
}
