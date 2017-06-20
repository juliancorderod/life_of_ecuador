using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour {

	public float amplitude, freq, clamp;

	float yVal, lerpVal;

	public AudioSource playerSound;

	public AudioClip step1, step2, step3, step4;

	bool stepCanPlay = false;

	// Use this for initialization
	void Start () {
		lerpVal = 1;

	}
	
	// Update is called once per frame
	void Update () {


		if(!transform.parent.GetComponent<player>().isCrouching){
			if(Mathf.Round(Input.GetAxis("Horizontal")) != 0 || Mathf.Round(Input.GetAxis("Vertical")) != 0){
				
				lerpVal -= Time.deltaTime * 5;

			}else{
				lerpVal += Time.deltaTime * 5;
			}
		} else{
			lerpVal += Time.deltaTime * 5;
		}

		if(Input.GetKey(KeyCode.LeftShift)){

			yVal = (Mathf.Lerp(0.75f +(Mathf.Sin(Time.time* (freq +5))/amplitude), 0.75f, lerpVal));
		} else{

		yVal = Mathf.Lerp(0.75f +(Mathf.Sin(Time.time* freq)/amplitude), 0.75f, lerpVal);
		}

		yVal = Mathf.Clamp(yVal,clamp,100f);

		transform.localPosition = new Vector3(transform.localPosition.x, yVal, transform.localPosition.z);

		lerpVal = Mathf.Clamp01(lerpVal);
		//Debug.Log(Mathf.Abs(Input.GetAxis("Vertical")));

		if(yVal == 0.66f && stepCanPlay){
			playStepSound();
			stepCanPlay = false;
		}
		if(yVal > 0.75f){
			stepCanPlay = true;
		}


		
	}

	void playStepSound(){
		int randNum = Random.Range(1,5);

		if(randNum == 1){
			playerSound.clip = step1;
			playerSound.Play();
		}
		if(randNum == 2){
			playerSound.clip = step2;
			playerSound.Play();
		}
		if(randNum == 3){
			playerSound.clip = step3;
			playerSound.Play();
		}
		if(randNum == 4){
			playerSound.clip = step4;
			playerSound.Play();
		}

		Debug.Log(randNum);

	}
}
