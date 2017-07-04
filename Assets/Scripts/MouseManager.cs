using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
// ReSharper disable All

public class MouseManager : MonoBehaviour {
	[SerializeField]
	public string path = null;
	[SerializeField]
	public bool test = false;
	[SerializeField]
	private GameObject _vessel;

	[SerializeField]
	private int symetry = 1;
	private Camera cam;
	RaycastHit hit;

	private GameObject gizmo = null;

	private GameObject translationGizmo = null;

	private int LastStage = 0;

	private bool isblank = true;
	// Use this for initialization
	void Start() {
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update() {
		if (isblank) {
			if (path != null && path != "") {
				_vessel = ExtensionMethods.InstantiateOut(
					path,
					Vector3.zero,
					Quaternion.Euler(-90,0,0)
				);
				Vessel vessel = _vessel.AddComponent<Vessel>();
				vessel.enabled = false;
				Rigidbody rb = _vessel.AddComponent<Rigidbody>();
				rb.isKinematic = true;
				rb.useGravity = false;
				DrawStats drawStats = _vessel.AddComponent<DrawStats>();
				drawStats.enabled = false;
				_vessel.AddComponent<ReactionWheel>();
				cam.transform.root.GetComponent<VesselCamera>().init(_vessel);

				isblank = false;
			}
		}

		//Start The Vessel
		if (test) {
			//Removes all snappoints
			GameObject[] snappoints = GameObject.FindGameObjectsWithTag("Snappoint");
			foreach (GameObject snappoint in snappoints) {
				Destroy(snappoint.gameObject);
			}
			_vessel.GetComponent<Vessel>().enabled = true;
			_vessel.GetComponent<Vessel>().Stage = LastStage;

			//Get the lowest position of the Vessel
			Transform[] vesselchilds = _vessel.GetComponentsInChildren<Transform>();
			GameObject lowestChild = _vessel;
			foreach (Transform child in vesselchilds) {
				if (child.position.y < lowestChild.transform.position.y) {
					lowestChild = child.gameObject;
				}
			}
			Mesh mesh = lowestChild.GetComponent<MeshFilter>().mesh;

			Vector3[] vertices = mesh.vertices;
				float lowest = 0f;
				int i = 0;
				while (i < vertices.Length) {
					if (vertices[i].z > lowest) lowest = vertices[i].z;
					i++;
				}

			//Loads the plane, and put it on the lowest position
				Instantiate(
					Resources.Load("Plane"),
					new Vector3(0f, lowestChild.transform.position.y - Mathf.Abs(lowest), 0f),
					Quaternion.identity
				);


			test = false;
		}

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		//Gets MouseButton and Spawns your object
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
					go.GetComponent<Part>().Parent = hit.collider.transform.parent.gameObject;

					if(hit.collider.transform.parent.GetComponent<Decoupler>() != null) {
						DecouplerKid decoupler = go.AddComponent<DecouplerKid>();
						decoupler.Parent = hit.collider.transform.parent.gameObject;
					} else
					if (hit.collider.transform.parent.GetComponent<DecouplerKid>() != null) {
						DecouplerKid decoupler = go.AddComponent<DecouplerKid>();
						decoupler.Parent = hit.collider.transform.parent.GetComponent<DecouplerKid>().Parent;
					}

					if (go.GetComponent<Staged>()) {
						go.GetComponent<Staged>().Stage = LastStage;
						LastStage++;
					}
			//Check if symetry is enabled, and spawn more objects
			if (symetry > 1) {
						if (Mathf.Abs(spawnpoint.x) > 0.1f ||
						    Mathf.Abs(spawnpoint.z) > 0.1f) {
							for (var i = 1; i < symetry; i++) {
								GameObject symgo = ExtensionMethods.InstantiateOut(
									path,
									Quaternion.AngleAxis(360 / symetry * i, Vector3.up) * spawnpoint,
									Quaternion.AngleAxis(360 / symetry * i, Vector3.up) * hit.collider.transform.rotation,
									hit.collider.transform.root
								);
								if(hit.collider.transform.parent.GetComponent<Decoupler>() != null) {
									DecouplerKid decoupler = symgo.AddComponent<DecouplerKid>();
									decoupler.Parent = hit.collider.transform.parent.gameObject;
								} else
								if (hit.collider.transform.parent.GetComponent<DecouplerKid>() != null) {
									DecouplerKid decoupler = symgo.AddComponent<DecouplerKid>();
									decoupler.Parent = hit.collider.transform.parent.GetComponent<DecouplerKid>().Parent;
								}
								if (symgo.GetComponent<Staged>()) {
									symgo.GetComponent<Staged>().Stage = LastStage;
								}
							}
						}
					}
				}
			}
		}

/* Translation Gizmos FIXME
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
		}*/
	}


	public void ChangeTest() {
		test = true;
	}
}
