using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ReactionWheel : MonoBehaviour {

	private Rigidbody rb;

	private Vessel vessel;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		vessel = GetComponent<Vessel>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(
			Input.GetAxis("Vertical"),
			-Input.GetAxis("Roll"),
			-Input.GetAxis("Horizontal")
		);
/*
		if (vessel.SAS == "Prograde") {
			transform.rotation = Quaternion.Lerp(
				transform.rotation,
				Quaternion.Euler(rb.velocity),
				Time.deltaTime);
		}*/
	}
}
