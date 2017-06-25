using UnityEngine;


//This is really interesting
//Apparently I can use this to make new functions for Unity, can call those anywhere in the code
public static class Vector2Extension {
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
}
 