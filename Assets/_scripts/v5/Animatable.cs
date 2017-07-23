using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatable : MonoBehaviour {
	public string _name;

	//all exported with 0 delay
	public float _frame_delay;

	public bool _ready;
	private Sprite[] _frames;

	private SpriteRenderer _rend;

	private int _ind;
	private float _time;
	private int _dir;

	// Use this for initialization
	void Start () {
		_ready = false;

		_rend = GetComponent<SpriteRenderer> ();

		if (_rend != null) {
			_frames = Resources.LoadAll<Sprite> ("_loops/" + _name);

			if (_frames.Length > 0) {
				if (_frames.Length > 1) {
					_ready = true;
					if (Random.Range (0, 2) == 0)
						_dir = 1;
					else
						_dir = -1;
					
					_ind = Random.Range (0, _frames.Length);
					_time = 0f;
				}
				
				GetComponent<SpriteRenderer> ().sprite = _frames [0];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_ready) {
			if (_time >= _frame_delay) {
				if ((_ind+_dir) >= _frames.Length - 1 || (_ind+_dir) <= 0)
					_ind = 0;
				else
					_ind += _dir;

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
