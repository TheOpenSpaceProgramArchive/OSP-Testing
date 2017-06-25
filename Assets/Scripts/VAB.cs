using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VAB : MonoBehaviour {
	private Camera cam;

	private Vector2 mousepos;
	private Vector2 worldpos;

	// Use this for initialization
	void Start() {
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update() {
		mousepos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit);
			if (hit.collider != null) {

				Instantiate(Resources.Load("Marker"), hit.point, Quaternion.identity);

			}
		}
	}
}
