using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LyricObject : MonoBehaviour
{
    [SerializeField] TMP_Text _lyricLineText;
    [SerializeField] TMP_Dropdown _translations;
    private string _correctAnswer;

    public void SetLyricText(string text)
    {
        _lyricLineText.text = text;
    }

    public void SetChoices(List<string> translations)
    {
        _correctAnswer = translations[0];

        UtilityFunctions.ShuffleList(translations);
        _translations.AddOptions(translations);
    }
}
