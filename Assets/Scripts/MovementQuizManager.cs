using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MovementQuizManager : MonoBehaviour
{
    [SerializeField] List<MoveEntry> _moveEntries;
    [SerializeField] Animator _animator;
    PlayableGraph _playableGraph;
    [SerializeField] AnimatorController _controller;
    List<string> _moveNames;
    IEnumerator<MoveEntry> _moveIter;
    
    [SerializeField] int _movementCount = 10;
    [SerializeField] int _randomNameCount = 5;

    [SerializeField] ScoreHolder _scoreHolder;

    [SerializeField] TMP_Text _movesText;
    [SerializeField] TMP_InputField _moveField;
    [SerializeField] TMP_Text _winText, _loseText;

    private string _correctName;



    public static MovementQuizManager Instance
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
        Assert.IsNotNull(_scoreHolder);
        Assert.IsNotNull(_movesText);
        Assert.IsNotNull(_animator);
        Assert.IsNotNull(_controller);
        Assert.IsNotNull(_moveField);

        // BuildAnimationGraph();

        // Pick 10 random moves.
        UtilityFunctions.ShuffleList(_moveEntries, _movementCount);

        // Get the unique movement names
        _moveNames = _moveEntries.Select(item => item.GetName()).Distinct().ToList();

        // Set up play
        _moveIter = _moveEntries.Take(_movementCount).GetEnumerator();

        _scoreHolder.totalQuestions = 0;
        _scoreHolder.correctAnswers = 0;
        _scoreHolder.currentGame = SceneManager.GetActiveScene();

        LoadNextMovement();
    }

    private void BuildAnimationGraph()
    {
        // Experimental function to build our transition graph

        // Clear it off
        for (int i=_controller.layers.Length-1; i>=0; i--)
        {
            _controller.RemoveLayer(i);
        }

        _controller.AddLayer("default");

        foreach (MoveEntry moveEntry in _moveEntries)
        {
            AnimatorState motionState = _controller.AddMotion(moveEntry.GetAnimation());

            Debug.Log(moveEntry.GetName());
        }


    }

    private void LoadNextMovement()
    {
        bool hasNext = _moveIter.MoveNext();

        if (hasNext)
        {
            HandleMovement(_moveIter.Current);
            _scoreHolder.totalQuestions++;
        }
        else
        {
            HandleQuizCompletion();
        }
    }
    
    private void HandleQuizCompletion()
    {
        string currentProfile = ProfileManager.Instance.GetCurrentProfile();
        float historicBest = ProfileManager.Instance.GetMoveQuizBest(currentProfile);
        ProfileManager.Instance.SetMoveQuizScore(currentProfile, _scoreHolder.correctAnswers * 1f / _scoreHolder.totalQuestions);
        _scoreHolder.historicHighScore = historicBest;
        // Display Results screen
        SceneManager.LoadScene("Results Screen");
        // That screen should provide a method to go back to the main screen (or the game choice screen?)
    }

    public void HandleAnswer()
    {
        if (String.Compare(_moveField.text, _correctName, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
        {
            ShowWinText();

            // Increment score
            _scoreHolder.correctAnswers++;
        }
        else
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
        winSequence.AppendCallback(() => { _winText.gameObject.SetActive(false); LoadNextMovement(); });
    }

    void ShowLoseText()
    {
        _loseText.gameObject.SetActive(true);
        Sequence loseSequence = DOTween.Sequence();
        loseSequence.Append(_loseText.transform.DOScale(1, 1f));
        loseSequence.AppendInterval(0.5f);
        loseSequence.Append(_loseText.transform.DOScale(0, 1f));
        loseSequence.AppendCallback(() => { _winText.gameObject.SetActive(false); LoadNextMovement(); });
    }

    private void HandleMovement(MoveEntry moveEntry)
    {
        _correctName = moveEntry.GetName();
        AnimationPlayableUtilities.PlayClip(_animator, moveEntry.GetAnimation(), out _playableGraph);
        UtilityFunctions.ShuffleList(_moveNames, _randomNameCount);
        List<string> moveNames = _moveNames.Take(_randomNameCount).ToList();
        if (!moveNames.Contains(_correctName))
        {
            moveNames[Random.Range(0, _randomNameCount)] = _correctName;
        }
        _movesText.text = string.Join("\n", moveNames);
        _moveField.text = "";
    }

    void OnDisable()
    {
        _playableGraph.Destroy();
    }
}
