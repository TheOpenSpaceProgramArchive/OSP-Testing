using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ReactionWheel : MonoBehaviour {
	/*INFORMATION : Researching how angular momentum works at the momment:
	* According to https://www.youtube.com/watch?v=PZdUSycZHeE :
	* Angular Momentum = Mass x Velocity x Distance
	*
	* http://formulas.tutorvista.com/physics/angular-momentum-formula.html :
	* L = IW
	* I = Momment of Inertia
	* W = Angular Velocity
	* or:
	* L = R x P (Cross Product)
	* R = Radius of the body from the axis passing through centre
	* P = is the linear momentum
	*
	* UNIT:  kg m^2/s
	*
	* https://en.wikipedia.org/wiki/Angular_momentum
	*
	* L = IW
	*
	* I = R^2 M
	* I = Momment of Inertia
	*
	* W = (R x V)/(R^2) (Cross Product)
	* W = Angular Velocity
	* r = Position Vector relative to the origin
	* v = Linear Velocity relative to the origin
	* M = mass
	*
	*
	*/
	private Rigidbody rb;

	private Vessel vessel;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		vessel = GetComponent<Vessel>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(
			Input.GetAxis("Vertical"),
			-Input.GetAxis("Roll"),
			-Input.GetAxis("Horizontal")
		);
/*
		if (vessel.SAS == "Prograde") {
			transform.rotation = Quaternion.Lerp(
				transform.rotation,
				Quaternion.Euler(rb.velocity),
				Time.deltaTime);
		}*/
	}
}
