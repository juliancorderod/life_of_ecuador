using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
	private player _player;
	private GameObject _object;

	private Vector3 _from_object;
	private Vector3 _move_vector;

	public float _speed;
	private float _lerp_speed = .5f;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find ("player").GetComponent<player>();

		_move_vector = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		_object = _player.HeldObject;

		if (_object != null) {
			_from_object = (_object.transform.position - transform.position).normalized;
			_from_object -= Vector3.up * _from_object.y;

			_move_vector = Vector3.Lerp (_move_vector, _from_object, _lerp_speed * Time.deltaTime);

			Debug.Log(Vector3.Angle(_move_vector, _from_object));
		} else {
			_from_object = Vector3.zero;
			_move_vector = _from_object;
		}

		transform.position += _move_vector * _speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "player")
			col.transform.parent = transform;
		else if (col.gameObject.layer == 8)
			col.transform.parent = transform;
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.name == "player")
			col.transform.parent = null;
	}
}
