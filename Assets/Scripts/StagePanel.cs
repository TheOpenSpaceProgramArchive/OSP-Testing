using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StagePanel : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IDropHandler {
	private RectTransform _rectTransform;
	// Use this for initialization
	void Start () {
		_rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		_rectTransform.sizeDelta  = new Vector2(
			60,
			(transform.childCount - 1) * 35 + 7 + 15
			);

		if (transform.childCount <= 1) {
			Destroy(gameObject);
		}
	}



	public void OnDrop(PointerEventData eventData) {
			if (transform.GetChild(1).name == "PlaceHolder") {
				Destroy(transform.GetChild(1).gameObject);
		}
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if (eventData.pointerDrag != null) {
			if (eventData.pointerDrag.GetComponent<UiStageDrag>()) {
				eventData.pointerDrag.GetComponent<UiStageDrag>().Parent = gameObject;
			}
		}

	}

	public void OnPointerExit(PointerEventData eventData) {

	}
}
