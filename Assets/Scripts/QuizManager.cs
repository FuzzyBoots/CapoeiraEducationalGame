using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuestionEntry
    {
        [Multiline] public string _question;
        // First entry will be the correct one. They'll get shuffled on display
        public string[] _answers;
    }

    [SerializeField] private QuestionEntry[] _questions;

    List<QuestionEntry> _curQuestions;
    IEnumerator _questionIter;

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


    // Start is called before the first frame update
    void Start()
    {
        // Start up quiz
        LoadQuizQuestions();
    }

    private void LoadQuizQuestions()
    {
        _curQuestions = new List<QuestionEntry>(_questions);
        _questionIter = _curQuestions.GetEnumerator();

        LoadNextQuestion();
    }

    private void DisplayQuestion(QuestionEntry question)
    {
        _questionField.text = question._question;

        List<GameObject> answers = new List<GameObject> ();

        foreach (string answer in question._answers)
        {
            GameObject answerObject = Instantiate(_answerPrefab);
            answers.Add (answerObject);
        }
    }

    internal void HandleCorrectAnswer()
    {
        Debug.Log("Correct!");
        if (!LoadNextQuestion())
        {
            HandleQuizCompletion();
        }
    }

    private bool LoadNextQuestion()
    {
        bool hasNext = _questionIter.MoveNext();

        return hasNext;
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
