using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExhaustController : MonoBehaviour {
	private bool _ON;

	private int _IND;

	private float _TIME_AT_ON;
	private float _FRAME_DELAY = 0f;

	private Sprite[] _FRAMES;

	private CarController _sc;
	private float _lookSway = 1f;

	public bool _colliding;

	private ParticleSystem _p;
	private ParticleSystem.MainModule _m;

	// Use this for initialization
	void Start () {
		_ON = false;	

		_colliding = false;

		_FRAMES = Resources.LoadAll<Sprite> ("car_exhaust");

		_sc = GetComponentInParent<CarController> ();
		_p = GetComponent<ParticleSystem> ();


		_IND = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Sprite s = null;

		if (_ON) {
			//GetComponent<ParticleSystem> ().
//			_TIME_AT_ON += Time.deltaTime;
//
//			if (_TIME_AT_ON >= _FRAME_DELAY) {
//				_TIME_AT_ON = 0f;
//
//				if (_IND >= _FRAMES.Length - 1)
//					_IND = 0;
//				else
//					++_IND;
//			}
//
//			if(!_colliding)
//				s = _FRAMES [_IND];
			//_p.loop = true;
		} else
			//_p.loop = false;

		//GetComponent<SpriteRenderer> ().sprite = s;

		transform.LookAt (transform.position - _sc._move_vector + Vector3.down*_lookSway);
	}

	public void SetState(bool b){
		if (b) {
			if (!_ON) {
				_IND = 0;
				_TIME_AT_ON = 0f;
				_ON = true;
			}
		} else {
			if (_ON) {
				_ON = false;
			}
		}
	}
}
