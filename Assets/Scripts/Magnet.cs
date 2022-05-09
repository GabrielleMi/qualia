using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private CapsuleCollider _collider;
    private bool _isOn = true;

    public bool IsOn {
           get { return _isOn; }
           set { _isOn = value; } 
    }

    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    public void ToggleMagnet()
    {
        _isOn = !_isOn; 
    }

    public void SetMagnet(bool state)
    {
        _isOn = state;
        _collider.enabled = state;
        enabled = state;
    }

}
