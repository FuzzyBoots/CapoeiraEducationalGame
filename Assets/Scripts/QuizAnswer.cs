using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizAnswer : MonoBehaviour
{
    [SerializeField] bool _isCorrectAnswer;

    public void HandleClick()
    {
        if (_isCorrectAnswer)
        {
            QuizManager.Instance.HandleCorrectAnswer();
        } else
        {
            QuizManager.Instance.HandleIncorrectAnswer();
        }
    }
}
