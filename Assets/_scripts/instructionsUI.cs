using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instructionsUI : MonoBehaviour {

	public GameObject WASD, look, pick, examine, sprint, crouch, title, pressSpace, pressEsc, website, space;

	string tutPic = "";

	// Use this for initialization
	void Start () {
		tutPic = "WASD";
		tutPic = "look";
		tutPic = "pick";
		tutPic = "examine";
		tutPic = "sprint";
		tutPic = "crouch";
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){
			if(tutPic == ""){
				tutPic = "WASD";
			}
			if(tutPic == "WASD"){
				tutPic = "look";
			}
			if(tutPic == "look"){
				tutPic = "pick";
			}
			if(tutPic == "pick"){
				tutPic = "examine";
			}
			if(tutPic == "examine"){
				tutPic = "sprint";
			}
			if(tutPic == "sprint"){
				tutPic = "crouch";
			}
			if(tutPic == "crouch"){
				SceneManager.LoadScene ("_MAIN_SCENE");
			}
		}

		if(tutPic == "WASD"){
			title.SetActive(false);
			pressSpace.SetActive(false);
			pressEsc.SetActive(false);
			website.SetActive(false);

			space.SetActive(true);
			WASD.SetActive(true);
		}
		if(tutPic == "look"){
			WASD.SetActive(false);
			look.SetActive(true);
		}
		if(tutPic == "pick"){
			look.SetActive(false);
			pick.SetActive(true);
		}
		if(tutPic == "examine"){
			pick.SetActive(false);
			examine.SetActive(true);
		}
		if(tutPic == "sprint"){
			examine.SetActive(false);
			sprint.SetActive(true);
		}
		if(tutPic == "crouch"){
			sprint.SetActive(false);
			crouch.SetActive(true);
		}
		
	}
}
