using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ResourceContainer : MonoBehaviour {
	private Part part;

	[SerializeField]
	public float DryMass = 500;

	[SerializeField]
	public float LiquidFuel = 6000; //Liters
	private float LFDensity = 1/3f;

	[SerializeField]
	public float Oxidizer;
	private float ODensity = 0.2f;
	// Use this for initialization
	void Start () {
		part = GetComponent<Part>();
	}
	
	// Update is called once per frame
	void Update () {
		part.mass = 0;
		part.mass += DryMass;
		part.mass += LiquidFuel * LFDensity;
	}
}
