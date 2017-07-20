using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraHolder : MonoBehaviour {
	private GameObject _followed;

	//angle code
	private float _angleSpeed = 1.5f;
	private float _dirSpeed = 3f;

	private float _angleChange;
	public float _direction;

	//pos code

	void Awake(){
		_followed = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		if (transform.parent != null)
			transform.parent = null;

		_angleChange = 0f;
		_direction = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = _followed.transform.position;

		_angleChange = DecideAngle ();

		_direction += (Mathf.Sign (_angleChange) - _direction) * Time.deltaTime * _dirSpeed;
		_angleChange = Mathf.Clamp(Mathf.Abs (_angleChange), 0f, 45f);

		transform.Rotate (Vector3.up, Time.deltaTime * _direction * _angleChange * _angleSpeed, Space.World);
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
