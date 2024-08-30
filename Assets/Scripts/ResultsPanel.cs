using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsPanel : MonoBehaviour
{
    [SerializeField] ScoreHolder _scoreHolder;

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{_scoreHolder.correctAnswers} / {_scoreHolder.totalQuestions}");

        _scoreText.text = $"You got {100 * _scoreHolder.correctAnswers / _scoreHolder.totalQuestions}% of the questions right.";
        _highScoreText.text = $"The previous high score for this profile is {100 * _scoreHolder.historicHighScore}%.";
    }
}
