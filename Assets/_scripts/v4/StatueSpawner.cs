using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueSpawner : MonoBehaviour {
	private float _startAngle;
	private float _endAngle;

	private float _minDistance = 10f;
	private float _maxDistance = 250f;

	public GameObject[] _STATUES;

	// Use this for initialization
	void Start () {
		_startAngle = 0f;
		_endAngle = Mathf.PI * 2f / 7f;

		GameObject _newStatue = null;
		float _angle = 0f;
		float _dist = 0f;
		Vector3 _pos = Vector3.zero;

		for (int i = 0; i < _STATUES.Length; i++) {
			_angle = Random.Range (_startAngle, _endAngle);
			_dist = Random.Range (_minDistance, _maxDistance);
			_pos = transform.position + Vector3.right * Mathf.Cos (_angle) * _dist + Vector3.forward * Mathf.Sin (_angle) * _dist;

			_newStatue = Instantiate (_STATUES [i], _pos + Vector3.up*4.2f, _STATUES [i].transform.rotation) as GameObject;
			_newStatue.transform.Rotate (Vector3.up, Random.Range (0f, 360f), Space.World);

			_startAngle += Mathf.PI * 2f / 7f;
			_endAngle += Mathf.PI * 2f / 7f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
