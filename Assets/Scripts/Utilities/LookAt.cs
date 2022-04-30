using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private bool _isLocked = false;
    private Vector3 _target;

    IEnumerator Look()
    {
        while (_isLocked)
        {
            Vector3 deltaPos = _target - transform.position;
            Quaternion rotation = Quaternion.LookRotation(deltaPos);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1.0f);
            yield return null;
        }
    }

    public void LockTarget(Vector3 targetPos)
    {
        _isLocked = true;
        _target = targetPos;
        StartCoroutine(Look());
    }

    public void UnlockTarget()
    {
        _isLocked = false;
    }
}
