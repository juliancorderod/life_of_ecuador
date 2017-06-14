using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _sphere : MonoBehaviour {
	public float _speed;

	private Vector3 _v;

	// Use this for initialization
	void Start () {
		_v = Random.insideUnitSphere;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (_v, Time.deltaTime * _speed, Space.World);
	}
}
