using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
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
    private string _correctName;

    // Start is called before the first frame update
    void Start()
    {
        // BuildAnimationGraph();

        // Pick 10 random ones.
        UtilityFunctions.ShuffleList(_moveEntries, _movementCount);

        // Get the unique movement names
        _moveNames = _moveEntries.Select(item => item.GetName()).Distinct().ToList();

        // Set up play
        _moveIter = _moveEntries.Take(_movementCount).GetEnumerator();

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
        throw new NotImplementedException();
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
    }

    void OnDisable()
    {
        _playableGraph.Destroy();
    }
}
