using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizQuestion", menuName = "ScriptableObjects/QuizQuestion")]
public class QuizQuestion : ScriptableObject
{
    [Multiline][SerializeField] string _question;
    // First answer is always the correct one. We'll shuffle later
    [SerializeField] List<string> _answers;

    public string Question { get { return _question; } }
    public List<string> Answers { get {  return _answers; } }
}
