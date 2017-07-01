using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staged : MonoBehaviour {

	public int Stage;
	public bool IsStaged = false;

	private Vessel vessel;
	// Use this for initialization
	void Start () {
		vessel = transform.root.gameObject.GetComponent<Vessel>();
		Debug.Log(IsStaged);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!IsStaged) {
			if (vessel.Stage >= Stage) {
				IsStaged = true;
			}
		}
	}
}
