using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
// ReSharper disable All

public class MouseManager : MonoBehaviour {
	[SerializeField]
	public string path = "Box";
	[SerializeField]
	public bool test = false;
	[SerializeField]
	private GameObject Vessel;

	[SerializeField]
	private int symetry = 1;
	private Camera cam;
	RaycastHit hit;

	private GameObject gizmo = null;

	private GameObject translationGizmo = null;
	// Use this for initialization
	void Start() {
		cam = Camera.main;
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
			float lowest = 0f;
			int iasda = 0;
			while (iasda < vertices.Length) {
				if(vertices[iasda].z > lowest) lowest = vertices[iasda].z;
				iasda++;
			}
			Instantiate(
				Resources.Load("Plane"),
				new Vector3(0f, lowestChild.transform.position.y + lowest, 0f),
				Quaternion.identity
			);

			test = false;
		}
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.CompareTag("Snappoint")) {

					Vector3 spawnpoint = hit.collider.transform.position +
					                     hit.collider.transform.rotation * Vector3.forward * 0.5f;

					GameObject go = ExtensionMethods.InstantiateOut(
						path,
						spawnpoint,
						hit.collider.transform.rotation,
						hit.collider.transform.root
					);
					if (hit.collider.transform.parent.GetComponent<Decoupler>() != null) {
						Decoupler decoupler = go.AddComponent<Decoupler>();
						if (hit.collider.transform.parent.GetComponent<Decoupler>().Parent != null) {
							decoupler.Parent = hit.collider.transform.parent.GetComponent<Decoupler>().Parent;
						}
						else {
						decoupler.Parent = hit.collider.transform.parent.gameObject;
						}
					}

					if (symetry > 1) {
						if (Mathf.Abs(spawnpoint.x) > 0.1f ||
						    Mathf.Abs(spawnpoint.z) > 0.1f) {
							for (var i = 1; i < symetry; i++) {
								ExtensionMethods.InstantiateOut(
									path,
									Quaternion.AngleAxis(360 / symetry * i, Vector3.up) * spawnpoint,
									Quaternion.AngleAxis(360 / symetry * i, Vector3.up) * hit.collider.transform.rotation,
									hit.collider.transform.root
								);
							}
						}
					}
				}
			}
		}

		if (Input.GetMouseButton(0)) {
			if (gizmo != null) {
				if (gizmo.name == "X") {
					hit.collider.gameObject.transform.parent.transform.parent.Translate(
						transform.right * Input.GetAxis("Mouse X")
					);
				}
				if (gizmo.name == "Y") {
					hit.collider.gameObject.transform.parent.transform.parent.Translate(
						transform.up * Input.GetAxis("Mouse X")
					);
				}
				if (gizmo.name == "Z") {
					hit.collider.gameObject.transform.parent.transform.parent.Translate(
						transform.forward * Input.GetAxis("Mouse X")
					);
				}
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			gizmo = null;
		}

		if (Input.GetMouseButtonDown(1)) {
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.transform.parent.CompareTag("Part")) {
					//Destroy(hit.collider.transform.parent.gameObject);
					if (translationGizmo != null) {
						Destroy(translationGizmo.gameObject);
					}
					else {
						translationGizmo = (GameObject) Instantiate(
							Resources.Load("TranslationGizmo"),
							hit.collider.transform.position/2,
							hit.collider.transform.parent.rotation,
							hit.collider.transform.parent.gameObject.transform
						);
					}
				}
			}
		}
	}


	public void ChangeTest() {
		test = true;
	}
}
