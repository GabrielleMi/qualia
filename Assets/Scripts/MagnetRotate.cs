using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetRotate : MonoBehaviour
{
    private const float HALF_CIRCLE_ANGLE = 180.0f;
    private PersoMain _perso;

    void Start()
    {
        _perso = GetComponentInChildren<PersoMain>();
    }

    IEnumerator RotateVert()
    {
        float angleX;
        float targetAngle = _perso.IsMagnetAttracted ? HALF_CIRCLE_ANGLE : 0;

        while (transform.localRotation.x != targetAngle)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(targetAngle, 0, 0), 3.0f);
            yield return null;
        }
    }

    public void Rotate() { 
        StartCoroutine(RotateVert());
    }
}
