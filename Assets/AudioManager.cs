using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public static AudioManager Instance
    {
        get;
        private set;
    }

    public static GameObject createGameObject()
    {
        var obj = new GameObject("Audio Manager");
        obj.AddComponent<AudioManager>();
        return obj;
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

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        throw new System.NotImplementedException();
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
