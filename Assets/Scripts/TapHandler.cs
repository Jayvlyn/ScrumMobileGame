using GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public VoidEvent activate;
	public VoidEvent deactivate;

	public void OnPointerDown(PointerEventData eventData)
	{
		activate.Raise();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		deactivate.Raise();
	}
}
