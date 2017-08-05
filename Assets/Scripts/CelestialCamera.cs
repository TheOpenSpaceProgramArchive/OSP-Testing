using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialCamera : MonoBehaviour {
	private Camera mainCamera;

	private Vector3 oldpos = Vector3.zero;
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		oldpos = mainCamera.transform.root.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = mainCamera.transform.rotation;

		transform.position += (mainCamera.transform.root.position - oldpos)/10000;
		oldpos = mainCamera.transform.root.position;
	}
}
