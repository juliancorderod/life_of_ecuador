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
		GameObject new_prefab = null;

		prefab = Resources.LoadAll<GameObject> ("_prefabs/stuff");

		meshInst = GetComponent<MeshFilter>().mesh;
		verts = meshInst.vertices;



		//meshes should have about 100 verts
		for(int i = 0; i < num_objects ; i++){
			new_prefab = prefab [Random.Range (0, prefab.Length)];

			if (new_prefab.GetComponent<MeshFilter> () != null &&
				(new_prefab.name != "light-ball" && 
					new_prefab.name != "car" &&
					new_prefab.name != "flowers" &&
					new_prefab.name != "insects" &&
					new_prefab.name != "person" &&
					new_prefab.name != "statue_" &&
					new_prefab.name != "computer"  &&
					new_prefab.name != "radio")) {

				if (new_prefab.GetComponent<MeshRenderer>().sharedMaterial.name == "3" && Random.Range (0, 50) != 0) {
					//Debug.Log ("hey");
					--i;
				} else {
					newPrefab = Instantiate(new_prefab, verts[Random.Range(0, verts.Length)]+ transform.position
						+ new Vector3(Random.Range(-2,2), Random.Range(-2,2), Random.Range(-2,2)),Random.rotation,transform);
					
					if (newPrefab.GetComponent<MeshFilter> ().mesh.bounds.size.magnitude != 0)
						newPrefab.transform.localScale /= newPrefab.GetComponent<MeshFilter> ().mesh.bounds.size.magnitude / 3f;
					if (newPrefab.GetComponent<Collider> () != null)
						newPrefab.GetComponent<Collider> ().enabled = false;
				}
			} else {
				--i;
			}
		}
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
