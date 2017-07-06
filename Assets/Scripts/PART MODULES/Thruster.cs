using UnityEngine;

public class Thruster : MonoBehaviour {
	public float ISP = 250f;       //s
	public float gravity = 9.80665f; //m/s^2
	public float MassFlowRate = 0f;   //fuel/s
	public float Thrust = 0f; //Newtons
	public float Throtle = 0;
	public float ExhaustVelocity = 0f;
	public Vector3 Vel;
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
	void Update() {
		if (staged.IsStaged) {
			Throtle = vessel.Throtle;
			if (Throtle > 0f) {
				ExhaustVelocity = gravity * ISP;
				MassFlowRate = Thrust / (ExhaustVelocity) * Throtle / 100;
				//Thrust = MassFlowRate * ExhaustVelocity * Throtle / 100;

			parentRes.SendMessage("SendFuel", new ResourceParams(
				"LiquidFuel",
				Mathf.Abs(MassFlowRate * Throtle/100 * Time.deltaTime),
				gameObject)
			);
			}
		}
		else {
			flame.SetActive(false);
		}
	}

	public void ReceiveFuel(ResourceParams data) {
		switch (data.Type) {
			case "LiquidFuel":
				if (data.Amount > 0) {
					rb.AddForceAtPosition(
						//Should make this use something relative to the amount of fuel recived
						(MassFlowRate * ExhaustVelocity * Throtle / 100) * -transform.forward,
						transform.position,
						ForceMode.Force
					);
					flame.SetActive(true);
				}
				else {
					flame.SetActive(false);
				}
				break;

			default:
				Debug.LogError("ReceiveFuel Recives Default");
				break;
		}

	}
}

