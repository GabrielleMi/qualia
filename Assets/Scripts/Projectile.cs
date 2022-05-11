using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 0.25f;
    private Vector3 _target;

    public Vector3 Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public float Speed
    {
        set { _speed = value; }
    }

    void Start()
    {
        StartCoroutine(Move());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CharacterBody"))
        {
            collision.gameObject.GetComponent<Perso>().Dmg();
        }

        if (!collision.gameObject.CompareTag("Drone"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Move()
    {
        while (transform.position != _target)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed);
            yield return null;
        }

        Destroy(gameObject);
    }
}
