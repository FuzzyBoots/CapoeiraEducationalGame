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
    [SerializeField] private AudioMixer _audioMixer;

    public static AudioManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Debug.Log("Audio Manager Awake on " + SceneManager.GetActiveScene().name);
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            _audioSourceMusic.Play();
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
        float decibels = Mathf.Log10(value) * 20f;
        _audioMixer.SetFloat("MusicVolume", decibels);
    }

    public void SetSFXVolume(float value)
    {
        float decibels = Mathf.Log10(value) * 20f;
        _audioMixer.SetFloat("SFXVolume", decibels);
    }
}
