using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotate : MonoBehaviour {
	private Vector3 startSize;

	private Test test;
	// Use this for initialization
	void Start () {
		test = GetComponentInParent<Test>();
		startSize = transform.localScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0,1,0);
		transform.localScale = startSize / test.scale;
	}
}
