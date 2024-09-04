using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] List<string> _profiles;
    [SerializeField] ProfileObject _profileObject;

    private const string HISTORY_QUIZ_BEST_SCORE = "{0}HistoryQuizBestScore";
    private const string MOVE_QUIZ_BEST_SCORE = "{0}MoveQuizBestScore";
    private const string TRANSLATION_QUIZ_BEST_SCORE = "{0}TranslationQuizBestScore";

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
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        // Fetch profile names. Add default profile if none exist.
        _profiles = GetProfiles();
        
        if (_profiles.Count < 1)
        {
            AddNewProfile("Default");
        }


        if (PlayerPrefs.HasKey("CurrentProfile"))
        {
            _profileObject.curProfile = PlayerPrefs.GetString("CurrentProfile");
        }
        else
        {
            _profileObject.curProfile = _profiles[0];
        }
    }

    public List<string> GetProfiles()
    {
        if (PlayerPrefs.HasKey("Profiles"))
        {
            string curProfiles = PlayerPrefs.GetString("Profiles");
            String[] profiles = curProfiles.Split(',').Distinct().ToArray();
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
        if (!_profiles.Contains(profile))
        {
            _profiles.Add(profile);
            OutputProfileList();

            SetProfileInitialRecords(profile);
        }
    }

    private void SetProfileInitialRecords(string name)
    {
        Debug.Log($"Setting initial records for {name}");
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

    public float GetTranslationQuizBest(string profile)
    {
        string key = String.Format(TRANSLATION_QUIZ_BEST_SCORE, profile);
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            PlayerPrefs.SetFloat(key, 0f);
            return 0f;
        }
    }

    public float GetHistoryQuizBest(string profile)
    {
        string key = String.Format(HISTORY_QUIZ_BEST_SCORE, profile);
        if (PlayerPrefs.HasKey(key))
        {
            Debug.Log("Has Key");
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            PlayerPrefs.SetFloat(key, 0f);
            return 0f;
        }
    }

    public float GetMoveQuizBest(string profile)
    {
        string key = String.Format(MOVE_QUIZ_BEST_SCORE, profile);
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            PlayerPrefs.SetFloat(key, 0f);
            return 0f;
        }
    }

    public void SetHistoryQuizScore(string profile, float score)
    {
        float oldScore = GetHistoryQuizBest(profile);
        if (score > oldScore)
        {
            PlayerPrefs.SetFloat(String.Format(HISTORY_QUIZ_BEST_SCORE, profile), score);
        }
    }

    public void SetMoveQuizScore(string profile, float score)
    {
        float oldScore = GetMoveQuizBest(profile);
        if (score > oldScore)
        {
            PlayerPrefs.SetFloat(String.Format(MOVE_QUIZ_BEST_SCORE, profile), score);
        }
    }

    public void SetTranslationQuizScore(string profile, float score)
    {
        float oldScore = GetTranslationQuizBest(profile);
        if (score > oldScore)
        {
            PlayerPrefs.SetFloat(String.Format(TRANSLATION_QUIZ_BEST_SCORE, profile), score);
        }
    }

    internal void DeleteProfile(Profile profile)
    {
        string name = profile.GetName();
        if (_profiles.Contains(name))
        {
            _profiles.Remove(name);
            OutputProfileList();
            Destroy(profile.gameObject);
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
        PlayerPrefs.SetString("CurrentProfile", name);
        _profileObject.curProfile = name;
    }

    internal string GetCurrentProfile()
    {
        return _profileObject.curProfile;
    }
}
