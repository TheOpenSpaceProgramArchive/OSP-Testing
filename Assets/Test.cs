using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using LitJson;


public class Test : MonoBehaviour {
	private string jsonString;
	private JsonData data;


	void Start() {
		jsonString = File.ReadAllText("D:\\Documents\\Unity\\OSP Testing\\Data\\OSP\\test.cfg");
		data = JsonMapper.ToObject(jsonString);
		IDictionary tdictionary = data as IDictionary;


	}

}
