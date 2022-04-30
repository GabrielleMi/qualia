using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class Glitch : MonoBehaviour
{
    [SerializeField] private GameObject _cmCamera;
    [SerializeField] private AnalogGlitch _glitch;

    void Start()
    {
        GameManager.Instance.UpdateDeaths.AddListener(DeathGlitch);
    }

    void SetGlitchValues(float value)
    {
        _glitch.scanLineJitter = value * 2;
        _glitch.verticalJump = value;
        _glitch.horizontalShake = value / 2;
        _glitch.colorDrift = value / 2;
    }

    void DeathGlitch()
    {
        _glitch.enabled = true;
        StartCoroutine(Glitching(1.0f, false));
    }

    IEnumerator Glitching(float time, bool increase)
    {
        float elapsed = 0;
        float lerpStart = increase ? 0.0f : 0.3f;
        float lerpEnd = increase ? 0.3f : 0.0f;

        SoundManager.Instance.GlitchSound();

        while (elapsed < time)
        {
            float lerp = Mathf.Lerp(lerpStart, lerpEnd, (elapsed / time));

            SetGlitchValues(lerp);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _glitch.enabled = false;
    }

}
