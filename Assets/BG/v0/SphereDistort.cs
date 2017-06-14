using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDistort : MonoBehaviour {
	public float _baseRotSpeed;
	public float _rotLerpSpeed;

	[Range(-1f, 0f)]
	public float _clampValueMin;
	[Range(0f, 1f)]
	public float _clampValueMax;

	public float _speed;
	public float _range;

	private Vector3 _rot;

	private MeshFilter _filter;

	private Mesh _mesh;
	private Vector3[] _normals;
	private Vector3[] orig_verts;
	private Vector3[] new_verts;

	// Use this for initialization
	void Start () {
		_filter = GetComponent<MeshFilter> ();

		_mesh = _filter.sharedMesh;
		_normals = _mesh.normals;
		orig_verts = _mesh.vertices;
		new_verts = new Vector3[orig_verts.Length];

		for (int j = 0; j < orig_verts.Length; j++)
			new_verts [j] = orig_verts [j];

		_rot = Random.insideUnitSphere;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (_rot, Time.deltaTime * Mathf.Clamp (Mathf.Sin (Time.time), _clampValueMin, _clampValueMax) * _baseRotSpeed, Space.World);

		for (int i = 0; i < orig_verts.Length; i++) {
			new_verts[i] = transform.InverseTransformPoint(Mathf.PerlinNoise (transform.TransformPoint(new_verts[i]).x, transform.TransformPoint(new_verts[i]).z) * _range * Mathf.Sin (Time.timeSinceLevelLoad * _speed)
				* _normals[i]
				+ transform.TransformPoint(orig_verts[i]));
		}
		_mesh.vertices = new_verts;
	}
}
