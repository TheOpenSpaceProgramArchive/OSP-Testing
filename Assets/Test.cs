using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	public List<String> parts = new List<string>();

	public string[] folders;
	public List<string> CategoryName = new List<string>();
	public List<List<string>> Parts = new List<List<string>>();

	[SerializeField]
	private GameObject list;

	public float scale = 1;

	// Use this for initialization
	void Start () {
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

			}
		}
	}
}
