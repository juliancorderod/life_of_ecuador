using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statueAnim : MonoBehaviour {

	public Animator statueAnimator;



	float timeElapsed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (statueAnimator.GetCurrentAnimatorStateInfo(0).length < timeElapsed){
			
			int randNum = Random.Range(1,5);

			if(randNum == 1){
				statueAnimator.Play("statue1");

			}
			if(randNum == 2){
				statueAnimator.Play("statue2");

			}
			if(randNum == 3){
				statueAnimator.Play("statue3");

			}
			if(randNum == 4){
				statueAnimator.Play("statue4");
			}

			timeElapsed = 0f;


		}
		timeElapsed += Time.deltaTime;

	}


}
