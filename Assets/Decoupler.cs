using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Decoupler : MonoBehaviour {
	[SerializeField]
	public GameObject Parent = null;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			if (Parent != null) {
				transform.parent = Parent.transform;
			} else {
				transform.parent = null;
				gameObject.AddComponent<Rigidbody>();
				Vessel vessel = gameObject.AddComponent<Vessel>();
				vessel.Start();
			}
		}
	}
}
