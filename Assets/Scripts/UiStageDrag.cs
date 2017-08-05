using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiStageDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private float startXPos;

	private GameObject Placeholder = null;

	public GameObject Parent;

	private LayoutElement _layoutElement;
	private RectTransform _rectTransform;

	private CanvasGroup _canvasGroup;
	// Use this for initialization
	void Start () {
		startXPos = transform.position.x;
		Parent = transform.parent.gameObject;
		_layoutElement = GetComponent<LayoutElement>();
		_rectTransform = GetComponent<RectTransform>();

		GetComponent<Image>().color = Random.ColorHSV();

		_canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		/*transform.position = new Vector2(
			startXPos,
			transform.position.y
		);*/
	}

	public void OnBeginDrag(PointerEventData eventData) {
		Placeholder = new GameObject();
		Placeholder.transform.SetParent(Parent.transform);
		Placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
		LayoutElement pLE = Placeholder.AddComponent<LayoutElement>();
		RectTransform ptransform = Placeholder.GetComponent<RectTransform>();
		ptransform.sizeDelta = new Vector2(
			_rectTransform.rect.height,
			_rectTransform.rect.width
		);
		pLE.preferredWidth = _layoutElement.preferredWidth;
		pLE.preferredHeight = _layoutElement.preferredHeight;
		pLE.flexibleWidth = 0;
		pLE.flexibleHeight = 0;
		transform.parent = transform.root;

		_canvasGroup.blocksRaycasts = false;

	}

	public void OnDrag(PointerEventData eventData) {
		Placeholder.transform.SetParent(Parent.transform);
		transform.position = new Vector2(
			transform.position.x,
			eventData.position.y
			);

		for (int i = 0; i < Parent.transform.childCount; i++) {
			if (i != 0) {
				if (transform.position.y > Parent.transform.GetChild(i).position.y) {
					Placeholder.transform.SetSiblingIndex(i);
					break;
				}
			}
		}

	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log("EndDrag");
		transform.parent = Parent.transform;
		transform.SetSiblingIndex(Placeholder.transform.GetSiblingIndex());
		Destroy(Placeholder.gameObject);
		_canvasGroup.blocksRaycasts = true;
	}

}
