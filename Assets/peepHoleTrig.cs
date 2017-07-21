using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peepHoleTrig : MonoBehaviour {

	public bool canPeep;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			col.transform.FindChild("playerMesh").GetComponent<animationController>().canPeep = true;
		} 
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			col.transform.FindChild("playerMesh").GetComponent<animationController>().canPeep = false;
		} 
	}
}
