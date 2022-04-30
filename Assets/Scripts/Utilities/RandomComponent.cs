using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class RandomComponent : MonoBehaviour
{
    [SerializeField] private AnalogGlitch _component;
    [SerializeField] private float _maxInterval = 15.0f;
    [SerializeField] private float _minInterval = 4.0f;

    void Start()
    {
        StartCoroutine(RandTrigger());
    }

    IEnumerator RandTrigger()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval));
            _component.enabled = !_component.enabled;
            yield return new WaitForSeconds(0.25f);
            _component.enabled = !_component.enabled;
        }
    }

}
