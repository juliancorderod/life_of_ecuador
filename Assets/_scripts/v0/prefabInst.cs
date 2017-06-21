using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabInst : MonoBehaviour {

	public Transform prefabTest;


	List<Transform> prefabList = new List<Transform>();

	private GameObject[] _prefabs;
	private GameObject[] _list;

	// Use this for initialization
	void Start () {
		_prefabs = Resources.LoadAll<GameObject> ("_prefabs/stuff");
		_list = new GameObject[_prefabs.Length];

		GameObject newPrefab;



		for(int i = 0; i < _prefabs.Length; i++){
			newPrefab = Instantiate (_prefabs [i], (Random.insideUnitSphere*1.25f) + (new Vector3 (0f, 4f, 0f)), Random.rotation, transform) as GameObject;
			newPrefab.transform.localScale *= 0.2f;


			_list [i] = newPrefab;
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i<prefabList.Count; i++){

			_list[i].transform.Rotate(0,0,10 * Time.deltaTime);

		}
		
	}
}
