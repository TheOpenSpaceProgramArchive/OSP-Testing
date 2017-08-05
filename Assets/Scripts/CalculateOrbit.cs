using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateOrbit : MonoBehaviour {
	private Rigidbody rb;
	private float g = -9.8f;
	float T = 0f;
	public float vf = 10;
	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		//FreeFall to get Interection with Planet:
		//Vf = V0 + gT

		float HighsetPoint = rb.velocity.y;

		T = -rb.velocity.y / g;
		//T =
		//
			Debug.Log(
				"Time: " + T +
				" YPos: " + transform.position.y + T * rb.velocity.y + 0.5f * g * T * T
				);
	}
}
