using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
	private player _player;
	private GameObject _object;

	private Vector3 _from_object;
	public Vector3 _move_vector;

	public float _speed;
	private float _lerp_speed = .5f;

	private CarExhaustController[] _exhausts;

	private bool _started;
	private Vector3 _angle;
	private float _maxShake = .5f;

	public GameObject _brakeLights;
	private float _maxDistance = 900f;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find ("player").GetComponent<player>();

		_exhausts = GetComponentsInChildren<CarExhaustController> ();

		_move_vector = Vector3.zero;
		_started = false;
	}
	
	// Update is called once per frame
	void Update () {
		_object = _player.HeldObject;

		if (_object != null) {
			if (!_started) {
				_started = true;
				_angle = transform.eulerAngles;
			}
				
			_from_object = (_object.transform.position - transform.position).normalized;
			_from_object -= Vector3.up * _from_object.y;

			_move_vector = Vector3.Lerp (_move_vector, _from_object, _lerp_speed * Time.deltaTime);

			if (Mathf.Abs (transform.position.x + 191f) >= _maxDistance)
				transform.position = new Vector3(-Mathf.Sign(transform.position.x) * (2f*_maxDistance-50f) + transform.position.x, transform.position.y, transform.position.z);

			if(Mathf.Abs(transform.position.z + 172.7f) >= _maxDistance)
				transform.position = new Vector3(transform.position.x, transform.position.y, -Mathf.Sign(transform.position.z) * (2f*_maxDistance-50f) + transform.position.z);

			foreach (CarExhaustController c in _exhausts)
				c.SetState (true);

			if(Random.Range(0,4) == 0)
				transform.eulerAngles = _angle + Random.insideUnitSphere * _maxShake;
			else
				transform.eulerAngles = _angle;

			if (_brakeLights.activeInHierarchy)
				_brakeLights.SetActive (false);
		} else {
			_started = false;
			_from_object = Vector3.zero;
			_move_vector = _from_object;

			foreach (CarExhaustController c in _exhausts)
				c.SetState (false);

			if (!_brakeLights.activeInHierarchy)
				_brakeLights.SetActive (true);
		}

		transform.position += _move_vector * _speed * Time.deltaTime;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.layer == 8)
			col.transform.parent = transform;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player")
			col.transform.parent = transform;
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.name == "player")
			col.transform.parent = null;
	}
}
