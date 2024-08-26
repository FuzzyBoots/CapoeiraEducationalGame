using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] List<string> _profiles;
    [SerializeField] ProfileObject _profileObject;

    private const string HISTORY_QUIZ_BEST_SCORE = "{}HistoryQuizBestScore";
    private const string MOVE_QUIZ_BEST_SCORE = "{}MoveQuizBestScore";
    private const string TRANSLATION_QUIZ_BEST_SCORE = "{}TranslationQuizBestScore";

    public static ProfileManager Instance
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

    public void Start()
    {
        // Fetch profile names. Add default profile if none exist.
        _profiles = GetProfiles();
        Debug.Log(_profiles.ToCommaSeparatedString());
        if (_profiles.Count < 1)
        {
            AddNewProfile("Default");
        }
    }

    public List<string> GetProfiles()
    {
        if (PlayerPrefs.HasKey("Profiles"))
        {
            string curProfiles = PlayerPrefs.GetString("Profiles");
            String[] profiles = curProfiles.Split(',');
            return new List<string>(profiles);
        }
        else
        {
            PlayerPrefs.SetString("Profiles", "");
            return new List<string>();
        }
    }

    public void AddNewProfile(string profile)
    {
        _profiles.Add(profile);
        OutputProfileList();

        SetProfileInitialRecords(profile);
        //PlayerPrefs.SetInt($"{profile}RhythmGamesPlayed", 0);
        //PlayerPrefs.SetInt($"{profile}PuzzleGamesPlayed", 0);
        //PlayerPrefs.SetInt($"{profile}HistoryQuizGamesPlayed", 0);
        //PlayerPrefs.SetInt($"{profile}MoveIdentityGamesPlayed", 0);
    }

    private void SetProfileInitialRecords(string name)
    {
        PlayerPrefs.SetFloat(String.Format(HISTORY_QUIZ_BEST_SCORE, name), 0f);
        PlayerPrefs.SetFloat(String.Format(MOVE_QUIZ_BEST_SCORE, name), 0f);
        PlayerPrefs.SetFloat(String.Format(TRANSLATION_QUIZ_BEST_SCORE, name), 0f);
    }

    private void DeleteProfileRegistryEntries(string name)
    {
        PlayerPrefs.DeleteKey(String.Format(HISTORY_QUIZ_BEST_SCORE, name));
        PlayerPrefs.DeleteKey(String.Format(MOVE_QUIZ_BEST_SCORE, name));
        PlayerPrefs.DeleteKey(String.Format(TRANSLATION_QUIZ_BEST_SCORE, name));
    }

    public void SetHistoryQuizScore(string profile, float score)
    {
        float oldScore = PlayerPrefs.GetFloat(String.Format(HISTORY_QUIZ_BEST_SCORE, profile));
        if (score > oldScore)
        {
            PlayerPrefs.SetFloat(String.Format(HISTORY_QUIZ_BEST_SCORE, name), score);
        }
    }

    public void SetMoveQuizScore(string profile, float score)
    {
        float oldScore = GetMoveQuizBest(profile);
        if (score > oldScore)
        {
            PlayerPrefs.SetFloat(String.Format(MOVE_QUIZ_BEST_SCORE, name), score);
        }
    }

    private static float GetMoveQuizBest(string profile)
    {
        return PlayerPrefs.GetFloat(String.Format(MOVE_QUIZ_BEST_SCORE, profile));
    }

    public void SetTranslationQuizScore(string profile, float score)
    {
        float oldScore = PlayerPrefs.GetFloat(String.Format(TRANSLATION_QUIZ_BEST_SCORE, profile));
        if (score > oldScore)
        {
            PlayerPrefs.SetFloat(String.Format(TRANSLATION_QUIZ_BEST_SCORE, name), score);
        }
    }

    public float GetHistoryQuizBest()
    {
        return PlayerPrefs.GetFloat(String.Format(TRANSLATION_QUIZ_BEST_SCORE, profile));
    }

    internal void DeleteProfile(Profile profile)
    {
        string name = profile.GetName();
        if (_profiles.Contains(name))
        {
            _profiles.Remove(name);
            OutputProfileList();
            DeleteProfileRegistryEntries(name);
        }
        else
        {
            Debug.LogWarning($"Attempted to delete {name} but it's not listed.");
        }
    }

    private void OutputProfileList()
    {
        PlayerPrefs.SetString($"Profiles", string.Join(",", _profiles));
    }

    internal void SetCurrentProfile(string name)
    {
        _profileObject.curProfile = name;
    }

    internal string GetCurrentProfile()
    {
        return _profileObject.curProfile;
    }
}
