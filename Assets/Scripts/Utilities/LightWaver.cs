using System.Collections;
using UnityEngine;

public class LightWaver : MonoBehaviour
{
    #region Properties

    [SerializeField] private float _maxInterval = 6.0f;
    [SerializeField] private float _maxLightIntensity = 1.5f;
    [SerializeField] private float _minInterval = 0.0f;
    [SerializeField] private float _minLightIntensity = 1.0f;
    [SerializeField] private bool _unstable = true;

    private Light _light;
    private float _randLightIntensity;

    #endregion Properties

    #region Private methods

    void Start()
    {
        _light = GetComponent<Light>();
        _randLightIntensity = Random.Range(_minLightIntensity, _maxLightIntensity);
        StartCoroutine(_unstable ? Flicker() : Waver());
    }

    void ChangeIntensity(float newIntensity)
    {
        _light.intensity = newIntensity;
        
        if (_unstable)
        {
            _randLightIntensity = Random.Range(_minLightIntensity, _maxLightIntensity);
        }
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval));
            ChangeIntensity(_randLightIntensity);
            yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval / _maxInterval) / 2);
            ChangeIntensity(_randLightIntensity);
            yield return new WaitForSeconds(Random.Range(_minInterval, (_maxInterval / _maxInterval) / 2));
            ChangeIntensity(_randLightIntensity);
            yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval / _maxInterval) / 2);
            ChangeIntensity(_maxLightIntensity);
            yield return null;
        }
    }

    IEnumerator Waver()
    {
        bool desc = true;

        while (true)
        {
            float elapsed = 0;
            float minLight = desc ? _maxLightIntensity : _minLightIntensity;
            float maxLight = desc ? _minLightIntensity : _maxLightIntensity;

            while (elapsed < _maxInterval)
            {
                ChangeIntensity(Mathf.Lerp(minLight, maxLight, elapsed / _maxInterval));
                elapsed += Time.deltaTime;
                yield return null;
            }

            desc = !desc;

        }
    }

    #endregion Private methods

}
