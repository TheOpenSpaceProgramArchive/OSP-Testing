using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ListParts : MonoBehaviour {

	public GameObject[] parts;

	private Dropdown _dropdown;

	private GameObject _scripts;
	// Use this for initialization
	void Start () {
		_dropdown = GetComponent<Dropdown>();
		_scripts = GameObject.Find("_Scripts");
		parts = Resources.LoadAll("Parts/", typeof(GameObject)).Cast<GameObject>().ToArray();
		foreach (GameObject part in parts) {
			Debug.Log(part);
		}
		foreach (var part in parts)
		{
			_dropdown.options.Add(new Dropdown.OptionData()
			{
				text = part.name
			});
		}

		_dropdown.onValueChanged.AddListener(delegate {
			OnValueChange(_dropdown);
		});
	}
	
	private void OnValueChange(Dropdown target) {
		_scripts.GetComponent<MouseManager>().load = parts[target.value].name;
	}
}
