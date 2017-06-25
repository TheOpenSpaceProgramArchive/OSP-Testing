using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {
	[SerializeField]
	private GameObject Vessel;

	Rigidbody vesselRb;

	[SerializeField]
	private Text verticalspeed, horizontalspeed, totalspeed, altitude;
	// Use this for initialization
	void Start () {
		vesselRb = Vessel.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		verticalspeed.text = vesselRb.velocity.y.ToString("F3") + " m/s^2";
		horizontalspeed.text = (vesselRb.velocity.x + vesselRb.velocity.z).ToString("F3") + " m/s^2";
		totalspeed.text = vesselRb.velocity.magnitude.ToString("F3") + " m/s^2";
		altitude.text = Vessel.transform.position.y.ToString("F3") + " m";
	}
}
