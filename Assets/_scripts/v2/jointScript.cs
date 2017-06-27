using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jointScript : MonoBehaviour {

	private Vector3 initPos, dancePos;
	private Vector3[] dancePosRange;
	float lerpVal, setPosTimer, lerpValDamp = 0f;
	public bool canDance, setDancePos;
	public Transform jointParent;

	public GameObject playerScript, mainBody;

	bool playerHasUs = false;



	int _lerp0 = 0;
	int _lerp1 = 1;
	int _dir = 1;

	// Use this for initialization
	void Start () {

		initPos = transform.position;

		dancePosRange = new Vector3[10];
		dancePosRange[0]= initPos;
		dancePosRange[1]= initPos;
		dancePosRange[2]= initPos;
		dancePosRange[3]= initPos;
		dancePosRange[4]= initPos;
		dancePosRange[5]= initPos;
		dancePosRange[6]= initPos;
		dancePosRange[7]= initPos;
		dancePosRange[8]= initPos;
		dancePosRange[9]= initPos;
	
	}
	
	// Update is called once per frame
	void Update () {



	

		if(playerScript.GetComponent<player>().HeldObjectName == this.name){
			canDance = false;
			setDancePos = true;
			playerHasUs = true;

			setPosTimer += Time.deltaTime;
		

			float adjacentLine = Vector3.Distance(Camera.main.transform.position,initPos);
			float angle = Vector3.Angle(initPos - Camera.main.transform.position, Camera.main.transform.forward) * Mathf.Deg2Rad;
			angle = Mathf.Clamp (angle, 0f, Mathf.PI/4f);



			float zVal = adjacentLine/Mathf.Cos(angle);


			Debug.Log(angle);
			transform.position = Camera.main.ScreenToWorldPoint(
				new Vector3(Camera.main.pixelWidth/2  ,Camera.main.pixelHeight/2 ,zVal));

			for(int i = 0; i < dancePosRange.Length; i++){
				if(setPosTimer < i  && setPosTimer > i-1 ){
					dancePosRange[i] = transform.position;
					Debug.Log("set" + i);

				}
			}
		} 


		if(playerHasUs){
			if(playerScript.GetComponent<player>().HeldObjectName != this.name){
				canDance = true;
				setPosTimer = 0f;
				lerpValDamp = 0f;
				playerHasUs = false;

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


			lerpVal += Time.deltaTime*_speed;

			lerpVal = Mathf.Clamp01(lerpVal);

			if(_dir == 1)
				transform.position = Vector3.Slerp(dancePosRange[_lerp0],dancePosRange[_lerp1], lerpVal);
			else
				transform.position = Vector3.Slerp(dancePosRange[_lerp1],dancePosRange[_lerp0], lerpVal);

			if(lerpVal >= 1f){
				if(_dir == 1){
					if(_lerp1 >= dancePosRange.Length-1)
						_dir *= -1;
				}
				else{
					if(_lerp0 <= 0)
						_dir *= -1;
				}
					
				_lerp0 += _dir;
				_lerp1 += _dir;

				lerpVal = 0f;
			}
				

			lerpValDamp += Time.deltaTime * 0.00001f;

			for(int i = 0; i < dancePosRange.Length; i++){
		
				dancePosRange[i] = Vector3.Lerp(dancePosRange[i],initPos, lerpValDamp);
			}
		}





		
	}

}
