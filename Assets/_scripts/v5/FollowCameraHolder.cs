using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraHolder : MonoBehaviour {
	private GameObject _followed;

	public float _angle;
	private float _lerpSpeed = .5f;
	private float _angleChange;

	float _x;
	float _minDistMove = 5f;

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

		_angleChange = DecideAngle ();

		transform.Rotate (Vector3.up, Time.deltaTime * _angleChange * _lerpSpeed, Space.World);
	}

	float DecideAngle(){
		float _a = _followed.transform.eulerAngles.y;
		float _b = transform.eulerAngles.y;
		float _sign_a = Mathf.Sign (_a);
		float _sign_b = Mathf.Sign (_b);

		float _distance = 0f;
		float _dir = 0f;

		if (_sign_a == _sign_b) {
			_distance = Mathf.Abs (_a - _b);

			_dir = Mathf.Sign (_a - _b);
		} else {
			_dir = 1f;

			if (_sign_b >= 0f) {
				_distance = (180f - _b) + (180f + _a);
			} else {
				_distance = Mathf.Abs (_b) + _a;
			}
		}

		if (_distance >= 180f) {
			return (_dir * -1f) * (360f - _distance);
		} 
		else {
			return _dir * _distance;
		}
	}
		
}
