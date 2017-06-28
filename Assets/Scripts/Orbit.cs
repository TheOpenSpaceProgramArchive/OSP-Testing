using UnityEngine;

public class Orbit : MonoBehaviour {
	
	public static float G = 6.673e-11f; //Gravitational Constant

	public float Mass = 5.98e24f;
	public float Distance;

	public float OrbitalVelocity;

	//VARIABLES:
	public float SMA = 100f;  //Semi Major Axis
	public float ECC = 0.8f;  //Eccentricity
	[Range(0,360)]
	public float INC = 0f;    //Inclination (In sexagesimal Degrees)
	[Range(0,360)]
	public float LPE = 0f;    //Longitude of Periapsis (In sexagesimal Degrees)
	public float LAN;         //Longitude of Ascending Node
	public float MNA = 0;     //Mean Anomaly at Epoch: References the position of the body
	                          //(In Radians)
	public float EPH;         //Epoch
	public GameObject REF;    //Reference Body
	//End of Variables

	public float semiMinorAxis;
	public float semiFocalAxis;

	private Vector3 PE;
	private Vector3 AP;
	[SerializeField]
	private int multiplier = 1;

	[SerializeField]
	private GameObject[] Markers = new GameObject[2];

	void Start() {
		Debug.Log(Markers.Length);
		Markers[0] = (GameObject)Instantiate(Resources.Load("Marker"));
		Markers[1] = (GameObject)Instantiate(Resources.Load("Marker"));
	}

	void FixedUpdate() {
		//Semi Mayor Axis = A
		//Semi Minor Axis = B
		//Semi Focal Axis = C
		//A^2 = B^2 + C^2
		//ECC = A/C
		//ECC * C = A
		semiFocalAxis = SMA*ECC;
		//Too Lazy to Mathf.Pow :v
		
		semiMinorAxis = Mathf.Sqrt(SMA * SMA - semiFocalAxis * semiFocalAxis);
		
		//Calculate distance between the body and the reference body
		//This helps us calculate the orbital Velocity
		Distance = Vector3.Distance(
			transform.position,
			REF.transform.position
		);
		
		//Using Kepler's Laws, we get the Orbital Velocity
		//Its divided by 10000 so it doesnt go too fast (havent worked a scale yet)
		OrbitalVelocity = Mathf.Sqrt((G*Mass)/Distance)/ (multiplier * 1000);
		
		//Getting the Angle for the position of the Vessel
		//We add a degree * Orbital velocity, because the orbit its relative to the velocity
		MNA += Time.deltaTime * (Mathf.PI / 180) * OrbitalVelocity;
		//To avoid getting numbers to big, we loop the angle.
		if (MNA > Mathf.PI * 2) {
			MNA -= Mathf.PI * 2;
		}
		//Calculate the position with center on the Reference Body
		Vector3 newpos = new Vector3(
			(Mathf.Sin(MNA) * SMA),
			0,
			(Mathf.Cos(MNA) * semiMinorAxis)
		);
		
		//Rotate the orbit in relation to the LPE
		newpos = newpos.RotateXZ(LPE);
		//Translate the orbit in relation to the Reference Body
		newpos += REF.transform.position;
		//Translate the orbit so the Reference body its on one of the foci
		newpos -= (Vector3.right * semiFocalAxis).RotateXZ(LPE);
		//Moves the body and applies the inclination
		transform.position = Quaternion.AngleAxis(INC, Vector3.forward) * newpos;


		
//PE & AP
		Markers[0].transform.position = Quaternion.AngleAxis(INC, Vector3.forward) *
			(REF.transform.position -
			(Vector3.right * (semiFocalAxis - SMA))).RotateXZ(LPE);

		Markers[1].transform.position = Quaternion.AngleAxis(INC, Vector3.forward) *
		(REF.transform.position -
		(Vector3.right * (semiFocalAxis + SMA))).RotateXZ(LPE);;

	}
}
