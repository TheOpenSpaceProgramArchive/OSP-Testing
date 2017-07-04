using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
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
	private GameObject flame;
	private Vessel vessel;
	private Staged staged;
	private Part part;
	private ResourceContainer parentRes;

	private bool pump;
	// Use this for initialization
	public void Start () {
		rb = transform.root.GetComponent<Rigidbody>();
		vessel = transform.root.GetComponent<Vessel>();
		flame = transform.GetComponentInChildren<ParticleSystem>(true).gameObject;
		staged = GetComponent<Staged>();
		part = GetComponent<Part>();

		if (part.Parent.GetComponent<ResourceContainer>()) {
			parentRes = part.Parent.GetComponent<ResourceContainer>();
			pump = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (staged.IsStaged) {
			if (pump && parentRes.LiquidFuel > 0) {
				throtle = vessel.Throtle;
				if (thrust != 0f) {
					mfr = thrust / (isp * gravity) * throtle / 100;

					exhaustvelocity = gravity * isp;
					//Thrust=Mass ejection rate×Speed of ejection
					TTW = (mfr * exhaustvelocity) / (vessel.TotalMass * -gravity);

					//OLD: vessel.UsedFuel += mfr;
					/*if (parentRes.LiquidFuel + mfr >= 0) {
						parentRes.LiquidFuel += mfr;
					} else if (parentRes.LiquidFuel > 0) {
						parentRes.LiquidFuel = 0;
					}*/



					rb.AddForceAtPosition(
						TTW * gravity * transform.forward,
						transform.position,
						ForceMode.Force
					);
				}
				flame.SetActive(throtle > 0);
			}
			else {
				flame.SetActive(false);
			}

		}
	}
}
