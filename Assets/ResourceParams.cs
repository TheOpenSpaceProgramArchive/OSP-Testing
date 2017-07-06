using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceParams {
	public string Type = "";
	public float Amount = 0f;
	public GameObject Sender = null;

	public ResourceParams(string type, float amount, GameObject sender) {
		Type = type;
		Amount = amount;
		Sender = sender;
	}
}
