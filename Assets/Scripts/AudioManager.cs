using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSFX;
    [SerializeField] private AudioMixer _audioMixerMusic;

    public static AudioManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Assert.IsNotNull(_audioSourceMusic);
        Assert.IsNotNull(_audioSourceSFX);
    }

    public void PlayMusic(AudioClip clip)
    {
        _audioSourceMusic.PlayOneShot(clip);
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSourceSFX.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value)
    {
        _audioMixerMusic.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
    }

    public void SetSFXVolume(float value)
    {
        _audioMixerMusic.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
    }
}
