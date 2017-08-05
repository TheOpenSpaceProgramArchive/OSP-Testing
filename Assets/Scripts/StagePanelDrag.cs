using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePanelDrag : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InstantiateStagePanel(int index) {
		GameObject newpanel = (GameObject)Instantiate(
			Resources.Load("UI/StagePanel"),
			transform.parent.parent
		);
		int test = transform.parent.GetSiblingIndex();
		newpanel.transform.SetSiblingIndex(test);
	}
}
