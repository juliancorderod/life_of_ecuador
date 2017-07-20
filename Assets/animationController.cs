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

		Debug.Log(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("walk1"));
		
		if(!Input.anyKey && playerScript.leftRightLook == 0 && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("crouch") && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("standUp")){
			if(!playerScript.isCrouching)
				playerAnimator.CrossFade("emptyAnim", 0.2f);

			else
				playerAnimator.CrossFade("emptyAnimCrouch", 0.2f);

		}
			


		//turning
		if (playerScript.leftRightLook != 0 && !Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.V)) {
			if(!playerScript.isCrouching){
				if (playerScript.HeldObject != null){
					if (playerScript.leftRightLook > 0){

						StartCoroutine(crossFadeAnims("turningRightNoArms",0.1f));
						//ADD STUFF ABOUT ANIM SPEED FASTER IF TURNING FASTER
						//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
					}else {
						StartCoroutine(crossFadeAnims("turningLeftNoArms",0.1f));
						//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
					}
				}else if (playerScript.leftRightLook > 0){
					StartCoroutine(crossFadeAnims("turningRight",0.1f));
				
					//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
				}else{
					StartCoroutine(crossFadeAnims("turningLeft",0.1f));
					//playerAnimator.speed = Mathf.Abs(playerScript.leftRightLook * 0.7f);
				}
			}
		} else
			//if(playerAnimator.GetCurrentAnimatorStateInfo(0) == null)
			//playerAnimator.Play ("emptyAnim");

			if (Input.GetKey (KeyCode.Space) && !playerScript.isCrouching && !Input.GetKey (KeyCode.V)) {

				if (playerScript.HeldObject == null){

					StartCoroutine(crossFadeAnims("walk1",0.1f));
				}
			else
					StartCoroutine(crossFadeAnims("walkNoArms",0.1f));
		}

		if(Input.GetKeyDown (KeyCode.C)){
			if(!playerScript.isCrouching)
				StartCoroutine(crossFadeAnims("crouch",0.1f));
			else
				StartCoroutine(crossFadeAnims("standUp",0.1f));


		}

		if(playerScript.isCrouching){
			if(Input.GetKey (KeyCode.Space)) {

				if (playerScript.HeldObject == null)
					StartCoroutine(crossFadeAnims("crouchWalk",0.1f));
				else
					StartCoroutine(crossFadeAnims("crouchWalkNoArms",0.1f));
			} 
			//else
				//playerAnimator.Play ("emptyAnimCrouch");
		} 

		if(Input.GetKey (KeyCode.V) && !playerScript.isCrouching){
			if (playerScript.HeldObject == null)
				StartCoroutine(crossFadeAnims("run",0.1f));
			else
				StartCoroutine(crossFadeAnims("run",0.1f));

		} 
		
	}

	void LateUpdate(){

		neck.localEulerAngles = playerCam.transform.localEulerAngles + neckInitRot;

	}

	IEnumerator crossFadeAnims (string animName, float seconds){

		playerAnimator.CrossFade(animName,seconds);
		yield return new WaitForSeconds(seconds);
		playerAnimator.Play(animName);
	}
}
