using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{

	Animator playerAnimator;

	public player playerScript;
	public Transform neck;
	public GameObject playerCam;

	// Use this for initialization
	void Start ()
	{

		playerAnimator = GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void Update ()
	{

		//neck.localEulerAngles = new Vector3(neck.localEulerAngles.x ,neck.localEulerAngles.y,playerCam.transform.localEulerAngles.x) ;

		if (Input.GetKey (KeyCode.Space)) {

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
		} else
			playerAnimator.Play ("idle1");
		
	}
}
