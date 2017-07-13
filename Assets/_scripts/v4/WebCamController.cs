using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class WebCamController : MonoBehaviour {
	private Renderer rend;

	public MovieTexture[] _vids;
	public float _timeBetweenVideos;

	private float _timeStart;
	private float _timeEnd;

	private MovieTexture _tex;

	public int _ind;

	private bool _off;
	private bool _started;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();

		_ind = 0;

		_started = false;
		_off = false;

		_timeStart = 0f;
		_timeEnd = 0f;

		GetComponent<MeshManipulation> ().IND = _ind;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && !_started) 
			_started = true;

		if (_started) {
			if (!_off)
				Playing (_ind);
			else
				Stopping (_ind);
		}
	}

	void Playing(int i){
		float _stop = _vids [i].duration;

		rend.material.mainTexture = _vids [i];
		_tex = (MovieTexture)rend.material.mainTexture;

		if (_timeStart >= _stop) {
			_tex.Stop ();
			_off = true;
			_timeStart = 0f;
			GetComponent<MeshManipulation> ().Increment (0, _ind);
		} else {
			_timeStart += Time.deltaTime;

			if (!_tex.isPlaying) {
				_tex.Play ();
				GetComponent<AudioSource> ().clip = _tex.audioClip;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	void Stopping(int i){
		float _stop = _timeBetweenVideos;

		rend.material.mainTexture = null;
		_tex = null;

		if (_timeEnd >= _stop) {
			if (i >= _vids.Length - 1) {
				_ind = 0;
				GetComponent<MeshManipulation> ().IND = _ind;
			}
			else
				++_ind;

			_off = false;
			_timeEnd = 0f;
		} else
			_timeEnd += Time.deltaTime;
	}
}
