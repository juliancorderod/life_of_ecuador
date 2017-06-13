using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabInst : MonoBehaviour {

	public Transform prefabTest;


	List<Transform> prefabList = new List<Transform>();

	// Use this for initialization
	void Start () {

		for(int i = 0; i < 100; i++){
			Transform newPrefab = (Transform) Instantiate(prefabTest, (Random.insideUnitSphere ) + (new Vector3(0f,4f,0f)), Random.rotation);
			//Transform newPrefab = (Transform) Instantiate(prefabTest, new Vector3(0f,2f,0f), Random.rotation);
			newPrefab.localScale = Vector3.one * 0.25f;


			prefabList.Add (newPrefab);
		}

		
	}
	
	// Update is called once per frame
	void Update () {



		for(int i=0; i<prefabList.Count; i++){

			prefabList[i].Rotate(0,0,10 * Time.deltaTime);

		}
		
	}
}
