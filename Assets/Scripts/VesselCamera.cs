using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselCamera : MonoBehaviour {
	[SerializeField]
	public GameObject vessel;

	private float rotspeed = 5;

	private float scroll = 0;

	private float zoom = -10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vessel != null) {
			transform.position = new Vector3(
				vessel.transform.position.x,
				vessel.transform.position.y + scroll,
				vessel.transform.position.z
			);

			scroll += Input.GetAxis("Mouse ScrollWheel");
			zoom += Input.GetAxis("Zoom");

			transform.GetChild(0).transform.localPosition = Vector3.forward * zoom;

			if (Input.GetMouseButton(1)) {
				transform.eulerAngles +=
					new Vector3(
						-Input.GetAxis("Mouse Y") * rotspeed,
						Input.GetAxis("Mouse X") * rotspeed,
						0
					);
				transform.eulerAngles =
					new Vector3(
						transform.eulerAngles.x,
						transform.eulerAngles.y,
						0
					);
			}
		}
	}

	public void init(GameObject go) {
		vessel = go;
	}
}
