﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{

	Animator playerAnimator;

	public player playerScript;
	public Transform neck;
	public GameObject playerCam;

	Vector3 neckInitPos, neckInitRot;

	// Use this for initialization
	void Start ()
	{

		playerAnimator = GetComponent<Animator> ();

		neckInitPos = neck.position;
		neckInitRot = neck.localEulerAngles;

	}
	
	// Update is called once per frame
	void Update ()
	{



		if(playerScript.isCrouching){
			if(Input.GetKey (KeyCode.Space)) {

				if (playerScript.HeldObject == null)
					playerAnimator.Play ("crouchWalk");
				else
					playerAnimator.Play ("crouchWalkNoArms");
			}

		} else if(Input.GetKey (KeyCode.V) && !playerScript.isCrouching){
			if (playerScript.HeldObject == null)
				playerAnimator.Play ("run");
			else
				playerAnimator.Play ("runNoArms");

		} else if (Input.GetKey (KeyCode.Space) && !playerScript.isCrouching) {

			if (playerScript.HeldObject == null)
				playerAnimator.Play ("walk1");
			else
				playerAnimator.Play ("walkNoArms");
		} else if (playerScript.leftRightLook != 0) {
			if (playerScript.HeldObject != null){
				if (playerScript.leftRightLook > 0){
					playerAnimator.Play ("turningRightNoArms");//ADD STUFF ABOUT ANIM SPEED FASTER IF TURNING FASTER
					//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
				}else {
					playerAnimator.Play ("turningLeftNoArms");
					//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
				}
			}else if (playerScript.leftRightLook > 0){
				playerAnimator.Play ("turningRight");
				//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
			}else{
				playerAnimator.Play ("turningLeft");
				//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
			}
		} else{

			if(playerScript.isCrouching)
				playerAnimator.Play ("emptyAnimCrouch");
			else
				playerAnimator.Play ("emptyAnim");
		}
		
	}

	void LateUpdate(){

		neck.localEulerAngles = playerCam.transform.localEulerAngles + neckInitRot;

	}
}
