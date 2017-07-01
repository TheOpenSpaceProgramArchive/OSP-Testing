using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DecouplerKid : MonoBehaviour {
	[SerializeField] public GameObject Parent = null;

	private Decoupler decoupler;
	private Vessel vessel;

	// Use this for initialization
	void Start() {
		if (!GetComponent<Decoupler>()) {
			decoupler = Parent.GetComponent<Decoupler>();
			decoupler.kids.Add(gameObject);
		}
		//vessel = transform.root.GetComponent<Vessel>();
	}

	// Update is called once per frame
	void Update() {
	/*	if (decoupler.HasStaged) {
			transform.parent = Parent.transform;
		}*/
	}
}