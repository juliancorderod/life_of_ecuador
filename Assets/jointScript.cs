using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jointScript : MonoBehaviour {

	private Vector3 initPos, dancePos;
	float lerpVal;
	public bool canDance, setDancePos;
	public Transform jointParent;

	public GameObject playerScript;

	bool playerHasUs = false;




	// Use this for initialization
	void Start () {

		initPos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {



		if(playerScript.GetComponent<player>().HeldObjectName == this.name){
			canDance = false;
			setDancePos = true;
			playerHasUs = true;

		} 


		if(playerHasUs){
			if(playerScript.GetComponent<player>().HeldObjectName != this.name){
				canDance = true;
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

			transform.position = Vector3.Lerp(dancePos,initPos,lerpVal);
		}

		lerpVal = Mathf.Sin(Time.time*4) *0.5f +0.5f;





		
	}

}
