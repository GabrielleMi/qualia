using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerInDangerZone : UnityEvent<GameObject>{}

public class VisionDetection : MonoBehaviour
{
    public PlayerInDangerZone OnEnter = new PlayerInDangerZone();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PersoMainTag.persoTag)
        {
            OnEnter.Invoke(other.gameObject);
        }
    }
}
