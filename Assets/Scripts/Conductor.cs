using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    [SerializeField] float _songBpm;

    //The number of seconds for each song beat
    [SerializeField] float _secPerBeat;

    //Current song position, in seconds
    [SerializeField] float _songPosition;

    //Current song position, in beats
    [SerializeField] float _songPositionInBeats;

    //How many seconds have passed since the song started
    [SerializeField] float _dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    [SerializeField] AudioSource _musicSource;

    [SerializeField] List<float> _openNoteBeats;
    [SerializeField] List<float> _closedNoteBeats;
    [SerializeField] List<float> _buzzNoteBeats;

    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        _musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        _secPerBeat = 60f / _songBpm;

        //Record the time when the music starts
        _dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        _musicSource.Play();
    }

    void Update()
    {
        //determine how many seconds since the song started
        _songPosition = (float)(AudioSettings.dspTime - _dspSongTime);

        //determine how many beats since the song started
        _songPositionInBeats = _songPosition / _secPerBeat;
    }
}
