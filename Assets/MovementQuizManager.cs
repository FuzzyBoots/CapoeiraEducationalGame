using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;

public class MovementQuizManager : MonoBehaviour
{
    [SerializeField] List<MoveEntry> _moveEntries;
    [SerializeField] Animator _animator;
    PlayableGraph _playableGraph;
    [SerializeField] AnimatorController _controller;

    // Start is called before the first frame update
    void Start()
    {
        // Pick 10 random ones.
        UtilityFunctions.ShuffleList(_moveEntries, 10);
        Debug.Log($"Entry count: {_moveEntries.Count}");
        foreach (MoveEntry moveEntry in _moveEntries )
        {
            _controller.AddMotion(moveEntry.GetAnimation());
            Debug.Log(moveEntry.GetName());
        }

        // Set up play
        AnimationPlayableUtilities.PlayClip(_animator, _moveEntries[0].GetAnimation(), out _playableGraph);
    }

    // Update is called once per frame
    void Update()
    {
        // _playableGraph.Destroy();
    }
}
