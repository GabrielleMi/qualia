using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{

    public void RemoveObjectFromCamera(bool state)
    {
        enabled = state;
        Destroy(gameObject);
    }
}
