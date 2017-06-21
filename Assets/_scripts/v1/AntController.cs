using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {
	public GameObject[] _body0;
	public GameObject[] _body1;

	public float _legSpeed;
	public float _legArc;

	private float _timeAtLerp;

	private bool _startedLerp;
	private bool _moving;

	public bool hasObject;
	private bool grounded;

	private float _groundDistance = .25f;
	private float _turnDistance = 2f;

	public float _moveSpeed;
	public float _turnSpeed;

	private Vector3 _newPos;
	private float _turn;
	private Vector3 _newRot;

	private float _lerpSpeed = 3f;

	// Use this for initialization
	void Start () {
		_moving = true;
		_startedLerp = false;
		transform.eulerAngles = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (_moving) {
			Move (hasObject);
			if (_startedLerp) {
				if (_body0.Length > 0)
					MoveLegs (_body0, 1f);
				if (_body1.Length > 0)
					MoveLegs (_body1, -1f);
			} else {
				StartLerp ();
			}
		} else {
			_startedLerp = false;

			if (_body0.Length > 0)
				ResetLegs (_body0);
			if (_body1.Length > 0)
				ResetLegs (_body1);
		}
	}

	void StartLerp(){
		_timeAtLerp = Time.time;
		_startedLerp = true;
	}

	void MoveLegs(GameObject[] g, float _d){
		float _t = Time.time - _timeAtLerp;

		for (int i = 0; i < g.Length; i++)
			g [i].transform.localEulerAngles = Vector3.forward * _d * Mathf.Sin (_t * _legSpeed) * _legArc;
	}

	void ResetLegs(GameObject[] _r){
		for (int i = 0; i < _r.Length; i++)
			_r [i].transform.localEulerAngles = Vector3.zero;
	}

	void Move(bool _hasObject){
		Ray _groundRay = new Ray (transform.position - transform.up / 10f, -transform.up);
		RaycastHit _groundHit = new RaycastHit ();

		Debug.DrawRay (_groundRay.origin, _groundRay.direction * _groundDistance, Color.red);

		grounded = Physics.Raycast (_groundRay, out _groundHit, _groundDistance);



		if (grounded) {
			Debug.Log (_groundHit.collider.name);
			//_newPos = _groundHit.point + _groundHit.normal*_groundDistance/2f;
			//transform.up = _groundHit.normal;

			Ray _forwardRay = new Ray (transform.position + transform.forward, transform.forward);
			RaycastHit _forwardHit = new RaycastHit ();
			Ray _objectRay = new Ray (transform.position + transform.forward, transform.forward);
			RaycastHit _objectHit = new RaycastHit ();

			Physics.Raycast (_forwardRay, out _forwardHit, 1000f, 1 << 0);

			if (Physics.Raycast (_objectRay, out _objectHit, .05f, 1 << 8) && _objectHit.transform.tag != "ant")
				AssignObject (_objectHit.collider.gameObject);

			transform.Rotate(transform.up, Time.deltaTime * _turnSpeed * (4f * Mathf.Pow(_turnDistance / _forwardHit.distance, 2f)), Space.World);


			transform.position += transform.forward * Time.deltaTime * _moveSpeed;

			//_newRot = transform.eulerAngles + Vector3.up * _turn;

			//transform.position = Vector3.Lerp (transform.position, _newPos, Time.deltaTime * _lerpSpeed * 5f);
			//transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, _newRot, Time.deltaTime * _lerpSpeed * .1f);
		} else {
			transform.position -= Vector3.up * Time.deltaTime * 1f;
		}
	}

	void AssignObject(GameObject g){
		if (!hasObject && grounded) {
			g.transform.position = transform.position + Vector3.up * g.GetComponent<MeshFilter> ().mesh.bounds.size.y / 2f + Vector3.up*.1f;
			g.transform.eulerAngles = Random.insideUnitSphere * 90f;
			g.transform.parent = transform;
			g.layer = 0;
			hasObject = true;
		}
	}
}
