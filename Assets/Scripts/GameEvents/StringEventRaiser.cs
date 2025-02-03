using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringEventRaiser : MonoBehaviour
{
	[SerializeField] StringEvent eventToRaise;

	public void RaiseEvent(string s)
	{
		eventToRaise.Raise(s);
	}
}
