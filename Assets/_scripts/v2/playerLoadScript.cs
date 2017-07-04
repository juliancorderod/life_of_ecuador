using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerLoadScript : MonoBehaviour {

	public GameObject playerPrefab;

	void Awake(){
		Instantiate(playerPrefab);
		SceneManager.LoadScene ("_WAITINGROOM_v2");

	}
		
}
