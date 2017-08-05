using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCollision : MonoBehaviour {
	private Part part;
	// Use this for initialization
	void Start () {
		part = transform.parent.GetComponent<Part>();
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log("Collision: " +
		          transform.parent.name +
		          " " +
		          transform.root.GetComponent<Rigidbody>().velocity
		);
	}
}
