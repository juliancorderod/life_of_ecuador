using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateStatues : MonoBehaviour {
	Mesh meshInst;
	Vector3[] verts;

	GameObject[] prefab;
	GameObject newPrefab;

	int num_objects = 100;

	// Use this for initialization
	void Start () {

		prefab = Resources.LoadAll<GameObject> ("_prefabs/stuff");

		meshInst = GetComponent<MeshFilter>().mesh;
		verts = meshInst.vertices;



		//meshes should have about 100 verts
		for(int i = 0; i < num_objects ; i++){
			newPrefab = Instantiate(prefab[Random.Range(0,prefab.Length)], verts[Random.Range(0, verts.Length)]+ transform.position
				+ new Vector3(Random.Range(-2,2), Random.Range(-2,2), Random.Range(-2,2)),Random.rotation,transform);


			if (newPrefab.GetComponent<MeshFilter> () != null &&
				(newPrefab.name != "light-ball(Clone)" && 
				newPrefab.name != "car(Clone)" &&
				newPrefab.name != "flowers(Clone)" &&
				newPrefab.name != "insects(Clone)" &&
				newPrefab.name != "person(Clone)" &&
				newPrefab.name != "statue_(Clone)" &&
				newPrefab.name != "computer(Clone)"  &&
				newPrefab.name != "radio(Clone)")) {

				if (newPrefab.GetComponent<MeshFilter> ().mesh.name == "Cube Instance" && Random.Range (0, 50) != 0) {
					Destroy (newPrefab);
					--i;
				} else {
					if (newPrefab.GetComponent<MeshFilter> ().mesh.bounds.size.magnitude != 0)
						newPrefab.transform.localScale /= newPrefab.GetComponent<MeshFilter> ().mesh.bounds.size.magnitude / 3f;
					if (newPrefab.GetComponent<Collider> () != null)
						newPrefab.GetComponent<Collider> ().enabled = false;
				}
			} else {
				Destroy (newPrefab);
				--i;
			}
		}
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
