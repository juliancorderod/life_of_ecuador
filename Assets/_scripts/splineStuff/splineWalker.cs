using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineWalker : MonoBehaviour {


	public BezierSpline spline;

	[Range(0f,120f)]
	public float duration;

	private float progress;

	public bool lookForward;


	private void Update () {



		progress += Time.deltaTime / duration;
		if (progress > 1f) {
			progress -= 1f;
		}


		Vector3 position = spline.GetPoint(progress);
		transform.position = position;
		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(progress));
		}


//		Debug.Log(lerpVal);

	}

}
