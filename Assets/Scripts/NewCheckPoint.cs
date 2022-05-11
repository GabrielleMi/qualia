using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCheckPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PersoMainTag.persoTag))
        {
            other.gameObject.GetComponent<PersoMain>().CheckPoint = transform;
        }
    }
}
