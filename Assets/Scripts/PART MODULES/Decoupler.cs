using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Decoupler : MonoBehaviour {
	[SerializeField] public GameObject Parent = null;

	private Staged staged;
	public bool HasStaged = false;

	public List<GameObject> kids = new List<GameObject>();

	// Use this for initialization
	void Start() {
		staged = GetComponent<Staged>();
	}

	// Update is called once per frame
	void Update() {
		if (staged.IsStaged && !HasStaged) {
			HasStaged = true;
			Rigidbody oldparentrb = transform.parent.GetComponent<Rigidbody>();
			foreach (var kid in kids) {
				if (kid.transform.IsChildOf(transform.parent)) {
					kid.transform.parent = transform;
				}
			}
			transform.parent = null;
			Rigidbody rb = gameObject.AddComponent<Rigidbody>();
			Vessel vessel = gameObject.AddComponent<Vessel>();
			vessel.Start();
			rb.velocity = oldparentrb.velocity;

			rb.AddForceAtPosition(-transform.up * 2, transform.position, ForceMode.Impulse);
		}
	}
}