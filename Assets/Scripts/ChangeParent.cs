using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParent : MonoBehaviour {
	public GameObject[] Bodies;

	[SerializeField]
	private int i = 0;

	[SerializeField]
	private float speed = 1;
	
	// Use this for initialization
	void Start () {
		Bodies = GameObject.FindGameObjectsWithTag("CelestialBody");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			if (i + 1 > Bodies.Length -1) {
				i = 0;
			}
			else {
				i +=1;
			}
			transform.parent = Bodies[i].transform;
			transform.localPosition = Vector3.zero + Vector3.back * 10;
		}
	}
}
