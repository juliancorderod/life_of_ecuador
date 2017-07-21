using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{

	Animator playerAnimator;

	public player playerScript;
	public Transform neck;
	public GameObject playerCam;

	Vector3 neckInitPos, neckInitRot;

	public Transform rShould, rElbow, rWrist, rHand, lShould, lElbow, lWrist, lHand;

	float idleTimer;

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

		Ray _groundRay = new Ray (transform.position - transform.up / 10f, -transform.up);
		RaycastHit _groundHit = new RaycastHit ();

		//Debug.DrawRay (_groundRay.origin, _groundRay.direction * _groundDistance, Color.red);

		bool grounded = Physics.Raycast (_groundRay, out _groundHit, 2);

		if (!grounded) {
			StartCoroutine (crossFadeAnims ("fall", 0.2f));
		} else {


			if (!Input.anyKey && playerScript.leftRightLook == 0 && !playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("crouch") && !playerAnimator.GetCurrentAnimatorStateInfo (0).IsName ("standUp")) {
				if (!playerScript.isCrouching) {
					
					idleTimer += Time.deltaTime;
					if (idleTimer > 5f) {
						Debug.Log (idleTimer);
						playerAnimator.CrossFade ("idle" + Random.Range (1, 4), 0.2f);
						if (idleTimer > 10f)
							idleTimer = 0;
					} else
						playerAnimator.CrossFade ("emptyAnim", 0.2f);

				} else
					playerAnimator.CrossFade ("emptyAnimCrouch", 0.2f);

			} else
				idleTimer = 0;
			


			//turning

//			Debug.Log (playerScript.leftRightLook);
			if (playerScript.leftRightLook != 0 && !Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.V)) {
				if (!playerScript.isCrouching) {
					if (playerScript.HeldObject != null) {
						if (playerScript.leftRightLook > 0) {

							StartCoroutine (crossFadeAnims ("turningRightNoArms", 0.1f));
							//ADD STUFF ABOUT ANIM SPEED FASTER IF TURNING FASTER
							//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
						} else if (playerScript.leftRightLook < 0) {
							StartCoroutine (crossFadeAnims ("turningLeftNoArms", 0.1f));
							//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
						}
					} else if (playerScript.leftRightLook > 0) {
						StartCoroutine (crossFadeAnims ("turningRight", 0.1f));
				
						//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
					} else if (playerScript.leftRightLook < 0) {
						StartCoroutine (crossFadeAnims ("turningLeft", 0.1f));
						//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
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

			if (Input.GetKeyDown (KeyCode.A)) {
				StartCoroutine (crossFadeAnims ("coverPeep", 0.1f));
			}

		}
	}

	void LateUpdate ()
	{

		neck.localEulerAngles = playerCam.transform.localEulerAngles + neckInitRot;

		if (playerScript.HeldObject != null) {
			
			if (playerScript.HeldObject != null) { 

				Vector3 forRotation = playerScript.hit.point; 


				rElbow.transform.position = (playerScript.hit.point + rShould.transform.position) / 2f; 
				rWrist.transform.position = playerScript.hit.point; 

				lElbow.transform.position = (playerScript.hit.point + lShould.transform.position) / 2f; 
				lWrist.transform.position = playerScript.hit.point; 
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
