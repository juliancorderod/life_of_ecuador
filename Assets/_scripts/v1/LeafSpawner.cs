using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour {
	public GameObject pr_leaf;

	public float _radius;
	public float _height;

	public Sprite[] _pos_leaves;


	[Range(0f, 5f)]
	public float _scale;

	[Range(0, 3000)]
	public int _leaves;

	// Use this for initialization
	void Start () {
		GameObject newLeaf;
		float _angle;
		Vector3 _pos;

		for (int i = 0; i < _leaves; i++) {
			_angle = Random.Range (0f, Mathf.PI * 2f);
			_pos = new Vector3 (Mathf.Cos(_angle)*_radius*Mathf.Clamp(Random.Range(0f,5f), 0, 1f), Random.Range (-_height / 2f, _height / 2f), Mathf.Sin(_angle)*_radius*Mathf.Clamp(Random.Range(0f,5f), 0f, 1f));
			newLeaf = Instantiate (pr_leaf, transform.position + _pos, pr_leaf.transform.rotation, transform) as GameObject;
			newLeaf.transform.localScale *= Random.Range (1f, _scale);
			newLeaf.transform.eulerAngles = Random.insideUnitSphere*360f;
			newLeaf.GetComponent<SpriteRenderer> ().sprite = _pos_leaves [Random.Range (0, _pos_leaves.Length)];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
