using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PersoMainTag.persoTag))
        {
            ScenesManager.Instance.ChangeScene("Level1");
        }
    }
}
