using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PersoMainTag.persoTag))
        {
            ScenesManager.Instance.ChangeScene("Level1");
        }
    }
}
