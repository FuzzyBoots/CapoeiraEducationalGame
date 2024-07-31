using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizQuestion[] _questions;

    List<QuizQuestion> _curQuestions;
    IEnumerator<QuizQuestion> _questionIter;

    [SerializeField] TMP_Text _questionField;

    [SerializeField] GameObject _answerField;

    [SerializeField] GameObject _answerPrefab;

    public static QuizManager Instance
    {
        get;
        private set;
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
        }
    }


    void Start()
    {
        // Start up quiz
        LoadQuizQuestions();
    }

    private void LoadQuizQuestions()
    {
        _curQuestions = new List<QuizQuestion>(_questions);
        // We should shuffle those questions

        _questionIter = _curQuestions.GetEnumerator();

        LoadNextQuestion();
    }

    private bool LoadNextQuestion()
    {
        bool hasNext = _questionIter.MoveNext();
        DisplayQuestion(_questionIter.Current);

        return hasNext;
    }

    private void DisplayQuestion(QuizQuestion question)
    {
        _questionField.text = question.Question;

        List<GameObject> answers = new List<GameObject> ();

        // Clear the answer field
        while (_answerField.transform.childCount > 0)
        {
            DestroyImmediate(_answerField.transform.GetChild(0).gameObject);
        }

        foreach (string answer in question.Answers)
        {
            GameObject answerObject = Instantiate(_answerPrefab);
            QuizAnswer quizAnswer = answerObject.GetComponent<QuizAnswer>();
            quizAnswer.SetText(answer);
            answers.Add (answerObject);
        }

        if (answers.Count > 0)
        {
            answers[0].GetComponent<QuizAnswer>().SetCorrectAnswer(true);

            // Shuffle them
            for (int i = answers.Count - 1; i> 0 ; i--)
            {
                int randIndex = UnityEngine.Random.Range(0, answers.Count);

                GameObject swap = answers[i];
                answers[i] = answers[randIndex];
                answers[randIndex] = swap;
            }

            foreach (GameObject answer in answers)
            {
                answer.transform.SetParent(_answerField.transform);
            }
        }
    }

    internal void HandleAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct!");
        } else
        {
            Debug.Log("Incorrect!");
        }

        if (!LoadNextQuestion())
        {
            HandleQuizCompletion();
        }
    }

    public float UpdateQuestionAnswerCount(string playerProfileName, int _questions, int _correctAnswers)
    {
        int totalQuestions = PlayerPrefs.GetInt(playerProfileName + "totalQuestions");
        int totalAnsweredRight= PlayerPrefs.GetInt(playerProfileName + "totalAnsweredRight");

        totalQuestions += _questions;
        totalAnsweredRight += _correctAnswers;

        return totalAnsweredRight / totalQuestions;
    }

    private void HandleQuizCompletion()
    {
        throw new NotImplementedException();
    }

    internal void HandleIncorrectAnswer()
    {
        Debug.Log("Incorrect!");
        if (!LoadNextQuestion())
        {
            HandleQuizCompletion();
        }
    }
}
