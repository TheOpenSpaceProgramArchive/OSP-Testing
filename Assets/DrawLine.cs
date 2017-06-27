using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class DrawLine : MonoBehaviour {

	private LineRenderer lr;

	[SerializeField]
	private GameObject vessel;


	[Range(1f, 10f)]
	[SerializeField]
	private float length = 1;

	private Rigidbody rb;

	private Vessel _vessel;
	// Use this for initialization
	void Start() {
		lr = GetComponent<LineRenderer>();
		rb = vessel.GetComponent<Rigidbody>();
		_vessel = vessel.GetComponent<Vessel>();
	}

	// Update is called once per frame
	void Update () {
		if (_vessel.SAS == "Prograde") {
			lr.SetPosition(0, vessel.transform.position);
			lr.SetPosition(1, vessel.transform.position + rb.velocity.normalized * rb.velocity.magnitude);
		}
		if (_vessel.SAS == "Retrograde") {
			lr.SetPosition(0, vessel.transform.position);
			lr.SetPosition(1, vessel.transform.position - rb.velocity.normalized * rb.velocity.magnitude);
		}

	}
}
