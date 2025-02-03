using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidEventRaiser : MonoBehaviour
{
    [SerializeField] VoidEvent eventToRaise;

    public void RaiseEvent()
    {
        eventToRaise.Raise();
    }
}
