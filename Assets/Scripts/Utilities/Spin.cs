using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    void Update()
    {
        transform.Rotate(0, 0, _speed * Time.deltaTime);
    }

    public void StopSpin()
    {
        enabled = false;
    }
}
