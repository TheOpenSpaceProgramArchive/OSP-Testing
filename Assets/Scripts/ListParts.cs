using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ListParts : MonoBehaviour {

	public List<String> parts = new List<string>();

	private Dropdown _dropdown;

	private GameObject _scripts;


	public string[] folders;
	public List<string> CategoryName = new List<string>();
	public List<List<string>> Parts = new List<List<string>>();

	// Use this for initialization
	void Start () {
		_dropdown = GetComponent<Dropdown>();
		_scripts = GameObject.Find("_Scripts");


		_dropdown.onValueChanged.AddListener(delegate {
			OnValueChange(_dropdown);
		});


		folders = Directory.GetDirectories(
			Application.dataPath + "/../Data/OSP/Parts"
		);
		for (int i = 0; i < folders.Length; i++) {
			CategoryName.Add(folders[i].Substring(folders[i].LastIndexOf("\\") + 1));

			Parts.Add(
				new List<string>(Directory.GetDirectories(
					Application.dataPath + "/../Data/OSP/Parts/" + CategoryName[i])
				)
			);
		}

		for (int i = 0; i < CategoryName.Count; i++) {
			for (int j = 0; j < Parts[i].Count; j++) {
				var part = Parts[i][j];
				parts.Add(part);
				_dropdown.options.Add(new Dropdown.OptionData() {
					text = ExtensionMethods.GetDetails(Parts[i][j])["Name"].ToString()
				});
			}
		}
		_scripts.GetComponent<MouseManager>().path = parts[0];
	}
	
	private void OnValueChange(Dropdown target) {
		_scripts.GetComponent<MouseManager>().path = parts[target.value];
	}
}
