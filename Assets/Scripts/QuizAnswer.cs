using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class QuizAnswer : MonoBehaviour
{
    [SerializeField] bool _isCorrectAnswer;
    [SerializeField] TMP_Text _text;

    private void Start()
    {
        if (_text == null)
        {
            _text = transform.GetComponentInChildren<TMP_Text>();

            Assert.IsNotNull( _text , "Toggle text could not be set");
        }
    }

    public void HandleClick()
    {
        QuizManager.Instance.HandleAnswer(_isCorrectAnswer);
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetCorrectAnswer(bool correctAnswer)
    {
        _isCorrectAnswer = correctAnswer;
    }
}
