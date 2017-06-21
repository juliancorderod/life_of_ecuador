using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
	public GameObject _branch;

	[Range(0f, 360f)]
	public float _branch_rot;

	public float _scale_mod;

	private float min_branch_scale_x_z = .5f;
	private float max_branch_scale_x_z = 5f;

	private float min_branch_scale_y = .5f;
	private float max_branch_scale_y = 3f;

	public float _min_radius;
	public float _max_radius;
	public float _height;

	[Range(0, 300)]
	public int _num_branches;

	public float _greater_than_outside;

	// Use this for initialization
	void Start () {
		SpawnBranches ();
	}

	void SpawnBranches(){
		GameObject _new_branch;
		float _pr = 0f;
		float _radius = 0f;
		float _angle = 0f;
		float _perlin = 0f;
		float _slope_xz = (max_branch_scale_x_z - min_branch_scale_x_z);
		float _slope_y = (max_branch_scale_y - min_branch_scale_y);


		Vector3 _rot = Vector3.zero;
		Vector3 _pos = Vector3.zero;
		Vector3 _scale = Vector3.zero;

		for (int i = 0; i < _num_branches; i++) {
			_pr = Random.Range (0f, 1f);
			if (_pr >= _greater_than_outside)
				_radius = _max_radius;
			else
				_radius = _min_radius;
			_angle = Random.Range (0f, Mathf.PI * 2f);
			_perlin = Mathf.PerlinNoise (Mathf.Cos (_angle), Mathf.Sin (_angle));

			_rot = new Vector3 (Random.Range (-5f, 5f), Random.Range (-15f, 15f), Random.Range (-5f, 5f));

			_pos = transform.position + Vector3.up * Random.Range (-_height / 2f, _height / 2f)
			+ Vector3.right * Mathf.Cos (_angle) * _radius + Vector3.forward * Mathf.Sin (_angle) * _radius;

			_scale = _scale_mod * new Vector3 (_perlin * _slope_xz + min_branch_scale_x_z, 
									_perlin * _slope_y + min_branch_scale_y,
									_perlin * _slope_xz + min_branch_scale_x_z);

			_new_branch = Instantiate (_branch, _pos, _branch.transform.rotation) as GameObject;
			_new_branch.transform.LookAt (transform.position);

			if(_radius == _min_radius)
				_new_branch.transform.eulerAngles += new Vector3 (0f, -90f, -30f);
			else
				_new_branch.transform.eulerAngles += new Vector3 (0f, 90f, -30f);

			_new_branch.transform.eulerAngles += _rot;

			_new_branch.transform.localScale = _scale;

			_new_branch.GetComponent<Branch> ()._percent = _perlin;
		}
	}
}
