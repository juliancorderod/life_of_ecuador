using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jointScript : MonoBehaviour {

	private Vector3 initPos, dancePos;
	public Vector3[] dancePosRange;
	float lerpVal, setPosTimer, lerpValDamp = 0f;
	public bool canDance, setDancePos;
	public Transform jointParent;

	GameObject playerScript;

	bool playerHasUs = false;

	public AudioSource jointAudio;
	float audioPitch;

	int _lerp0 = 0;
	int _lerp1 = 1;
	int _dir = 1;


	public GameObject[] ADJACENCIES;
	public Vector3[] adjacencyRange;
	private float AFFECTED_CONST = 2f;

	public Vector3[] _move;
	private int _ind;
	private bool _posSet;

	// Use this for initialization
	void Start () {
		playerScript = GameObject.FindGameObjectWithTag("Player");

		canDance = true;
		_ind = 0;


		initPos = transform.position;

		dancePosRange = new Vector3[10];
		dancePosRange[0]= Vector3.zero;
		dancePosRange[1]= Vector3.zero;
		dancePosRange[2]= Vector3.zero;
		dancePosRange[3]= Vector3.zero;
		dancePosRange[4]= Vector3.zero;
		dancePosRange[5]= Vector3.zero;
		dancePosRange[6]= Vector3.zero;
		dancePosRange[7]= Vector3.zero;
		dancePosRange[8]= Vector3.zero;
		dancePosRange[9]= Vector3.zero;

		adjacencyRange = new Vector3[10];
		_move = new Vector3[10];
	
	}
	
	// Update is called once per frame
	void Update () {
		if(playerScript.GetComponent<player>().HeldObjectName == this.name){
			if (canDance) {
				canDance = false;

				if (_dir == 1)
					_ind = _lerp0;
				else
					_ind = _lerp1;

				setPosTimer = 0f;
			}
			setDancePos = true;
			playerHasUs = true;
			_posSet = false;

		/////
			float adjacentLine = Vector3.Distance(Camera.main.transform.position,initPos);
			float angle = Vector3.Angle(initPos - Camera.main.transform.position, Camera.main.transform.forward) * Mathf.Deg2Rad;
			angle = Mathf.Clamp (angle, 0f, Mathf.PI/4f);

			float zVal = adjacentLine/Mathf.Cos(angle);

			transform.position = Camera.main.ScreenToWorldPoint(
				new Vector3(Camera.main.pixelWidth/2  ,Camera.main.pixelHeight/2 ,zVal));
		/////
		} 




		if(playerHasUs){
			if (playerScript.GetComponent<player> ().HeldObjectName != this.name) {
				canDance = true;
				setPosTimer = 0f;
				jointAudio.volume = 0;
				playerHasUs = false;
			} else {
				if (setPosTimer == 0f)
					AdjustPos (_ind);

				setPosTimer += Time.deltaTime;

				if (setPosTimer >= 1f) {
					Debug.Log (_ind);
					setPosTimer = 0f;

					if(_dir == 1){
						if (_ind >= dancePosRange.Length - 1){
							_ind = 0;
							audioPitch = 0;
						}
						else
							++_ind;
					}
					else{
						if (_ind <= 0){
							_ind = dancePosRange.Length - 1;
							audioPitch = 0;
						}
						else
							--_ind;
					}
				}
				jointAudio.volume = 1;
				jointAudio.pitch = audioPitch /2;

			}
		}
	


		if(transform.parent == null){
			transform.parent = jointParent;
		}

		if(setDancePos){
			dancePos = transform.position;
			setDancePos = false;

		}



		if(canDance){
			float _speed = 5f;

			if (lerpValDamp != 0f) {
				lerpValDamp = 0f;
			}

			lerpVal += Time.deltaTime*_speed;

			lerpVal = Mathf.Clamp01(lerpVal);

			if(_dir == 1)
				transform.position = initPos + Vector3.Slerp(_move[_lerp0], _move[_lerp1], lerpVal);
			else
				transform.position = initPos + Vector3.Slerp(_move[_lerp1], _move[_lerp0], lerpVal);

			if(lerpVal >= 1f){
				if(_dir == 1){
					if(_lerp1 >= dancePosRange.Length-1){
						_dir *= -1;

					}

				}
				else{
					if(_lerp0 <= 0){
						_dir *= -1;
					
					}
				}
					
				_lerp0 += _dir;
				_lerp1 += _dir;

				lerpVal = 0f;
			}
				

			lerpValDamp += Time.deltaTime * 0.00001f;

			for(int i = 0; i < dancePosRange.Length; i++){
				dancePosRange[i] = Vector3.Lerp(dancePosRange[i], Vector3.zero, lerpValDamp);

				Vector3 _adjSum = Vector3.zero;
				foreach (GameObject g in ADJACENCIES)
					_adjSum += g.GetComponent<jointScript> ()._move [i];
				adjacencyRange [i] = _adjSum / ADJACENCIES.Length;

				_move [i] = (dancePosRange [i] + AFFECTED_CONST * adjacencyRange [i]) / (1f + AFFECTED_CONST);
			}

//			for (int a = 0; a < adjacencyRange.Length; a++) {
//				Vector3 _adjSum = Vector3.zero;
//
//				foreach (GameObject g in ADJACENCIES)
//					_adjSum += g.GetComponent<jointScript> ().dancePosRange [a];
//
//				adjacencyRange [a] = _adjSum / ADJACENCIES.Length;
//				_move [a] = (dancePosRange [a] + AFFECTED_CONST * adjacencyRange [a]) / (1f + AFFECTED_CONST);
//			}


		}
		
	}

	void AdjustPos(int _pos){
		dancePosRange[_pos] = (transform.position - initPos);
		++audioPitch;


	}
}
