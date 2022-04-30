using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Trigger : UnityEvent<bool> { }

public class TriggerZone : Zone
{
    public Trigger TriggerEvent;

    void Start()
    {
        ColourEvent.Invoke(_colour);
    }

    public void Toggle(bool state)
    {
        TriggerEvent.Invoke(state);
    }

    protected override void ChangeColour()
    {
        ColourEvent.Invoke(_colour);
    }

    public override Type GetZoneType()
    {
        return Type.Trigger;
    }
}
