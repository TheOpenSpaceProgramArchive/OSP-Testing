using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour {
	[SerializeField]
	public float isp = 250f;       //s

	public float gravity = -9.807f; //m/s^2
	[SerializeField]
	public float mfr = 0f;   //fuel/s
	[SerializeField]
	public float thrust = 168750; //Newtons
	[SerializeField]
	public float throtle = 0;

	[SerializeField]
	public float exhaustvelocity = 0f;

	[SerializeField]
	public Vector3 Vel;
	[SerializeField]
	public float TTW = 0f;


	private Rigidbody rb;

	private Vessel vessel;
	// Use this for initialization
	void Start () {
		rb = transform.root.GetComponent<Rigidbody>();
		vessel = transform.root.GetComponent<Vessel>();
	}
	
	// Update is called once per frame
	void Update() {
		throtle = vessel.Throtle;

		if (thrust != 0f) {
			mfr = thrust / (isp * gravity) * throtle / 100;

			exhaustvelocity = gravity * isp;
			//Thrust=Mass ejection rate×Speed of ejection
			TTW = (mfr * exhaustvelocity) / (vessel.TotalMass * -gravity);

			rb.AddForce(TTW * gravity * -transform.parent.up, ForceMode.Force);
		}

	}
}
