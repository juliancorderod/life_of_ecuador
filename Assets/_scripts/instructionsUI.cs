using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instructionsUI : MonoBehaviour {
	public GameObject menu;
	public GameObject tut;
	public GameObject[] tut_items;

	private int _ind;
	private int _maxInd;

	// Use this for initialization
	void Start () {
		_ind = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			if (_ind >= tut_items.Length)
				SceneManager.LoadScene ("_MAIN_SCENE");
			else {
				++_ind;
				Switch (_ind);
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}

	void Switch(int _i){
		if (_i == 0) {
			menu.SetActive (true);
			tut.SetActive (false);
		} else {
			menu.SetActive (false);
			tut.SetActive (true);
		}
			

		for (int i = 0; i < tut_items.Length; i++) {
			if (_i == i + 1)
				tut_items [i].SetActive (true);
			else
				tut_items [i].SetActive (false);	
		}
	}
}
