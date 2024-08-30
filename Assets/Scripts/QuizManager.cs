using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [SerializeField] List<QuizQuestion> _questions;

    IEnumerator<QuizQuestion> _questionIter;

    [SerializeField] TMP_Text _questionField;

    [SerializeField] GameObject _answerField;

    [SerializeField] GameObject _answerPrefab;

    [SerializeField] TMP_Text _winText, _loseText;

    [SerializeField] ScoreHolder _scoreHolder;
    
    [SerializeField] private int _questionCount = 5;

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
        _scoreHolder.totalQuestions = 0;
        _scoreHolder.correctAnswers = 0;
        _scoreHolder.currentGame = SceneManager.GetActiveScene();

        UtilityFunctions.ShuffleList<QuizQuestion>(_questions, _questionCount);
        _questionIter = _questions.Take(this._questionCount).GetEnumerator();

        LoadNextQuestion();
    }

    private void LoadNextQuestion()
    {
        bool hasNext = _questionIter.MoveNext();
        
        if (hasNext)
        {
            DisplayQuestion(_questionIter.Current);
            _scoreHolder.totalQuestions++;
        } else
        {
            HandleQuizCompletion();
        }
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
            UtilityFunctions.ShuffleList(answers);

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
            ShowWinText();

            // Increment score
            _scoreHolder.correctAnswers++;
        } else
        {
            ShowLoseText();
        }
    }

    void ShowWinText()
    {
        _winText.gameObject.SetActive(true);
        Sequence winSequence = DOTween.Sequence();
        winSequence.Append(_winText.transform.DOScale(1, 1f));
        winSequence.AppendInterval(0.5f);
        winSequence.Append(_winText.transform.DOScale(0, 1f));
        winSequence.AppendCallback(() => { _winText.gameObject.SetActive(false); LoadNextQuestion(); });
    }

    void ShowLoseText()
    {
        _loseText.gameObject.SetActive(true);
        Sequence loseSequence = DOTween.Sequence();
        loseSequence.Append(_loseText.transform.DOScale(1, 1f));
        loseSequence.AppendInterval(0.5f);
        loseSequence.Append(_loseText.transform.DOScale(0, 1f));
        loseSequence.AppendCallback(() => { _winText.gameObject.SetActive(false); LoadNextQuestion(); });
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
        string currentProfile = ProfileManager.Instance.GetCurrentProfile();
        float historicBest = ProfileManager.Instance.GetHistoryQuizBest(currentProfile);
        ProfileManager.Instance.SetHistoryQuizScore(currentProfile, _scoreHolder.correctAnswers * 1f / _scoreHolder.totalQuestions);
        _scoreHolder.historicHighScore = historicBest;
        // Display Results screen
        SceneManager.LoadScene("Results Screen");
        // That screen should provide a method to go back to the main screen (or the game choice screen?)
    }
}
