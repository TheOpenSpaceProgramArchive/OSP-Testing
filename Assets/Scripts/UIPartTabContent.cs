using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPartTabContent : MonoBehaviour {
	RectTransform rt;
	// Use this for initialization
	void Start () {
	rt = GetComponent<RectTransform>();
		//TODO: AT the momment, 500 = width of transform.parent.parent
		int w = 500;
		int n = transform.parent.parent.childCount;
		int i = transform.parent.GetSiblingIndex();
		rt.anchoredPosition = new Vector2(
			w/2-w/2*(i+1),
			-200
			);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
