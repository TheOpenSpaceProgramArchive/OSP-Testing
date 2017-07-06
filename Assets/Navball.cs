using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navball : MonoBehaviour {
	private GameObject vessel = null;

	[SerializeField]
	Vector3 test = Vector3.up;

	private Rigidbody rb = null;
	// Use this for initialization
	void Start () {
		vessel = Camera.main.transform.root.GetComponent<VesselCamera>().vessel;
		if (vessel != null) {
			rb = vessel.GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (vessel != null) {
			transform.localRotation = Quaternion.LookRotation(rb.velocity, test);
		}
	}
}
