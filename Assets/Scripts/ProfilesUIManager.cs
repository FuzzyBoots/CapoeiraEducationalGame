using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ProfilesUIManager : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup _profileBox;
    [SerializeField] Profile _profilePrefab;

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
        List<string> profiles = ProfileManager.Instance.GetProfiles();
        Debug.Log(profiles);
        foreach (string profile in profiles)
        {
            AddProfileUIObject(profile);
        }
    }

    private void AddProfileUIObject(string profile, bool isNew = false)
    {
        Profile profileObj = Instantiate(_profilePrefab, _profileBox.transform);
        
        if (isNew)
        {
            Debug.Log("New Profile");
            profileObj.SetNewProfile();
        } else
        {
            profileObj.SetName(profile);
        }
    }

    public void AddProfile()
    {
        // We need to add the new profile object, and then lock things in so that
        // The player has to change the name.
        AddProfileUIObject("", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
