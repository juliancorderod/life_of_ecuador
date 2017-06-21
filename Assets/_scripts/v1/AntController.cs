using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {
	public static float _dropAngle;
	private float _timeDrop;
	private float _minTimeDrop = 5f;
	private float _angleDist = 5f;

	public GameObject[] _body0;
	public GameObject[] _body1;

	public float _legSpeed;
	public float _legArc;

	private float _timeAtLerp;

	private bool _startedLerp;
	private bool _moving;

	public bool hasObject;
	private GameObject myObject;
	private bool grounded;
	private bool held;

	private float _groundDistance = .25f;
	private float _turnDistance = 2f;

	public float _moveSpeed;
	public float _turnSpeed;

	private Vector3 _newPos;
	private float _turn;
	private Vector3 _newRot;

	private float _lerpSpeed = 3f;

	private Vector2 _my2;

	// Use this for initialization
	void Start () {
		_dropAngle = Random.Range (0f, 180f);

		_moving = true;
		held = false;
		_startedLerp = false;
		_timeDrop = _minTimeDrop;
		transform.eulerAngles = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
		_my2 = new Vector2(transform.position.x, transform.position.z);
		_my2 = _my2.normalized;

		//Debug.Log (_dropAngle + "     " + Vector2.Angle (Vector2.one, _my2));
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

		if (transform.parent == null || transform.parent.name != "player")
			held = false;
		else
			held = true;
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

		//Debug.DrawRay (_groundRay.origin, _groundRay.direction * _groundDistance, Color.red);

		grounded = Physics.Raycast (_groundRay, out _groundHit, _groundDistance);


		if (!held) {
			if (grounded) {
				//Debug.Log (_groundHit.collider.name);
				//transform.up = _groundHit.normal;
				_timeDrop += Time.deltaTime;
				_newPos = _groundHit.point + transform.up;

				Ray _forwardRay = new Ray (transform.position + transform.forward, transform.forward);
				RaycastHit _forwardHit = new RaycastHit ();


				Physics.Raycast (_forwardRay, out _forwardHit, 1000f, 1 << 0);

				transform.Rotate (Vector3.up, Time.deltaTime * _turnSpeed * (Mathf.Pow (_turnDistance / _forwardHit.distance, 2f)), Space.World);


				transform.position += transform.forward * Time.deltaTime * _moveSpeed;

				if (_timeDrop >= _minTimeDrop)
					CheckObject ();

				//_newRot = transform.eulerAngles + Vector3.up * _turn;

				//transform.position = Vector3.Lerp (transform.position, _newPos, Time.deltaTime * _lerpSpeed * 5f);
				//transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, _newRot, Time.deltaTime * _lerpSpeed * 1f);
			} else {
				transform.position -= Vector3.up * Time.deltaTime * 1f;
				transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y, 0f);
			}
		}
	}

	void CheckObject(){
		Ray _objectRay = new Ray (transform.position + transform.forward, transform.forward);
		RaycastHit _objectHit = new RaycastHit ();

		if (Physics.Raycast (_objectRay, out _objectHit, .05f, 1 << 8) && _objectHit.transform.tag != "ant")
			AssignObject (_objectHit.collider.gameObject);

		if (Mathf.Abs (Vector2.Angle(Vector2.one, _my2) - _dropAngle) <= _angleDist)
			DropObject (myObject, transform.position);

	}

	void AssignObject(GameObject g){
		if (!hasObject && grounded) {
			g.transform.position = transform.position + Vector3.up * g.GetComponent<MeshFilter> ().mesh.bounds.size.y / 2f + Vector3.up*.1f;
			g.transform.eulerAngles = Random.insideUnitSphere * 90f;
			g.transform.parent = transform;
			g.GetComponent<Collider> ().enabled = false;
			g.layer = 0;
			hasObject = true;
			myObject = g;
		}
	}

	void DropObject(GameObject h, Vector3 v){
		float _distanceConst = 2f;

		if (h != null) {
			Vector2 r = Random.insideUnitCircle;

			h.transform.parent = null;
			h.transform.position = v + Vector3.forward*r.x*_distanceConst + Vector3.right*r.y*_distanceConst;
			h.transform.eulerAngles = Random.insideUnitSphere * 90f;
			h.GetComponent<Collider> ().enabled = true;
			hasObject = false;
			myObject = null;
			_timeDrop = 0f;
		}
	}
}
