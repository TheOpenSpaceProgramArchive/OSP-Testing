using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour {
	public string[] folders;
	public List<string> CategoryName = new List<string>();
	public List<List<string>> Parts = new List<List<string>>();
	// Use this for initialization

	//private GameObject go;

	//Directory Should be:
	//Data/[MODNAME]/Parts/[CATEGORY]/[PARTNAME]/part.cfg
	void Start() {
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
			foreach (var part in Parts[i]) {
				Debug.Log(CategoryName[i] + ": " + part);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	}
}
