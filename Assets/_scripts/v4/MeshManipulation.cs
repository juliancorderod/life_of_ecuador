using UnityEngine;
using System.Collections;

public class MeshManipulation : MonoBehaviour {
	private Mesh _myMesh;

	private Vector3[] _verts;
	private Vector3[] _normals;

	private Vector3[] new_verts;

	private float DISTORTION_CONST = 5f;
	private int[] DISTORTIONS = new int[6];

	public int IND;
	private float _lerp = .1f;

	// Use this for initialization
	void Start () {
		_myMesh = GetComponent<MeshFilter> ().mesh;

		_verts = _myMesh.vertices;
		_normals = _myMesh.normals;

		new_verts = new Vector3[_verts.Length];

		for (int j = 0; j < _verts.Length; j++)
			new_verts [j] = _verts [j];

		IND = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 g = Vector3.zero;

		for (int i = 0; i < _verts.Length; i++) {
			g = transform.TransformPoint (_verts [i]);

			Vector3 n = g + ((Mathf.PerlinNoise (_verts [i].x, _verts [i].z)- .5f) /4f) * Vector3.back * DISTORTION_CONST * DISTORTIONS[IND];

			new_verts [i] = transform.InverseTransformPoint(Vector3.Slerp (transform.TransformPoint(new_verts [i]), n, _lerp * Time.deltaTime));
		}

		_myMesh.vertices = new_verts;
		_myMesh.RecalculateNormals ();
	}

	public void Increment(int i, int j){
		for (int k = i; k <= j; k++) {
			DISTORTIONS [k]++;
		}
	}
}
