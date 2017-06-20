using System.Collections;
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

	public GameObject pointer;

	public Camera object_camera;

	// Use this for initialization
	void Start ()
	{

		control = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;

		control.Move (transform.up * -Time.deltaTime * 20f);//gravity

		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		if (!lookatObject) {//this is so camera doesnt jump when you stop looking at objects
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
//			if (Input.GetKey (KeyCode.Space) && control.isGrounded) {//HAVE TO FIX THIS/ DO WE WANT IT?
//				control.Move (transform.up * Time.deltaTime * 100f);
//			}

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
				//Camera.main.transform.localPosition = new Vector3 (0, 0.75f, 0);
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
		Physics.Raycast (playerRay, out hit, 3, layerMask);

		if (Input.GetMouseButtonDown (0) && carryingObject) {//drop object
			Debug.Log ("drop");
			if(col.gameObject.GetComponent<Rigidbody>() == true){
				col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			}
			col.transform.parent = null;
			col.transform.localPosition = Camera.main.transform.position +  (Camera.main.transform.forward * 2f);
			//control.center = new Vector3 (0, 0, 0);
			carryingObject = false;
			lookatObject = false;
			col.gameObject.GetComponent<thing>().SetCloneActive(false);
			if (col.transform.childCount > 0) {
				for (int i = 0; i < col.transform.childCount; i++)
					col.transform.GetChild (i).gameObject.layer = 8;
			}
			col.layer = 8;
			col = null;
		}

		//this is so the raycast only hits stuff on the 'pickable' layer 
		if (hit.collider != null && !carryingObject) {
			if (hit.collider != null) {//if hit something
				col = hit.collider.gameObject;
				//pointer.GetComponent<Image>().color = Color.red;
				col.gameObject.GetComponent<thing>().SetCloneActive(true);//sets the outline object on when raycast is colliding

				//Grab object if mouse clicked (also freezes object at center of screen and slightly moves the player's collision box so object doesn't go through walls)
				if (Input.GetMouseButtonDown (0) && !carryingObject && hit.collider != null && HeldObjectName == "") {
					if (!lookatObject) {
						Debug.Log ("hit");
						col.transform.parent = Camera.main.transform;
						col.transform.localPosition = new Vector3 (0.5f, -0.7f, 1.3f);
						col.transform.localEulerAngles = new Vector3 (0, 0, 0);
					}
					//control.center = new Vector3 (0, 0, 0.5f);
					carryingObject = true;
					if (col.transform.childCount > 0) {
						for (int i = 0; i < col.transform.childCount; i++)
							col.transform.GetChild (i).gameObject.layer = 9;
					}
					col.layer = 9;
					if(col.gameObject.GetComponent<Rigidbody>() != null){
						col.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
						//col.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
					}

					col.gameObject.GetComponent<thing>().SetCloneActive(false);
				}
			} 

		} else if (!lookatObject && !carryingObject) {
			//	pointer.GetComponent<Image>().color = Color.white;
			if (col != null) {
				col.gameObject.GetComponent<thing>().SetCloneActive(false);//if raycast not colliding dont show outline
				if (col.transform.childCount > 0) {
					for (int i = 0; i < col.transform.childCount; i++)
						col.transform.GetChild (i).gameObject.layer = 8;
				}
				col.layer = 8;
			}
			//fuck

			col = null;

		}
		if (Input.GetMouseButton (1) && carryingObject || Input.GetKey (KeyCode.LeftControl) && carryingObject) {//if pressed ctrl or right mouse while carrying object, look at it
			lookatObject = true;
			object_camera.orthographic = true;
			object_camera.orthographicSize = col.GetComponent<MeshFilter> ().sharedMesh.bounds.size.magnitude / 2f;
		} else if (carryingObject) {//if not holding ctrl or right mouse stop looking

			if(lookatObject)
				col.transform.localPosition = new Vector3 (0.5f, -0.7f, 1f);
			
			lookatObject = false;
			object_camera.orthographic = false;


			col.gameObject.GetComponent<thing>().SetCloneActive(false);
		

		}


		if (lookatObject) {//look at object
			col.gameObject.GetComponent<thing>().SetCloneActive(false);

			leftRightLookObj = Input.GetAxis ("Mouse X") * Time.deltaTime * rotationVal;
			upDownLookObj = Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationVal;

			col.transform.localPosition = new Vector3 (0f, 0f, 8f);

			col.transform.Rotate (transform.up , -leftRightLookObj, Space.World);
			col.transform.Rotate (transform.right, upDownLookObj, Space.World);
		}

		if (carryingObject)
			HeldObjectName = col.name;
		else
			HeldObjectName = "";
	}
		
}
