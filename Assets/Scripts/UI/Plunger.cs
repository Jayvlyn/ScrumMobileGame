using GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool dragOnSurfaces = true;

	private GameObject m_DraggingIcon;
	private RectTransform m_DraggingPlane;

	public FloatEvent onPlungerReleased;

	[Range(1,10)] public float power = 10;

	public void OnBeginDrag(PointerEventData eventData)
	{
		var canvas = FindInParents<Canvas>(gameObject);
		if (canvas == null)
			return;

		// We have clicked something that can be dragged.
		// What we want to do is create an icon for this.
		m_DraggingIcon = new GameObject("icon", typeof(RectTransform));

		m_DraggingIcon.transform.SetParent(canvas.transform, false);
		m_DraggingIcon.transform.SetAsLastSibling();

		if(this.transform is RectTransform rt && m_DraggingIcon.transform is RectTransform newRt)
		{
			newRt.sizeDelta = rt.sizeDelta;
		}

		var image = m_DraggingIcon.AddComponent<Image>();

		image.sprite = GetComponent<Image>().sprite;
		

		if (dragOnSurfaces)
			m_DraggingPlane = transform as RectTransform;
		else
			m_DraggingPlane = canvas.transform as RectTransform;

		GetComponent<Image>().enabled = false;

		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData data)
	{
		if (m_DraggingIcon != null)
			SetDraggedPosition(data);
	}

	private void SetDraggedPosition(PointerEventData data)
	{
		if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
			m_DraggingPlane = data.pointerEnter.transform as RectTransform;

		var rt = m_DraggingIcon.GetComponent<RectTransform>();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
		{
			float newYPos = Mathf.Clamp(globalMousePos.y, transform.position.y - 600, transform.position.y);

			rt.position = new Vector3(gameObject.transform.position.x, newYPos);
			rt.rotation = m_DraggingPlane.rotation;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{

		float outputForce = (gameObject.transform.position.y - m_DraggingIcon.transform.position.y) * power;

		onPlungerReleased.Raise(outputForce);

		if (m_DraggingIcon != null)
			Destroy(m_DraggingIcon);

		GetComponent<Image>().enabled = true;
	}

	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;

		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}
}
