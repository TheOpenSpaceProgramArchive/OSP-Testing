using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Text.RegularExpressions;
using LitJson;


//This is really interesting
//Apparently I can use this to make new functions for Unity, can call those anywhere in the code
public static class ExtensionMethods {
	/// <summary>
	///   <para>Returns "a" rotated by "b".</para>
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	public static Vector3 RotateXZ(this Vector3 v, float degrees) {
		float radians = degrees * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);
         
		float tx = v.x;
		float ty = v.z;
 
		return new Vector3(cos * tx - sin * ty, 0, sin * tx + cos * ty);
	}

	/// <summary>
	///   <para>Instantiates a model located in "path" folder, adds textures,
	///         colliders and returns it as a GameObject.</para>
	/// </summary>
	/// <param name="path"></param>
	public static GameObject InstantiateOut(string path) {

		//This... Is my masterpiece. This is what I will be remembered for.
		path = path + path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal));
		Mesh newmesh = ObjImporter.Instance.ImportFile(path + ".obj");

		GameObject go = new GameObject();
		MeshFilter goMeshFilter = go.AddComponent<MeshFilter>();
		MeshRenderer goMeshRenderer = go.AddComponent<MeshRenderer>();
		MeshCollider goMeshCollider =go.AddComponent<MeshCollider>();
		goMeshFilter.mesh = newmesh;
		goMeshCollider.sharedMesh = newmesh;
		goMeshCollider.convex = true;
		goMeshRenderer.material = Resources.Load<Material>("Standard");
		goMeshRenderer.material.mainTexture = TextureLoader.LoadTexture(path + ".png");
		GameObject goparent = new GameObject();
		go.transform.parent = goparent.transform;
		go.name = "Graphics";
		goparent.name = path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal) + 1);
		ParseCfg(go, path);
		goparent.tag = "Part";
		return goparent;
	}

	public static GameObject LoadModel(string path, Vector3 pos, Quaternion rot, Transform goparent) {
		path = path + path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal));
		Mesh newmesh = ObjImporter.Instance.ImportFile(path + ".obj");

		GameObject go = new GameObject();
		MeshFilter goMeshFilter = go.AddComponent<MeshFilter>();
		MeshRenderer goMeshRenderer = go.AddComponent<MeshRenderer>();
		goMeshFilter.mesh = newmesh;
		goMeshRenderer.material = Resources.Load<Material>("Standard");
		goMeshRenderer.material.mainTexture = TextureLoader.LoadTexture(path + ".png");
		go.name = path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal) + 1);

		go.transform.position = pos;
		go.transform.rotation = rot;
		go.transform.parent = goparent;
		return go;
	}

	public static GameObject InstantiateOut(string path, Vector3 pos, Quaternion rot, Transform goparent) {
		GameObject go = InstantiateOut(path);
		go.transform.position = pos;
		go.transform.rotation = rot;
		go.transform.parent = goparent;
		return go;
	}

	public static GameObject InstantiateOut(string path, Vector3 pos, Quaternion rot) {
		GameObject go = InstantiateOut(path);
		go.transform.position = pos;
		go.transform.rotation = rot;
		return go;
	}

	private static float floatParse(LitJson.JsonData data) {
		return float.Parse(data.ToString());
	}
	private static Vector3 VectorParse(LitJson.JsonData data) {
		return new Vector3(
			floatParse(data[0]),
			floatParse(data[1]),
			floatParse(data[2])
			);
	}

	public static JsonData GetDetails(string path) {
		path = path + path.Substring(path.LastIndexOf("\\", StringComparison.Ordinal));
		string jsonString = File.ReadAllText(path + ".cfg");
		return JsonMapper.ToObject(jsonString);
	}

	private static void ParseCfg(GameObject go, string path) {
		string jsonString = File.ReadAllText(path + ".cfg");
		JsonData data = JsonMapper.ToObject(jsonString);
		IDictionary tdictionary = data;

		if (tdictionary.Contains("Part")) {
			Part part = go.transform.parent.gameObject.AddComponent<Part>();
			part.mass = floatParse(data["Part"]["Mass"]);

			go.transform.localPosition = VectorParse(data["Part"]["Position"]);
			go.transform.localRotation = Quaternion.Euler(VectorParse(data["Part"]["Rotation"]));
			go.transform.localScale = VectorParse(data["Part"]["Scale"]);
		}

		if (tdictionary.Contains("Thruster")) {
			Thruster thruster = go.transform.parent.gameObject.AddComponent<Thruster>();
			thruster.enabled = false;
			thruster.isp = floatParse(data["Thruster"]["ISP"]);
			thruster.thrust = floatParse(data["Thruster"]["Thrust"]);
			GameObject flame = (GameObject) Object.Instantiate(
				Resources.Load("Flame"),
				VectorParse(data["Thruster"]["Flame"]["Position"]),
				Quaternion.identity,
				go.transform.parent
			);
			flame.SetActive(false);

			go.transform.parent.gameObject.AddComponent<Staged>();
		}

		if (tdictionary.Contains("ResourceContainer")) {
			ResourceContainer resourceContainer = go.transform.parent.gameObject.AddComponent<ResourceContainer>();
			resourceContainer.DryMass = floatParse(data["Part"]["Mass"]);
			resourceContainer.LiquidFuel = floatParse(data["ResourceContainer"]["LiquidFuel"]);
			resourceContainer.Oxidizer = floatParse(data["ResourceContainer"]["Oxidizer"]);
		}

		if (tdictionary.Contains("Nodes")) {
			foreach (JsonData node in data["Nodes"]) {
				Object.Instantiate(
					Resources.Load("Snappoint"),
					VectorParse(node["Position"]),
					Quaternion.Euler(VectorParse(node["Rotation"])),
					go.transform.parent
				);
			}
		}
		if (tdictionary.Contains("Decoupler")) {
			go.transform.parent.gameObject.AddComponent<Decoupler>();
			go.transform.parent.gameObject.AddComponent<Staged>();
		}
	}
}
 