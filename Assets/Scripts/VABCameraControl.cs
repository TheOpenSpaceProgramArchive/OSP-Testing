using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VABCameraControl : MonoBehaviour {
	[SerializeField]
	private float speed = 1;

	private float rotspeed = 5;

	[SerializeField]
	private float t = 10;

	private GameObject cam;

	Vector3 dir;
	// Use this for initialization
	void Start () {
		cam = transform.GetChild(0).gameObject;
		dir = (cam.transform.position - transform.position).normalized;
	}
	
	// Update is called once per frame
	void Update () {

		//Zoming
		t -= Input.GetAxis("Mouse ScrollWheel") * speed;


		//Rotating
		if (Input.GetMouseButton(1)) {
			transform.eulerAngles +=
				new Vector3(
					-Input.GetAxis("Mouse Y") * rotspeed,
					Input.GetAxis("Mouse X") * rotspeed,
					0
				);
			transform.eulerAngles =
				new Vector3(
					transform.eulerAngles.x,
					transform.eulerAngles.y,
					0
				);
			dir = (cam.transform.position - transform.position).normalized;
		}
		//Panning WIP
		/*if (Input.GetMouseButton(2)) {
			transform.Translate(
				Input.GetAxis("Mouse X") * speed,
				Input.GetAxis("Mouse Y") * speed,
				0,
				Space.World
			);
			dir = (cam.transform.position - transform.position).normalized;
		}*/

		cam.transform.position = dir * t;
	}
}
