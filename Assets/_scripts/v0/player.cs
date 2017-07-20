using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{


	public CharacterController control;

	[Range (40f, 100f)]
	public float rotationVal;
	public float playerSpeed, sprintSpeed, crouchSpeed;
	public float vertical, upDownLook, upDownLookObj, leftRightLook, leftRightLookObj, rotating;
	public bool isCrouching = false;


	RaycastHit hit;
	bool carryingObject = false;
	public string HeldObjectName = "";
	public GameObject HeldObject;

	int layerMask = 1 << 8;
	bool lookatObject = false;
	public GameObject col;

	public GameObject pointer;

	public Camera object_camera;

	private Vector3 _rotatePos;
	private Vector3 _heldPos;

	public GameObject dropSounds;

	// Use this for initialization
	void Awake(){
		DontDestroyOnLoad(transform.gameObject);

	}

	void Start ()
	{
		
		control = GetComponent<CharacterController> ();
		rotating = -1f;
		_rotatePos = new Vector3 (0f, 0f, 240f);


	}
	
	// Update is called once per frame
	void Update ()
	{
//		if(pointer == null){
//			pointer = GameObject.Find("pointer");
//		}

		Cursor.lockState = CursorLockMode.Locked;

		control.Move (transform.up * -Time.deltaTime * 20f);//gravity

		vertical = Input.GetAxis ("Vertical");
		if (!lookatObject) {//this is so camera doesnt jump when you stop looking at objects
			upDownLook -= Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationVal;
			upDownLook = Mathf.Clamp (upDownLook, -80f, 80f);
		}

		leftRightLook = Input.GetAxis ("Mouse X") * Time.deltaTime * rotationVal;


		transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y, 0f);


		if (!lookatObject) {//so when you're looking at stuff you cant move
			
			transform.Rotate (0, leftRightLook, 0);//player rotation left and right with mouse
			Camera.main.transform.eulerAngles = new Vector3 (upDownLook, transform.eulerAngles.y, 0);//cam rotation up down with mouse

			//movement stuff
			if (Input.GetKey (KeyCode.V) && !isCrouching) {//if sprinting
				control.Move (transform.forward * Time.deltaTime * sprintSpeed);
			} else {
				if (isCrouching) {//if crouching move slower
					control.Move (transform.forward * Time.deltaTime * vertical * crouchSpeed);
				} else {//normal
					control.Move (transform.forward * Time.deltaTime * vertical * playerSpeed);
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
				if(control.height >1)
					control.height -= Time.deltaTime;
				Camera.main.transform.localPosition = Vector3.zero;
			} else {
				if(control.height <2){
					control.height += Time.deltaTime;
					transform.position += new Vector3(0f,Time.deltaTime,0f);
				}
				//Camera.main.transform.localPosition = new Vector3 (0, 0.75f, 0);
			}
		}
			
		InteractionFunction ();//for when you wanna pick objects

		Debug.DrawRay (Camera.main.transform.position, Camera.main.transform.forward, Color.red);

	}

	void InteractionFunction ()
	{
		//this creates raycast in front of player used to grab objects
		Ray playerRay = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		Physics.Raycast (playerRay, out hit, 15f, layerMask);


		if (Input.GetMouseButtonDown (0) && carryingObject) {//drop object
			//Debug.Log ("drop");
			if(col.gameObject.GetComponent<Rigidbody>() == true){
				col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			}
				
			col.transform.localPosition = _heldPos;
			col.transform.parent = null;
			//col.transform.localPosition = Camera.main.transform.position +  (Camera.main.transform.forward * 2f);
			//control.center = new Vector3 (0, 0, 0);
			carryingObject = false;
			lookatObject = false;
			if (col.gameObject.GetComponent<thing> () != null) {
				col.gameObject.GetComponent<thing> ().SetCloneActive (false);
			}
			if (col.transform.childCount > 0) {
				for (int i = 0; i < col.transform.childCount; i++) {
					if(col.transform.GetChild (i).gameObject.layer != 8)
						col.transform.GetChild (i).gameObject.layer = 8;
					if (col.transform.GetChild (i).transform.childCount > 0) {
						for (int j = 0; j < col.transform.GetChild (i).childCount; j++) {
							if (col.transform.GetChild (i).GetChild (j).gameObject.layer != 8)
								col.transform.GetChild (i).GetChild (j).gameObject.layer = 8;
						}
					}
				}
			}

			if(col.tag == "triangle"){
				dropSounds.GetComponent<trianleScript>().playTriangleThump();
			}


			col.layer = 8;
			col = null;
		}

		//this is so the raycast only hits stuff on the 'pickable' layer 
		if (hit.collider != null && !carryingObject) {
			if (hit.collider != null) {//if hit something
				col = hit.collider.gameObject;
				//pointer.GetComponent<Image>().color = Color.red;
				if(col.gameObject.GetComponent<thing>() != null)
					col.gameObject.GetComponent<thing>().SetCloneActive(true);//sets the outline object on when raycast is colliding

				//Grab object if mouse clicked (also freezes object at center of screen and slightly moves the player's collision box so object doesn't go through walls)
				if (Input.GetMouseButtonDown (0) && !carryingObject && hit.collider != null && HeldObjectName == "") {
					if (!lookatObject) {
						//Debug.Log ("hit");
						col.transform.parent = Camera.main.transform;
						_heldPos = col.transform.localPosition;
						//col.transform.localPosition = new Vector3 (0.5f, -1f + Mathf.Pow(col.GetComponent<MeshFilter> ().sharedMesh.bounds.size.magnitude * (col.transform.localScale.magnitude), 1/8f) / 3f, 1f);
						//col.transform.localEulerAngles = new Vector3 (0, 0, 0);
					}
					//control.center = new Vector3 (0, 0, 0.5f);
					carryingObject = true;
					if(col.gameObject.GetComponent<Rigidbody>() != null){
						col.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
						//col.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
					}

					if(col.gameObject.GetComponent<thing>() != null)
						col.gameObject.GetComponent<thing>().SetCloneActive(false);

		
				}
			} 

		} else if (!lookatObject && !carryingObject) {
			//	pointer.GetComponent<Image>().color = Color.white;
			if (col != null) {
				if (col.gameObject.GetComponent<thing> () != null)
					col.gameObject.GetComponent<thing> ().SetCloneActive (false);//if raycast not colliding dont show outline
				if (col.transform.childCount > 0) {
					for (int i = 0; i < col.transform.childCount; i++) {
						if (col.transform.GetChild (i).gameObject.layer != 8)
							col.transform.GetChild (i).gameObject.layer = 8;
						if (col.transform.GetChild (i).transform.childCount > 0) {
							for (int j = 0; j < col.transform.GetChild (i).childCount; j++) {
								if (col.transform.GetChild (i).GetChild (j).gameObject.layer != 8)
									col.transform.GetChild (i).GetChild (j).gameObject.layer = 8;
							}
						}
					}
				}
				col.layer = 8;
			} 
			//fuck

			col = null;

		}

		if (Input.GetKeyDown (KeyCode.R) && carryingObject && col.tag != "joint") {
			if (lookatObject)
				lookatObject = false;
			else
				lookatObject = true;
		}
		else if (carryingObject) {//if not holding ctrl or right mouse stop looking
			if (lookatObject) {
				if (col != null) {
					col.transform.localPosition = _rotatePos;
					col.layer = 9;
					if (col.transform.childCount > 0) {
						for (int i = 0; i < col.transform.childCount; i++) {
							if (col.transform.GetChild (i).gameObject.layer != 9)
								col.transform.GetChild (i).gameObject.layer = 9;
							if (col.transform.GetChild (i).transform.childCount > 0) {
								for (int j = 0; j < col.transform.GetChild (i).childCount; j++) {
									if (col.transform.GetChild (i).GetChild (j).gameObject.layer != 9)
										col.transform.GetChild (i).GetChild (j).gameObject.layer = 9;
								}
							}
						}
					}
				} else {
					lookatObject = false;
					carryingObject = false;
					Debug.Log ("f");
				}
			} else {
				if (col != null) {
					col.layer = 8;
					if (col.transform.childCount > 0) {
						for (int i = 0; i < col.transform.childCount; i++) {
							if (col.transform.GetChild (i).gameObject.layer != 8)
								col.transform.GetChild (i).gameObject.layer = 8;
							if (col.transform.GetChild (i).transform.childCount > 0) {
								for (int j = 0; j < col.transform.GetChild (i).childCount; j++) {
									if (col.transform.GetChild (i).GetChild (j).gameObject.layer != 8)
										col.transform.GetChild (i).GetChild (j).gameObject.layer = 8;
								}
							}
						}
						//object_camera.orthographic = false;
						if (col.gameObject.GetComponent<thing> () != null)
							col.gameObject.GetComponent<thing> ().SetCloneActive (false);
						col.transform.localPosition = _heldPos;
					} 
				} else {
					carryingObject = false;
					Debug.Log ("f");
				}
			}
		

		}


		if (col != null && lookatObject && col.tag != "joint") {//look at object
			col.gameObject.GetComponent<thing>().SetCloneActive(false);

			//object_camera.orthographic = true;
			//object_camera.orthographicSize = Mathf.Sqrt (col.GetComponent<MeshFilter> ().sharedMesh.bounds.size.magnitude * (col.transform.localScale.magnitude));

			leftRightLookObj = Input.GetAxis ("Mouse X") * Time.deltaTime * rotationVal;
			upDownLookObj = Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationVal;

			col.transform.localPosition = _rotatePos;

			col.transform.Rotate (transform.up , -leftRightLookObj, Space.World);
			col.transform.Rotate (transform.right, upDownLookObj, Space.World);
			//pointer.GetComponent<Image>().color = Color.clear;
		}else{
			//pointer.GetComponent<Image>().color = Color.white;
		}

		if (carryingObject) {
			HeldObjectName = col.name;
			HeldObject = col.gameObject;
		} else {
			HeldObjectName = "";
			HeldObject = null;
		}


	}
		
}
