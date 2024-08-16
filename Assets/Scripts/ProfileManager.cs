using System;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] List<string> _profiles;
    private const string HISTORY_BEST_SCORE_FORMAT_STRING = "{profile}HistoryQuizGameBestScore";
    private const string MOVE_BEST_SCORE_FORMAT_STRING = "{profile}MovementQuizGameBestScore";
    private const string PUZZLE_BEST_SCORE_FORMAT_STRING = "{profile}PuzzleGameBestScore";
    private const string RHYTHM_BEST_SCORE_FORMAT_STRING = "{profile}RhythmGameBestScore";

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
        Debug.Log(_profiles);
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
            AddNewProfile("Default");
            return new List<string> { "Default"};
        }
    }

    public void AddNewProfile(string profile)
    {
        _profiles.Add(profile);
        PlayerPrefs.SetString($"Profiles", string.Join(",", _profiles));

        //PlayerPrefs.SetInt($"{profile}RhythmGamesPlayed", 0);
        //PlayerPrefs.SetInt($"{profile}PuzzleGamesPlayed", 0);
        //PlayerPrefs.SetInt($"{profile}HistoryQuizGamesPlayed", 0);
        //PlayerPrefs.SetInt($"{profile}MoveIdentityGamesPlayed", 0);
        SetBestRhythmGameScore(profile, 0);
        SetBestPuzzleGameScore(profile, 0);
        SetBestHistoryQuizGameScore(profile, 0);
        SetBestMoveIdentityQuizGameScore(profile, 0);
    }

    public void SetBestMoveIdentityQuizGameScore(string profile, int score)
    {
        float curValue = PlayerPrefs.GetFloat(String.Format(MOVE_BEST_SCORE_FORMAT_STRING, profile), 0);
        if (score > curValue)
        {
            PlayerPrefs.SetFloat(String.Format(MOVE_BEST_SCORE_FORMAT_STRING, profile), score);
        }
    }

    public void SetBestHistoryQuizGameScore(string profile, int score)
    {
        float curValue = PlayerPrefs.GetFloat(String.Format(HISTORY_BEST_SCORE_FORMAT_STRING, profile), 0);
        if (score > curValue)
        {
            PlayerPrefs.SetFloat(String.Format(HISTORY_BEST_SCORE_FORMAT_STRING, profile), score);
        }
    }

    public void SetBestPuzzleGameScore(string profile, int score)
    {
        float curValue = PlayerPrefs.GetFloat(String.Format(PUZZLE_BEST_SCORE_FORMAT_STRING, profile), 0);
        if (score < curValue)
        {
            PlayerPrefs.SetFloat(String.Format(PUZZLE_BEST_SCORE_FORMAT_STRING, profile), score);
        }
    }

    public void SetBestRhythmGameScore(string profile, int score)
    {
        float curValue = PlayerPrefs.GetFloat(String.Format(RHYTHM_BEST_SCORE_FORMAT_STRING, profile), 0);
        if (score > curValue)
        {
            PlayerPrefs.SetFloat(String.Format(RHYTHM_BEST_SCORE_FORMAT_STRING, profile), score);
        }
    }
}
