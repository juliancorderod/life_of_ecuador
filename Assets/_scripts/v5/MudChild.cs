using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudChild : MonoBehaviour {
	private float _t;
	private float _delay;

	private bool _r;
	private float _range = 10f;

	// Use this for initialization
	void Start () {
		_t = 0f;

		_delay = GetComponent<Animatable> ()._frame_delay;
	}
	
	// Update is called once per frame
	void Update () {
		if (_t >= _delay) {
			Vector2 v = Random.insideUnitCircle * _range;
			transform.localPosition = new Vector3 (v.x, 0f, v.y);
			float a = Random.Range (0f, 259f);
			transform.localEulerAngles = new Vector3 (90f, 0f, a);
			_t = 0f;
		}
		_t += Time.deltaTime;
	}
}
