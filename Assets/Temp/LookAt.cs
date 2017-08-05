using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
	[SerializeField]
	private Transform target;

	[SerializeField]
	private int dist = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position;
		transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			-dist);	
	}
}
