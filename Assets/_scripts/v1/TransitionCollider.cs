using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCollider : MonoBehaviour {
	public string _name;

	public GameObject _linkedRoom;
	public GameObject _linkedCollider;

	public bool _active;

	void Start(){
		if (_linkedRoom.activeSelf)
			_active = true;
		else
			_active = false;
	}

	void Update(){
		if (_active) {
			if (!_linkedRoom.activeSelf)
				_linkedRoom.SetActive (true);
		} else {
			if (_linkedRoom.activeSelf)
				_linkedRoom.SetActive (false);
		}
	}
		
	public bool GetState(){
		return _active;
	}

	public void SetState(bool b){
		_active = b;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			if(!_active)
				_active = true;

			if (_linkedCollider.GetComponent<TransitionCollider> ().GetState ())
				_linkedCollider.GetComponent<TransitionCollider> ().SetState (false);
		}
	}
}
