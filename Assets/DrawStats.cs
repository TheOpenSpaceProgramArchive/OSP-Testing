using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class DrawStats : MonoBehaviour {
	private Rigidbody rb;
	private Vessel stats;
	int width = 180;
	void Start() {
		rb = GetComponent<Rigidbody>();
		stats = GetComponent<Vessel>();
	}

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(0,0,width,(6 + 1) * 20), "Vessel Stats");
		GUI.Label (new Rect (0,20,width,20), "Vertical Speed: " +
		                                   rb.velocity.y.ToString("F3") + " m/s^2");
		GUI.Label (new Rect (0,40,width,20), "Horizontal Speed: " +
		                                   (rb.velocity.x + rb.velocity.z).ToString("F3") +
		                                   " m/s^2");
		GUI.Label (new Rect (0,60,width,20), "Total Speed: " +
		                                   rb.velocity.magnitude.ToString("F3") + " m/s^2");
		GUI.Label (new Rect (0,80,width,20), "Altitude: " +
		                                   transform.position.y.ToString("F3") + " m");
		GUI.Label (new Rect (0,100,width,20), "Fuel: " +
		                                   stats.TotalFuel.ToString("F3") + " L");
		GUI.Label (new Rect (0,120,width,20), "Mass: " +
		                                      stats.TotalMass.ToString("F3") + " Kg");

		if (GUI.Button(new Rect(Screen.width - 80, 0, 80, 20), "Prograde")) {
			stats.SAS = "Prograde";
		}
		if (GUI.Button(new Rect(Screen.width - 80, 20, 80, 20), "Retrograde")) {
			stats.SAS = "Retrograde";
		}



	}
}
