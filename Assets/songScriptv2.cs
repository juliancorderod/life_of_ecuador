using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songScriptv2 : MonoBehaviour
{

	bool mute = true;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		GetComponent<AudioSource>().volume = Mathf.Clamp(GetComponent<AudioSource>().volume,0,0.7f);


		if (mute) {
			GetComponent<AudioSource> ().volume -= Time.deltaTime * 1.25f;

		} else {

			GetComponent<AudioSource> ().volume += Time.deltaTime * 0.25f;
		}

	}

	public void muteSong ()
	{

		mute = true;
	}

	public void unMuteSong ()
	{

		mute = false;
	}
}