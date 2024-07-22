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

    private void AddProfileUIObject(string profile)
    {
        Profile profileObj = Instantiate(_profilePrefab, _profileBox.transform);
        profileObj.setProfileName(profile);
    }

    public void AddProfile()
    {
        string[] randomNames = {"Macaco Branco", "Mestre Doutor", "Mestre Errado", "Kilroy", "Bob" };

        List<string> profiles = ProfileManager.Instance.GetProfiles();
        while (true)
        {
            string randomName = randomNames[UnityEngine.Random.Range(0, randomNames.Length)];
            if (!profiles.Contains(randomName))
            {
                ProfileManager.Instance.AddNewProfile(randomName);
                AddProfileUIObject(randomName);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
