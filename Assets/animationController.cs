using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{

	Animator playerAnimator;

	public GameObject playerObj;
	public player playerScript;
	public Transform neck;
	public GameObject playerCam;

	Vector3 neckInitPos, neckInitRot;

	public Transform rShould, rElbow, rWrist, rHand, lShould, lElbow, lWrist, lHand;

	float idleTimer;

	int randIdle = 3;
	public bool canPeep = false;

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

		Ray _groundRay = new Ray (transform.position + new Vector3(0,1,0), -transform.up);
		RaycastHit _groundHit = new RaycastHit ();
		bool grounded = Physics.Raycast (_groundRay, out _groundHit, 2f);
		Debug.DrawRay(_groundRay.origin,_groundRay.direction * 1.5f,Color.red);

		if (!grounded) {
			StartCoroutine (crossFadeAnims ("fall", 0.2f));
		} else {


			if (!Input.anyKey && playerScript.leftRightLook == 0 && !playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("crouch") && !playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("standUp")) {
				if (!playerScript.isCrouching) {
					
					idleTimer += Time.deltaTime;
					if (idleTimer > 5f && !canPeep) {
						StartCoroutine (crossFadeAnims ("idle" + randIdle, 0.2f));
						if (idleTimer > 13f){
							idleTimer = 0;
							randIdle = Random.Range (1, 4);
						}
					} else 	if (canPeep) {

						StartCoroutine (crossFadeAnims ("coverPeep", 0.1f));
						if(playerObj.transform.eulerAngles.y > 95f){
							playerObj.transform.eulerAngles -= new Vector3 (0,Time.deltaTime * 100,0);
						} else if (playerObj.transform.eulerAngles.y < 85f)
							playerObj.transform.eulerAngles += new Vector3 (0,Time.deltaTime * 100,0);

						if(playerObj.transform.position.z > -198.4f){
							playerObj.transform.position -= new Vector3 (0,0,Time.deltaTime * 5);
						} else if (playerObj.transform.position.z < -198.8f)
							playerObj.transform.position += new Vector3 (0,0, Time.deltaTime * 5);
					}else
						playerAnimator.CrossFade ("emptyAnim", 0.1f);
					

				} else
					playerAnimator.CrossFade ("emptyAnimCrouch", 0.1f);

			} else{
				idleTimer = 0;
				randIdle = Random.Range (1, 4);
			}
			


			//turning

//			Debug.Log (playerScript.leftRightLook);
			if (playerScript.leftRightLook != 0 && !Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.V)) {
				if (!playerScript.isCrouching) {
					
				
					if (playerScript.HeldObject != null) {
						if (playerScript.leftRightLook > 0) {

							StartCoroutine (crossFadeAnims ("turningRightNoArms", 0.1f));
							//ADD STUFF ABOUT ANIM SPEED FASTER IF TURNING FASTER
							//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook);
						} else if (playerScript.leftRightLook < 0) {
							StartCoroutine (crossFadeAnims ("turningLeftNoArms", 0.1f));
							//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook);
						}
					} else if (playerScript.leftRightLook > 0) {
						StartCoroutine (crossFadeAnims ("turningRight", 0.1f));
				
						//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook);
					} else if (playerScript.leftRightLook < 0) {
						StartCoroutine (crossFadeAnims ("turningLeft", 0.1f));
						//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook);
					}
				
				} else if (!playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("crouch") && !playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("standUp"))
					StartCoroutine (crossFadeAnims ("crouchWalk", 0.1f));
			}



			if (!playerScript.lookatObject) {
			
				if (Input.GetKeyDown (KeyCode.C) && !Input.GetKey (KeyCode.Space)) {
					if (!playerScript.isCrouching)
						StartCoroutine (crossFadeAnims ("crouch", 0.1f));
					else
						StartCoroutine (crossFadeAnims ("standUp", 0.1f));
				}

				if (Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.V)) {

					if (!playerScript.isCrouching) {

						if (playerScript.HeldObject == null) {

							StartCoroutine (crossFadeAnims ("walk1", 0.1f));
						} else
							StartCoroutine (crossFadeAnims ("walkNoArms", 0.1f));
					} else {
					

						if (playerScript.HeldObject == null)
							StartCoroutine (crossFadeAnims ("crouchWalk", 0.1f));
						else
							StartCoroutine (crossFadeAnims ("crouchWalkNoArms", 0.1f));
					 
					}
				}




				if (Input.GetKey (KeyCode.V) && !playerScript.isCrouching) {
					if (playerScript.HeldObject == null)
						StartCoroutine (crossFadeAnims ("run", 0.1f));
					else
						StartCoroutine (crossFadeAnims ("runNoArms", 0.1f));

				} 

				if (Input.GetKey (KeyCode.LeftShift)) { 
					if (!playerScript.isCrouching)
						StartCoroutine (crossFadeAnims ("walkBack", 0.1f));
					else
						StartCoroutine (crossFadeAnims ("crouchWalk", 0.1f));
				}

			}

		

		}
	}

	void LateUpdate ()
	{

		neck.localEulerAngles = playerCam.transform.localEulerAngles + neckInitRot;

		if (playerScript.HeldObject != null) {
			
			if (playerScript.HeldObject != null) { 

				Vector3 forRotation = playerScript.hit.point; 

				if(playerScript.lookatObject){
					rElbow.transform.position = (playerScript.col.transform.position + rShould.transform.position) / 2f; 
					rWrist.transform.position = playerScript.col.transform.position; 

					lElbow.transform.position = (playerScript.col.transform.position + lShould.transform.position) / 2f; 
					lWrist.transform.position = playerScript.col.transform.position;

				}else{

					rElbow.transform.position = (playerScript.hit.point + rShould.transform.position) / 2f; 
					rWrist.transform.position = playerScript.hit.point; 

					lElbow.transform.position = (playerScript.hit.point + lShould.transform.position) / 2f; 
					lWrist.transform.position = playerScript.hit.point;
				}
			}
		}


	}

	IEnumerator crossFadeAnims (string animName, float seconds)
	{

		playerAnimator.CrossFade (animName, seconds);
		yield return new WaitForSeconds (seconds);
		playerAnimator.Play (animName);
	}
}
