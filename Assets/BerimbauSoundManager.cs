using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerimbauSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip _openNote;
    [SerializeField] AudioClip _closedNote;
    [SerializeField] AudioClip _buzzNote;

    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        if (_audioSource == null)
        {
            if (!TryGetComponent<AudioSource>(out AudioSource source))
            {
                Debug.LogError("Could not set up audio source.");
            } else
            {
                _audioSource = source;
            }

        }
    }

    public void PlayOpenNote()
    {
        _audioSource.PlayOneShot(_openNote);
    }

    public void PlaClosednNote()
    {
        _audioSource.PlayOneShot(_closedNote);
    }

    public void PlayBuzzNote()
    {
        _audioSource.PlayOneShot(_buzzNote);
    }
}
