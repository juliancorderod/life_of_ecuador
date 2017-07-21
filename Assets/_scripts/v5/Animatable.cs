using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatable : MonoBehaviour {
	public string _name;

	//all exported with 0 delay
	public float _frame_delay;

	private bool _ready;
	private Sprite[] _frames;

	private SpriteRenderer _rend;

	private int _ind;
	private float _time;

	// Use this for initialization
	void Start () {
		_ready = false;
		_ind = 0;
		_time = 0f;

		_rend = GetComponent<SpriteRenderer> ();

		if (_rend != null) {
			_frames = Resources.LoadAll<Sprite> ("_loops/" + _name);

			if (_frames.Length > 0) {
				if (_frames.Length > 1) 
					_ready = true;
				
				GetComponent<SpriteRenderer> ().sprite = _frames [0];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_ready) {
			if (_time >= _frame_delay) {
				if (_ind >= _frames.Length - 1)
					_ind = 0;
				else
					++_ind;

				_time = 0f;
				SetSprite ();
			}
			_time += Time.deltaTime;
		}
	}

	void SetSprite(){
		GetComponent<SpriteRenderer> ().sprite = _frames [_ind];
	}
}
