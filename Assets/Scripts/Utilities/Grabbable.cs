using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] private bool _isGrabbable = true;

    public bool IsGrabbable
    {
        get { return _isGrabbable; }
        set { _isGrabbable = value; }
    }

}
