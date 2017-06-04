﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

	CharacterController control;

	[Range (40f, 100f)]
	public float rotationVal;
	public float playerSpeed, sprintSpeed, crouchSpeed;
	float horizontal, vertical, upDownLook, upDownLookObj, leftRightLook, leftRightLookObj;
	public bool isCrouching = false;


	RaycastHit hit;
	bool carryingObject = false;
	public string HeldObjectName = "";
	int layerMask = 1 << 8;
 	bool lookatObject = false;
	GameObject col;

	public Material outlineShader;

	public GameObject pointer;
	// Use this for initialization
	void Start ()
	{

		control = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;

		control.Move (transform.up * -Time.deltaTime * 10f);//gravity

		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		if(!lookatObject){//this is so camera doesnt jump when you stop looking at objects
			upDownLook -= Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationVal;
			upDownLook = Mathf.Clamp (upDownLook, -80f, 80f);
		}
		leftRightLook = Input.GetAxis ("Mouse X") * Time.deltaTime * rotationVal;




		if (!lookatObject) {//so when you're looking at stuff you cant move
			
			transform.Rotate (0, leftRightLook, 0);//player rotation left and right with mouse
			Camera.main.transform.eulerAngles = new Vector3 (upDownLook, transform.eulerAngles.y, 0);//cam rotation up down with mouse

			//movement stuff
			if (Input.GetKey (KeyCode.LeftShift)) {//if sprinting
				control.Move (transform.forward * Time.deltaTime * vertical * sprintSpeed);
				control.Move (transform.right * Time.deltaTime * horizontal * sprintSpeed);
			} else {
				if (isCrouching) {//if crouching move slower
					control.Move (transform.forward * Time.deltaTime * vertical * crouchSpeed);
					control.Move (transform.right * Time.deltaTime * horizontal * crouchSpeed);
				} else {//normal
					control.Move (transform.forward * Time.deltaTime * vertical * playerSpeed);
					control.Move (transform.right * Time.deltaTime * horizontal * playerSpeed);
				}
			}

			//jump
			if (Input.GetKey (KeyCode.Space) && control.isGrounded) {//HAVE TO FIX THIS/ DO WE WANT IT?
				control.Move (transform.up * Time.deltaTime * 100f);
			}

			//crouch stuff
			if (Input.GetKeyDown (KeyCode.C) && !isCrouching) {

				isCrouching = true;
			} else if (Input.GetKeyDown (KeyCode.C) && isCrouching) {

				isCrouching = false;
			}
			if (isCrouching) {
				control.height = 1;
				Camera.main.transform.localPosition = Vector3.zero;
			} else {
				control.height = 2;
				Camera.main.transform.localPosition = new Vector3 (0, 0.75f, 0);
			}
		}
			
		InteractionFunction ();//for when you wanna pick objects

		Debug.DrawRay (Camera.main.transform.position, Camera.main.transform.forward, Color.red);
		//Debug.Log(col);
	}

	void InteractionFunction ()
	{
		//this creates raycast in front of player used to grab objects
		Ray playerRay = new Ray (Camera.main.transform.position, Camera.main.transform.forward);

		//this is so the raycast only hits stuff on the 'pickable' layer 
		if (Physics.Raycast (playerRay, out hit, 3, layerMask)) {


			if (hit.collider != null) {//if hit something
				col = hit.collider.gameObject;
				//pointer.GetComponent<Image>().color = Color.red;
				col.transform.GetComponent<MeshRenderer>().materials[0].color = Color.black;//sets the outline object on when raycast is colliding

				//Grab object if mouse clicked (also freezes object at center of screen and slightly moves the player's collision box so object doesn't go through walls)
				if (Input.GetMouseButtonDown (0) && !carryingObject) {
					
						HeldObjectName = col.name;
							
						if(!lookatObject){
						col.transform.parent = Camera.main.transform;
						col.transform.localPosition = new Vector3 (0.5f, -0.8f, 1f);
						col.transform.localEulerAngles = new Vector3(0,0,0);
						}
						control.center = new Vector3 (0, 0, 0.5f);
						carryingObject = true;
						hit.rigidbody.constraints = RigidbodyConstraints.FreezeAll;

				}
			} 

		}else if(!lookatObject && !carryingObject){
		//	pointer.GetComponent<Image>().color = Color.white;


			if(col != null){
			col.transform.GetComponent<MeshRenderer>().materials[0].color = Color.clear;//if raycast not colliding dont show outline
			}

			col = null;
		}
		if (Input.GetMouseButton (1) && carryingObject || Input.GetKey(KeyCode.LeftControl) && carryingObject) {//if pressed ctrl or right mouse while carrying object, look at it
			lookatObject = true;
		} else if (carryingObject){//if not holding ctrl or right mouse stop looking

			Debug.Log ("stop looking");
			lookatObject = false;
			col.transform.localPosition = new Vector3 (0.5f, -0.8f, 1f);
		

		}


		if(lookatObject){//look at object
			leftRightLookObj += Input.GetAxis ("Mouse X") * Time.deltaTime * rotationVal;
			upDownLookObj += Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationVal;

			col.transform.localPosition = new Vector3(0f,0f,2f);

			//arreglar esto para que haga sentido bien (como que if x is bigger or smaller than 45 degrees, then leftrightlookobj changes y or z)
//			if(col.transform.localEulerAngles.x >= 315 && col.transform.localEulerAngles.x <= 45f){
//				col.transform.localEulerAngles = new Vector3 (upDownLookObj,-leftRightLookObj, 0f);
//			}
//			if(col.transform.localEulerAngles.x >= 45f && col.transform.localEulerAngles.x <= 135f){
//				col.transform.localEulerAngles = new Vector3 (upDownLookObj,leftRightLookObj, 0f);
//			}
//			if(col.transform.localEulerAngles.x >= 135f && col.transform.localEulerAngles.x <= 225){
//				col.transform.localEulerAngles = new Vector3 (upDownLookObj, 0f, leftRightLookObj);
//			}
//			if(col.transform.localEulerAngles.x >= 225 && col.transform.localEulerAngles.x <= 315){
				col.transform.localEulerAngles = new Vector3 (upDownLookObj, 0f, -leftRightLookObj);
		//	}
	
	


		}

		if (hit.collider == null && Input.GetMouseButtonDown (0) && carryingObject) {//drop object

			Debug.Log ("drop");
			Camera.main.transform.FindChild (HeldObjectName).GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			Camera.main.transform.FindChild (HeldObjectName).parent = null;
			control.center = new Vector3 (0, 0, 0);
			carryingObject = false;
			HeldObjectName = "";
			lookatObject = false;
			col.transform.GetComponent<MeshRenderer>().materials[0].color = Color.clear;
			col = null;

		}
	}
		
}
