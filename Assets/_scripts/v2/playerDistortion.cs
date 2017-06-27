using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDistortion : MonoBehaviour {
	private Mesh _mesh;
	private Mesh _bakedMesh;

	public Vector3[] _verts;
	private Vector3[] _normals;

	[Range(1, 10)]
	public int FIDELITY;
	[Range(0f, 10f)]
	public int HEIGHT;
	[Range(0f, 10f)]
	public int LENGTH;

	private Vector3[] _new_verts;

	private float _timeSinceStart;


	private float _moveSpeed;
	private float _moveDistance;
	private Vector3 _frameStartPos;

	private int _ind;

	// Use this for initialization
	void Start () {
		_timeSinceStart = 0f;

		_ind = 0;

		_mesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
		_verts = _mesh.vertices;
		_normals = _mesh.normals;

		_new_verts = _verts;

		_bakedMesh = new Mesh ();
	}
	
	// Update is called once per frame
	void Update () {
		_frameStartPos = transform.position;
		_timeSinceStart += Time.deltaTime;

		GetComponent<SkinnedMeshRenderer> ().BakeMesh (_bakedMesh);
		_verts = _bakedMesh.vertices;
		DistortMesh (_ind);
	}

	void LateUpdate(){
		_moveDistance = Vector3.Distance (transform.position, _frameStartPos);
		_moveSpeed = _moveDistance * Time.deltaTime;
	}

	void DistortMesh(int i){
		int j = i;
		Vector3 _world = Vector3.zero;
	

		for (; j < _verts.Length; j += FIDELITY) {
			_world = transform.TransformPoint (_verts [j]);

			_new_verts [j] = transform.InverseTransformPoint (_world + _normals[j]*(HEIGHT) * (_moveDistance) * Mathf.Sin (_timeSinceStart * _moveSpeed * LENGTH));
		}

		PassedThruVerts (j);
	}

	void PassedThruVerts(int k){
		_ind = (k - _verts.Length);

		_mesh.vertices = _new_verts;
		_mesh.RecalculateNormals ();
	}
}
