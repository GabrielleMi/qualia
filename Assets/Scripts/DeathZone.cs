using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PersoMainTag.persoTag))
        {
            collision.gameObject.GetComponent<PersoMain>().Die();
        }
    }

}
