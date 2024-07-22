using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] List<string> _profiles;

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
            PlayerPrefs.SetString("Profiles", "");
            return new List<string>();
        }
    }

    public void AddNewProfile(string profile)
    {
        _profiles.Add(profile);
        PlayerPrefs.SetString($"Profiles", string.Join(",", _profiles));

        PlayerPrefs.SetInt($"{profile}RhythmGamesPlayed", 0);
        PlayerPrefs.SetInt($"{profile}PuzzleGamesPlayed", 0);
        PlayerPrefs.SetInt($"{profile}HistoryQuizGamesPlayed", 0);
        PlayerPrefs.SetInt($"{profile}MoveIdentityGamesPlayed", 0);
    }
}
