using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TranslationQuizManager : MonoBehaviour
{
    [SerializeField] SongObject _curSong;

    [SerializeField] GameObject _lyricsPanel;

    [SerializeField] LyricObject _linePrefab;

    private void Start()
    {
        foreach (SongLine line in _curSong._songLines)
        {
            Debug.Log($"{line._portugueseLine}\n{line._translations.ToSeparatedString("|")}");
            LyricObject lineObj = Instantiate(_linePrefab, _lyricsPanel.transform);
            lineObj.SetLyricText(line._portugueseLine);
            lineObj.SetChoices(line._translations.ToList());
        }
    }

}
