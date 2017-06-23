using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour {

	public float amplitude, freq, clamp;

	float yVal, lerpVal, yValWithCrouch;

	public AudioSource playerSound;
	public AudioSource fxSound;

	public AudioClip step1, step2, step3, step4, jungleStep1, jungleStep2, jungleStep3, jungleStep4;

	public AudioClip jungleSwish1, jungleSwish2, jungleSwish3, jungleSwish4;

	bool stepCanPlay, swishCanPlay = false;
	float swishCanPlayReset = 0;

	public static bool inJungle = false;

	[Range (0, 100)]
	public int swishProbability;



	// Use this for initialization
	void Start () {
		lerpVal = 1;
	}
	
	// Update is called once per frame
	void Update () {


		fxSound.volume = Mathf.Clamp(fxSound.volume, 0, 0.5f);

		if(!transform.parent.GetComponent<player>().isCrouching){
			if(Mathf.Round(Input.GetAxis("Horizontal")) != 0 || Mathf.Round(Input.GetAxis("Vertical")) != 0){
				
				lerpVal -= Time.deltaTime * 5;
				fxSound.volume += Time.deltaTime * 1.25f;


			}else{
				lerpVal += Time.deltaTime * 5;
				fxSound.volume -= Time.deltaTime * 1.25f;
				swishCanPlayReset = 5;
			}
		} else{
			lerpVal += Time.deltaTime * 5;
		}

		if(Input.GetKey(KeyCode.V)){

			yVal = (Mathf.Lerp(0.75f +(Mathf.Sin(Time.time* (freq +5))/amplitude), yValWithCrouch, lerpVal));
		} else{
			yVal = Mathf.Lerp(0.75f +(Mathf.Sin(Time.time* freq)/amplitude), yValWithCrouch, lerpVal);
		}



		transform.localPosition = new Vector3(transform.localPosition.x, yVal, transform.localPosition.z);

		lerpVal = Mathf.Clamp01(lerpVal);

		if(transform.parent.GetComponent<player>().isCrouching){//check for crouch!
			yValWithCrouch  = 0;
		}else{
			yValWithCrouch  = 0.75f;
			yVal = Mathf.Clamp(yVal,clamp,100f);
		}

//		Debug.Log(yVal);
		if(yVal == 0.66f && stepCanPlay){
//			Debug.Log("yeah");
			playStepSound();
			stepCanPlay = false;
		}
		if(yVal > 0.75f){
			stepCanPlay = true;
		}

		if(yVal == 0.66f && swishCanPlay){
			playSwishSound();
			swishCanPlay = false;
			swishCanPlayReset = 0;
//			Debug.Log("yeah");
		}
		if(!swishCanPlay){
			swishCanPlayReset += Time.deltaTime;
			if(swishCanPlayReset >= 5){
				swishCanPlay = true;
			}
				
		}




		
	}

	void playStepSound(){

		if(!inJungle){
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
		}else{
			int randNum = Random.Range(1,5);

			if(randNum == 1){
				playerSound.clip = jungleStep1;
				playerSound.Play();
			}
			if(randNum == 2){
				playerSound.clip = jungleStep2;
				playerSound.Play();
			}
			if(randNum == 3){
				playerSound.clip = jungleStep3;
				playerSound.Play();
			}
			if(randNum == 4){
				playerSound.clip = jungleStep4;
				playerSound.Play();
			}


		}
	}

	void playSwishSound(){
		int randNum = Random.Range(0,100);
		if(inJungle){
			if (randNum <= swishProbability){
				fxSound.Play();
				int randNum2 = Random.Range(1,5);

				if(randNum2 == 1){
					fxSound.clip = jungleSwish1;
					fxSound.Play();
				}
				if(randNum2 == 2){
					fxSound.clip = jungleSwish2;
					fxSound.Play();
				}
				if(randNum2 == 3){
					fxSound.clip = jungleSwish3;
					fxSound.Play();
				}
				if(randNum2 == 4){
					fxSound.clip = jungleSwish4;
					fxSound.Play();
				}
			}
		}

	}
}
