using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour {
	private Thruster[] Thrusters;
	private Part[] Parts;

	[SerializeField]
	public float TTW;
	[SerializeField]
	public float TotalThrust;
	[SerializeField]
	public float TotalMass;

	[SerializeField]
	public float Throtle = 0f;

	private Rigidbody rb;

	private float gravity = -9.807f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		Thrusters = GetComponentsInChildren<Thruster>();
		Parts = GetComponentsInChildren<Part>();

		rb.useGravity = true;
		rb.isKinematic = false;
		GetComponent<ReactionWheel>().enabled = true;
		foreach (Thruster thruster in Thrusters) {
			thruster.enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		TotalThrust = 0;
		TotalMass = 0;
		TTW = 0;

		//Throtle Control
		Throtle += Input.GetAxis("Throtle");
		if (Input.GetKeyDown(KeyCode.Z)) {
			Throtle = 100;
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			Throtle = 0;
		}
		Throtle = Mathf.Clamp(Throtle, 0, 100f);


		foreach (var part in Parts) {
			TotalMass += part.GetComponent<Part>().mass;
		}
		foreach (var thruster in Thrusters) {
			TTW += thruster.GetComponent<Thruster>().mfr *
			       thruster.GetComponent<Thruster>().exhaustvelocity;
		}
		TTW /= (-gravity * TotalMass);
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log(rb.velocity.magnitude);
	}
}
