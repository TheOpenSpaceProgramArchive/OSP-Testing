using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ResourceContainer : MonoBehaviour {
	private Part part;

	[SerializeField]
	public float DryMass = 500;

	[SerializeField]
	public float LiquidFuel = 0; //Liters
	private float LFDensity = 1/3f;

	private float maxLiquidFuel;

	[SerializeField]
	public float Oxidizer;
	private float ODensity = 0.2f;

	private bool pump = false;
	private float flowrate = 100; //TODO: CHANGE THIS VALUE WHEN BALANCING

	private ResourceContainer parentRes;
	// Use this for initialization
	void Start () {
		part = GetComponent<Part>();
		if (part.Parent.GetComponent<ResourceContainer>()) {
			pump = true;
			parentRes = part.Parent.GetComponent<ResourceContainer>();
		}
		maxLiquidFuel = LiquidFuel;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		part.mass = 0;
		part.mass += DryMass;
		part.mass += LiquidFuel * LFDensity;
		part.mass += Oxidizer * ODensity;

		if (pump && maxLiquidFuel > LiquidFuel + flowrate) {
			if (parentRes.LiquidFuel - flowrate >= 0) {
				parentRes.LiquidFuel -= flowrate;
				LiquidFuel += flowrate;
			} else if (parentRes.LiquidFuel > 0) {
				LiquidFuel += parentRes.LiquidFuel;
				parentRes.LiquidFuel = 0;
			}
		}
	}
}
