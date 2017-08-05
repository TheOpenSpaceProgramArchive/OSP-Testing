using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timescale : MonoBehaviour {

	[SerializeField]
	[Range(0, 5)] 
	private float timescale = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = timescale;
		if (Input.GetKeyDown(KeyCode.Comma)) {
			timescale = Mathf.Clamp(timescale - 1, 0, 5);
		}
		if (Input.GetKeyDown(KeyCode.Period)) {
			timescale = Mathf.Clamp(timescale + 1, 0, 5);
		}
	}
}
