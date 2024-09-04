using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsAudioManager : MonoBehaviour
{
    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
