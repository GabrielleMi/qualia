using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PersoMainTag.persoTag)
        {
            ScenesManager.Instance.ChangeScene("BetaText");
        }
    }
}
