using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ColorEvent : UnityEvent<Color> { }

public class Zone : MonoBehaviour
{
    [SerializeField] protected Transform _endPoint;
    public enum Type { Trigger, Danger, ToActivate }
    public ColorEvent ColourEvent;

    protected Color _colour;

    public Color Colour
    {
        get { return _colour; }
        set {
            _colour = value;
            ChangeColour();
        }
    }

    protected virtual void ChangeColour(){}

    public Transform EndPoint
    {
        get { return _endPoint; }
    }

    public virtual Type GetZoneType()
    {
        throw new System.Exception();
    }
}
