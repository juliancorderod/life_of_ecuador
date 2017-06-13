using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuActive : MonoBehaviour {

	bool pauseMenuOnBool = false;
	public Camera backgroundCam;
	public GameObject pauseMenuUI, playerObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.Escape)){
			if(pauseMenuOnBool){
				pauseMenuOff();

			}else{
				pauseMenuOn();

			}
		}

		if(pauseMenuOnBool){
			Cursor.lockState = CursorLockMode.None;
		}else{
			Cursor.lockState = CursorLockMode.Locked;
		}

		Debug.Log(pauseMenuOnBool);
	}

	void pauseMenuOn(){
		
		pauseMenuOnBool = true;
		backgroundCam.depth = 1;
		//playerObj.GetComponent<player>().enabled = false;
		pauseMenuUI.SetActive (true);

	}

	void pauseMenuOff(){
		
		backgroundCam.depth = -3;
		pauseMenuUI.SetActive (false);
		//playerObj.GetComponent<player>().enabled = true;
		pauseMenuOnBool = false;
	}


}
