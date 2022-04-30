using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    private AudioSource _audio;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void GlitchSound()
    {
        _audio.Play();
    }
}
