using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loops : MonoBehaviour {
	public string _dest;
	public float _timeBetweenFrames;
	public float _shiftSpeed;

	private Texture[] _frames;

	public int _ind;
	private float _timeSince;

	public bool _isSecond;

	// Use this for initialization
	void Start () {
		_frames = Resources.LoadAll<Texture> ("_loops/" + _dest);

		_timeSince = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (_timeSince >= _timeBetweenFrames) {
			if (_ind >= _frames.Length - 1)
				_ind = 0;
			else
				++_ind;

			_timeSince = 0f;
		}
		//_timeSince += Time.deltaTime;

		if (!_isSecond) {
			GetComponent<MeshRenderer> ().materials[0].color = new Color (GetComponent<MeshRenderer> ().materials[0].color.r,
				GetComponent<MeshRenderer> ().materials[0].color.g,
				GetComponent<MeshRenderer> ().materials[0].color.b,
				(_timeBetweenFrames - _timeSince) / _timeBetweenFrames);
		} else {
			GetComponent<MeshRenderer> ().materials[0].color = new Color (GetComponent<MeshRenderer> ().materials[0].color.r,
				GetComponent<MeshRenderer> ().materials[0].color.g,
				GetComponent<MeshRenderer> ().materials[0].color.b,
				(_timeSince) / _timeBetweenFrames);
		}

		GetComponent<MeshRenderer> ().materials[0].SetTexture("_BumpMap", _frames [_ind]);
		GetComponent<MeshRenderer> ().materials[0].mainTextureOffset = Vector3.up * Time.timeSinceLevelLoad * _shiftSpeed;
	}
}
