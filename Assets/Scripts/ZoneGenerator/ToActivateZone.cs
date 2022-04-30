using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ToggleActivate : UnityEvent<bool> { }

public class ToActivateZone : Zone
{
    public ToggleActivate ActivateEvent;

    private GameObject _linkedTrigger;

    public GameObject LinkedTrigger
    {
        get { return _linkedTrigger; }
        set {
            _linkedTrigger = value;
            _linkedTrigger.GetComponent<TriggerZone>().TriggerEvent.AddListener(Toggle);
        }
    }

    public override Type GetZoneType()
    {
        return Type.ToActivate;
    }

    void Start()
    {
        ColourEvent.Invoke(_colour);
    }

    protected override void ChangeColour()
    {
        ColourEvent.Invoke(_colour);
    }

    private void Toggle(bool state)
    {
        ActivateEvent.Invoke(state);
    }
}
