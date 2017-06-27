using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MouseManager : MonoBehaviour {
	[SerializeField] public string load = "Box";
	[SerializeField] public bool test = false;
	[SerializeField] private GameObject Vessel;

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
			Vessel.GetComponent<Vessel>().enabled = true;


			Transform[] vesselchilds = Vessel.GetComponentsInChildren<Transform>();
			GameObject lowestChild = Vessel;
			foreach (Transform child in vesselchilds) {
				if (child.position.y < lowestChild.transform.position.y) {
					lowestChild = child.gameObject;
				}
			}
			Mesh mesh = lowestChild.GetComponent<MeshFilter>().mesh;
			Vector3[] vertices = mesh.vertices;
			float lowest = Mathf.Infinity;
			int i = 0;
			while (i < vertices.Length) {
				if(vertices[i].y < lowest) lowest = vertices[i].y;
				i++;
			}
			Instantiate(
				Resources.Load("Plane"),
				new Vector3(0f, lowest - 0.5f, 0f),
				Quaternion.identity
			);

			test = false;
		}
		if (Input.GetMouseButtonDown(0)) {
			Camera cam = Camera.main;

			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;


			if (Physics.Raycast(ray, out hit)) {
//				Debug.Log(hit.collider.tag);
//				Debug.Log(hit.collider.name);
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

	public void ChangeTest() {
		test = true;
	}
}
