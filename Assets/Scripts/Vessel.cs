using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour {
	private Thruster[] Thrusters;
	private Part[] Parts;
	private ResourceContainer[] ResourceContainers;

	public float TTW;
	public float TotalThrust;
	public float TotalMass;
	public float TotalFuel;

	public float UsedFuel = 0f;

	public string SAS = "Stabilization";

	[SerializeField]
	public float Throtle = 0f;
	private Rigidbody rb;
	private float gravity = -9.807f;

	[SerializeField]
	public bool ActiveVessel;

	// Use this for initialization
	public void Start () {
		rb = GetComponent<Rigidbody>();
		Thrusters = GetComponentsInChildren<Thruster>();
		Parts = GetComponentsInChildren<Part>();
		ResourceContainers = GetComponentsInChildren<ResourceContainer>();

		rb.useGravity = true;
		rb.isKinematic = false;
		GetComponent<ReactionWheel>().enabled = true;
		GetComponent<DrawStats>().enabled = true;
		foreach (Thruster thruster in Thrusters) {
			thruster.enabled = true;
			thruster.Start();
		}
	}
	
	// Update is called once per frame
	void Update () {
		TotalThrust = 0;
		TotalMass = 0;
		TTW = 0;
		TotalFuel = 0;

		bool isFuelTaken = true;

		//Throtle Control
		Throtle += Input.GetAxis("Throtle");
		if (Input.GetKeyDown(KeyCode.Z)) {
			Throtle = 100;
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			Throtle = 0;
		}
		Throtle = Mathf.Clamp(Throtle, 0, 100f);


		foreach (var part in Parts) {
			TotalMass += part.mass;
		}
		foreach (var thruster in Thrusters) {
			TTW += thruster.mfr * thruster.exhaustvelocity;
		}
		foreach (var resourceContainer in ResourceContainers) {
			TotalFuel += resourceContainer.LiquidFuel;
			if (resourceContainer.LiquidFuel > 0 && isFuelTaken) {
				isFuelTaken = false;
				//Used fuel is thruster.MFR (Which is always negative)
				if (resourceContainer.LiquidFuel + UsedFuel * Time.deltaTime < 0) {
					resourceContainer.LiquidFuel = 0;
				}
				else {
					resourceContainer.LiquidFuel += UsedFuel * Time.deltaTime;
				}
				UsedFuel = 0;
			}
		}
		TTW /= (-gravity * TotalMass);
		if (Input.GetButtonDown("Jump")) {
			BroadcastMessage("Start",null,SendMessageOptions.DontRequireReceiver);
		}
	}

	/*void OnCollisionEnter(Collision other) {
		Debug.Log(rb.velocity.magnitude);
	}*/
}
