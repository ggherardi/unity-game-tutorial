using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if(_instance == null)
        {
            _instance = this;

            // Keep this object alive even when changing levels
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySound(AudioClip _sound)
    {
        _instance._audioSource.PlayOneShot(_sound);
    }

    public static void PlaySoundLoop(AudioClip _sound)
    {
        //_instance._audioSource.loop
    }
}
