using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
	[SerializeField] public string load = "Box";
	[SerializeField] private bool test = false;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (test) {
			GameObject[] snappoints = GameObject.FindGameObjectsWithTag("Snappoint");
			foreach (GameObject snappoint in snappoints) {
				Destroy(snappoint.gameObject);
			}
			test = false;
		}
		if (Input.GetMouseButtonDown(0)) {
			Camera cam = Camera.main;

			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;


			if (Physics.Raycast(ray, out hit)) {
				Debug.Log(hit.collider.tag);
				Debug.Log(hit.collider.name);
				if (hit.collider.tag == "Snappoint") {
					Instantiate(
						Resources.Load("Parts/" + load),
						hit.collider.transform.position +
						hit.collider.transform.rotation * Vector3.forward * 0.5f,
						hit.collider.transform.rotation,
						hit.collider.transform.root
					);
				}
			}
		}
	}
}
