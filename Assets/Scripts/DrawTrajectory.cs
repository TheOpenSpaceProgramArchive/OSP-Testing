using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrawTrajectory : MonoBehaviour {
	private LineRenderer lr;

	private int i = 1;
	
	[SerializeField]
	private bool Reset = false;

	[SerializeField]
	private bool Drawing = true;

	// Use this for initialization
	void Start () {
		lr = gameObject.AddComponent<LineRenderer>();
		lr.widthMultiplier = 15;
		lr.positionCount = 1;
		lr.SetPosition(0, transform.position);
		InvokeRepeating("AddPoint", 0.1f, 0.1f);
	}

	void AddPoint() {
		if (Drawing) {
			lr.positionCount++;
			lr.SetPosition(i, transform.position);
			i++;
		}
	}

	void Update() {
		if (Reset) {
			lr.positionCount = 0;
			i = 0;
			Reset = false;
		}
	}
}
