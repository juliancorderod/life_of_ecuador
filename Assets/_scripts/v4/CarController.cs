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
	private float _maxDistance = 400f;

	public bool inCar = false;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<player>();

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
			}

			Quaternion targetRotation = Quaternion.LookRotation (transform.position - new Vector3(_object.transform.position.x, transform.position.y, _object.transform.position.z));
			float str = Mathf.Min (_lerp_speed * 2.5f * Time.deltaTime, 1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);

			_angle = transform.eulerAngles;
				
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
		
		if (col.gameObject.name == "player"){
			inCar = true;
			col.transform.parent = transform;
			//DontDestroyOnLoad(transform.gameObject);
		}
	}


	void OnTriggerExit(Collider col){
		if (col.gameObject.name == "player"){
			inCar = false;
			col.transform.parent = null;

		}
	}
}
