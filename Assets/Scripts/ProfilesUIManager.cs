using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ProfilesUIManager : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup _profileBox;
    [SerializeField] ScrollRect _profileScrollView;
    [SerializeField] Profile _profilePrefab;

    public static ProfilesUIManager Instance
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
        if (_profileBox == null)
        {
            _profileBox = transform.GetComponentInChildren<VerticalLayoutGroup>();
            Assert.IsNotNull( _profileBox, "No container found for profiles." );
        }

        Assert.IsNotNull(_profilePrefab, "No profile prefab set");

        UpdateProfileList();
    }

    private void UpdateProfileList()
    {
        Debug.Log("Updating profile list");
        List<string> profiles = ProfileManager.Instance.GetProfiles();
        Debug.Log(profiles.ToSeparatedString(", "));

        foreach (string profile in profiles)
        {
            AddProfileUIObject(profile);
        }
    }

    private void AddProfileUIObject(string profile, bool isNew = false)
    {
        Profile profileObj = Instantiate(_profilePrefab, _profileBox.transform);
        Debug.Log($"Adding {profile} to the list. Profile Object is {profileObj}");

        if (isNew)
        {
            Debug.Log("New Profile");
            profileObj.SetNewProfile();

            _profileScrollView.DOVerticalNormalizedPos(0f, 0.1f);
        } else
        {
            Debug.Log("Just setting the name");
            profileObj.SetName(profile);
            if (profile.Equals(ProfileManager.Instance.GetCurrentProfile()))
            {
                profileObj.SetHighlight(true);
            }
        }
    }

    public void AddProfile()
    {
        AddProfileUIObject("", true);
    }

    public void DeselectCurrentProfile()
    {
        foreach (Profile profile in _profileBox.GetComponentsInChildren<Profile>())
        {
            if (profile.GetName().Equals(ProfileManager.Instance.GetCurrentProfile())) {
                profile.SetHighlight(false);
            }
        }
    }
}
