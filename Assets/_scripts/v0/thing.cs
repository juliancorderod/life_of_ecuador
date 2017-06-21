using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thing : MonoBehaviour {
	public GameObject _zFightPrefab;

	private Vector3 _scale;
	private Quaternion _rotation;
	private Vector3 _position;

	private GameObject _clone;
	private float _cloneAlpha;
	private bool _cloneActive = false;

	private bool _cloneMesh = false;
	// Use this for initialization
	void Start () {
		_scale = transform.localScale;
		_rotation = transform.rotation;
		_position = transform.position;


		_clone = Instantiate (_zFightPrefab, _position, _rotation) as GameObject;
		_clone.transform.localScale = _scale*1.00001f;
		_clone.transform.parent = transform;

		_cloneAlpha = _clone.GetComponent<MeshRenderer> ().material.color.a;

		SetClone ();
	}

	void Update(){
		if(!_cloneMesh){
			_clone.GetComponent<MeshFilter> ().sharedMesh = GetComponent<MeshFilter> ().sharedMesh;
			_cloneMesh = true;
		}
	}

	public void SetCloneActive(bool _b){
		_cloneActive = _b;

		SetClone ();
	}

	void SetClone(){
		if (_cloneActive && !_clone.activeInHierarchy) {
			_clone.GetComponent<MeshRenderer> ().materials[0].color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), _cloneAlpha);
			_clone.GetComponent<MeshRenderer> ().materials[1].color = _clone.GetComponent<MeshRenderer> ().materials[0].color;
			_clone.SetActive (true);
		} else if (!_cloneActive && _clone.activeInHierarchy) {
			_clone.GetComponent<MeshRenderer> ().materials[0].color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), _cloneAlpha);
			_clone.GetComponent<MeshRenderer> ().materials[1].color = _clone.GetComponent<MeshRenderer> ().materials[0].color;
			_clone.SetActive (false);
		}

	}
}
