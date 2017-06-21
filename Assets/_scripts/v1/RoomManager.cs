using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
	private ObjectManager sc_Manager;

	public GameObject _tiedCollider;

	// Use this for initialization
	void Start () {
		sc_Manager = GetComponentInParent<ObjectManager> ();
	}


}
