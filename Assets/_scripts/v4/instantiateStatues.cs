using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateStatues : MonoBehaviour {

	Mesh meshInst;
	Vector3[] verts;

	GameObject[] prefab;
	GameObject newPrefab;

	// Use this for initialization
	void Start () {

		prefab = Resources.LoadAll<GameObject> ("_prefabs/stuff");

		meshInst = GetComponent<MeshFilter>().mesh;
		verts = meshInst.vertices;



		//meshes should have about 100 verts
		for(int i = 0; i < verts.Length ; i++){
			newPrefab = Instantiate(prefab[Random.Range(0,prefab.Length)], verts[i]+ transform.position
				+ new Vector3(Random.Range(-2,2), Random.Range(-2,2), Random.Range(-2,2)),Random.rotation,transform);

			if(newPrefab.GetComponent<MeshFilter>()!= null){
				if(newPrefab.GetComponent<MeshFilter>().mesh.bounds.size.magnitude != 0)
					newPrefab.transform.localScale /= newPrefab.GetComponent<MeshFilter>().mesh.bounds.size.magnitude /3f;
			}else
				Destroy(newPrefab);
		}
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
