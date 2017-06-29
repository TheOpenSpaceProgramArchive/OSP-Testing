using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Text.RegularExpressions;


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

	private static void ParseCfg(GameObject go, string path) {
		string text = System.IO.File.ReadAllText(path + ".cfg");
		text = text.Replace("\r\n", string.Empty).
			Replace("\n", string.Empty).
			Replace("\r", string.Empty).
			Replace(":", string.Empty).
			Replace(" ", string.Empty);
		text = Regex.Replace(text, "[A-Za-z]", "");

		string[] data = text.Split(new char[] {',' , ';' });

		Part part = go.transform.parent.gameObject.AddComponent<Part>();
		part.mass = float.Parse(data[0]);

		if (Math.Abs(float.Parse(data[1])) > 0.1) {
			Thruster thruster = go.transform.parent.gameObject.AddComponent<Thruster>();
			thruster.enabled = false;
			thruster.isp = float.Parse(data[1]);
			thruster.thrust = float.Parse(data[2]);
		}

		if (int.Parse(data[3]) == 1) {
			ResourceContainer resourceContainer = go.transform.parent.gameObject.AddComponent<ResourceContainer>();
			resourceContainer.DryMass = part.mass;
			resourceContainer.LiquidFuel = float.Parse(data[4]);
			resourceContainer.Oxidizer = float.Parse(data[5]);
		}

		go.transform.localPosition = new Vector3(
			float.Parse(data[6]),
			float.Parse(data[7]),
			float.Parse(data[8])
		);
		go.transform.localRotation = Quaternion.Euler(
			float.Parse(data[9]),
			float.Parse(data[10]),
			float.Parse(data[11])
		);
		go.transform.localScale = new Vector3(
			float.Parse(data[12]),
			float.Parse(data[13]),
			float.Parse(data[14])
		);


		for (int i = 15; i < data.Length; i += 6) {
			Object.Instantiate(
				Resources.Load("Snappoint"),
				new Vector3(
					float.Parse(data[i]),
					float.Parse(data[i+1]),
					float.Parse(data[i+2])
				),
				Quaternion.Euler(
					float.Parse(data[i+3]),
					float.Parse(data[i+4]),
					float.Parse(data[i+5])
				),
				go.transform.parent
			);
		}
	}
}
 