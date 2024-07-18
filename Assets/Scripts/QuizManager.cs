using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] List<QuestionEntry> _curQuestions;

    // Start is called before the first frame update
    void Start()
    {
        // Start up quiz
        LoadQuizQuestions();
    }

    private void LoadQuizQuestions()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
