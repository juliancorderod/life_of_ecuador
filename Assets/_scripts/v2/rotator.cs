using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {
	public GameObject pr_person;

	[Range(0, 1000)]
	public int _numPeople;

	public Sprite[] _pos_people;

	[Range(0f, 100f)]
	public int _minDistance;
	[Range(0f, 100f)]
	public int _maxDistance;

	private float _minSpeed = 1f;
	private float _maxSpeed = 15f;

	private float _speed;

	// Use this for initialization
	void Start () {
		_speed = Random.Range (_minSpeed, _maxSpeed);

		if (Random.Range (0, 2) == 1)
			_speed *= -1f;

		float _angle = 0f;
		GameObject _newPerson = null;
		Vector3 _newPos = Vector3.zero;
		for (int j = 0; j < _numPeople; j++) {
			_angle = Random.Range (0f, Mathf.PI * 2f);
			_newPos = transform.position + Random.Range(_minDistance,_maxDistance)*(transform.forward * Mathf.Cos (_angle) + transform.right * Mathf.Sin (_angle));
			_newPerson = Instantiate (pr_person, _newPos, transform.rotation, transform) as GameObject;
			_newPerson.transform.LookAt (transform.position);
			_newPerson.transform.Rotate (_newPerson.transform.forward, Random.Range (0f, 360f), Space.World);
			_newPerson.GetComponent<SpriteRenderer> ().sprite = _pos_people [Random.Range (0, _pos_people.Length)];
			_newPerson.transform.localScale *= Random.Range (1f, 3f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	//	transform.Rotate (Vector3.up, Time.deltaTime * _speed, Space.World);
	}
}
