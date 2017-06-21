using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {
	public GameObject _leaf;

	[Range(0, 1000)]
	public int _num_leaves;

	public float _min_dist;
	public float _max_dist;

	[Range(0f, 5f)]
	public float _leaf_scale;

	public float _percent;

	public Sprite[] _pos_leaves;

	// Use this for initialization
	void Start () {
		_num_leaves = (int)(_percent * _num_leaves);

		SpawnLeaves ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnLeaves(){
		GameObject _new_Leaf;
		float _pos;
		float _radius;

		Vector3 _position = Vector3.zero;

		for (int i = 0; i < _num_leaves; i++) {
			_pos = Random.Range (0f, 1f);
			_pos = Mathf.Clamp (_pos + .3f, 0f, 1f);
			_radius = _min_dist + (_max_dist - _min_dist) * _pos * _pos;

			Debug.Log (GetComponent<MeshFilter> ().mesh.bounds.size);
			_position = transform.position + GetComponent<MeshFilter> ().mesh.bounds.size.x * transform.localScale.x * -transform.right * _pos + Random.insideUnitSphere * _radius - transform.up;

			_new_Leaf = Instantiate (_leaf, _position, _leaf.transform.rotation, transform) as GameObject;
			_new_Leaf.transform.localScale *= Random.Range (1f, _leaf_scale);
			_new_Leaf.transform.eulerAngles = Random.insideUnitSphere*360f;
			_new_Leaf.GetComponent<SpriteRenderer> ().sprite = _pos_leaves [Random.Range (0, _pos_leaves.Length)];
		}
	}
}
