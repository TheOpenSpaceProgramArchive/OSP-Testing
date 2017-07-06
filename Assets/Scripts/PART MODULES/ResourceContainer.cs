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

	[SerializeField]
	public float Oxidizer;
	private float ODensity = 0.2f;

	private float maxLiquidFuel;



	private bool pump = false;
	private float flowrate = 100; //TODO: CHANGE THIS VALUE WHEN BALANCING

	private ResourceContainer parentRes;
	// Use this for initialization
	void Start () {
		maxLiquidFuel = LiquidFuel;
		part = GetComponent<Part>();
		if (part.Parent.GetComponent<ResourceContainer>()) {
			pump = true;
			parentRes = part.Parent.GetComponent<ResourceContainer>();
		}


	}
	
	// Update is called once per frame
	void Update () {
		part.mass = DryMass +
			        LiquidFuel * LFDensity +
			        Oxidizer * ODensity;
		if (pump && LiquidFuel < maxLiquidFuel) {
			part.Parent.SendMessage("SendFuel", new ResourceParams(
				"LiquidFuel",
				Mathf.Abs(flowrate * Time.deltaTime),
				gameObject)
			);
		}
	}

	public void SendFuel(ResourceParams data) {
		switch (data.Type) {
			case "LiquidFuel":
				if (LiquidFuel - data.Amount > 0) {
					LiquidFuel -= data.Amount;
					data.Sender.SendMessage("ReceiveFuel", data);
				} else if (LiquidFuel >= 0) {
					data.Amount = LiquidFuel;
					LiquidFuel = 0;
					data.Sender.SendMessage("ReceiveFuel", data);
				}
				break;

			default:
				Debug.LogError("SendFuel Recives Default");
				break;

		}
	}

	public void ReceiveFuel(ResourceParams data) {
		switch (data.Type) {
			case "LiquidFuel":
				if (data.Amount >= 0) {
					LiquidFuel += data.Amount;
				}
				break;

			default:
				Debug.LogError("ReceiveFuel Recives Default");
				break;
		}

	}
}
