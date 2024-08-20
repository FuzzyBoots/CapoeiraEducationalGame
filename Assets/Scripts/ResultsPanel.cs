using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsPanel : MonoBehaviour
{
    [SerializeField] ScoreHolder _scoreHolder;

    [SerializeField] TMP_Text _successText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{_scoreHolder.correctAnswers} / {_scoreHolder.totalQuestions}");

        _successText.text = $"You got {100 * _scoreHolder.correctAnswers / _scoreHolder.totalQuestions}% of the questions right.";
    }
}
