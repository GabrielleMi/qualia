using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTowards : MonoBehaviour
{
    [SerializeField] private Transform _wp1;
    [SerializeField] private Transform _wp2;

    private float _speed = 0.025f;

    void Start()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while (true)
        {
            while (transform.position.x != _wp1.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_wp1.position.x, transform.position.y, transform.position.z), _speed);
                yield return null;
            }

            transform.position = new Vector3(_wp2.position.x, transform.position.y, transform.position.z);
            yield return null;
        }
        
    }

}
