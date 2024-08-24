using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranslationQuizManager : MonoBehaviour
{
    [SerializeField] SongObject _curSong;

    [SerializeField] GameObject _lyricsPanel;

    [SerializeField] LyricObject _linePrefab;

    [SerializeField] ScoreHolder _scoreHolder;

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

    public void HandleQuizCompletion()
    {
        int totalLines = 0, correctLines = 0;
        foreach(LyricObject lyric in _lyricsPanel.GetComponentsInChildren<LyricObject>())
        {
           totalLines++;
            if (lyric.IsCorrect())
            {
                correctLines++;
            }
        }
        Debug.Log($"{correctLines} out of {totalLines}");

        _scoreHolder.totalQuestions = totalLines;
        _scoreHolder.correctAnswers = correctLines;
        
        // Display Results screen
        SceneManager.LoadScene("Results Screen");
        // That screen should provide a method to go back to the main screen (or the game choice screen?)
    }
}
