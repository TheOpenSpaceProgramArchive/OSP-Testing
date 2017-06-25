using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionWheel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(
			Input.GetAxis("Vertical"),
			-Input.GetAxis("Roll"),
			-Input.GetAxis("Horizontal")
		);
	}
}
