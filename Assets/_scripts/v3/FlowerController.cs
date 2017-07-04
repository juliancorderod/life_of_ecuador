﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour {
	public bool _inComputer;

	private GameObject _computer;

	private SpriteRenderer[] _rends;
	private Vector3 _material_color;
	private float _alpha = 1f;

	public static int _numFlowers = 0;

	// Use this for initialization
	void Start () {
		_computer = GameObject.Find ("computer");

		if (_computer != null) {
			transform.parent = _computer.transform;

			_inComputer = InComputer ();
		} else
			_inComputer = false;

		++_numFlowers;

		_rends = GetComponentsInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_inComputer) {
			if (transform.localScale.x >= .5f) {
				transform.localScale = Vector3.one * .05f;
				transform.parent = _computer.transform;
			}
		} else {
			if (transform.localScale.x < .5f) {
				transform.localScale = Vector3.one;
				transform.parent = null;
			}
		}

		_material_color = Vector3.one * (_numFlowers / 100f);

		_rends [0].material.color = new Color (_material_color.x, _material_color.y, _material_color.z, _alpha);
		_rends [1].material.color = new Color (_material_color.x, _material_color.y, _material_color.z, _alpha);

		Debug.Log (_numFlowers);
	}

	bool InComputer(){
		if (Mathf.Abs (transform.localPosition.x) >= .5f || Mathf.Abs (transform.localPosition.z) >= .5f || Mathf.Abs (transform.localPosition.y) >= .5f)
			return false;
		else
			return true;
	}

	void OnCollisionEnter(Collision col){
		if (transform.parent == null || transform.parent.name != "Main Camera") {
			if (col.collider.gameObject.name == "computer") {
				--_numFlowers;
				GameObject.Destroy (gameObject);
			}
		}
	}
}
