using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour {

	public float amplitude, freq, clamp;

	float yVal, lerpVal;

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
		}


		if(Input.GetKey(KeyCode.LeftShift)){

			yVal = (Mathf.Lerp(0.75f +(Mathf.Sin(Time.time* (freq +5))/amplitude), 0.75f, lerpVal));
		} else{

		yVal = Mathf.Lerp(0.75f +(Mathf.Sin(Time.time* freq)/amplitude), 0.75f, lerpVal);
		}
		yVal = Mathf.Clamp(yVal,clamp,100f);

		transform.localPosition = new Vector3(transform.localPosition.x, yVal, transform.localPosition.z);

		lerpVal = Mathf.Clamp01(lerpVal);
		Debug.Log(Mathf.Abs(Input.GetAxis("Vertical")));
		
	}
}
