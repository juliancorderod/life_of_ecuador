using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerLoadScript : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject cameraThirdPerson;

	void Awake(){
		Instantiate(playerPrefab);
		Instantiate(cameraThirdPerson);
		SceneManager.LoadScene ("_WAITINGROOM_v2");

	}
		
}
