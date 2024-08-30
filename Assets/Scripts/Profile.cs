using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile: MonoBehaviour
{
    [SerializeField] TMP_Text _profileName;
    [SerializeField] TMP_InputField _newName;
    
    public void SetNewProfile()
    {
        ScrollRect _scrollRect = transform.GetComponentInParent<ScrollRect>();
        _profileName.gameObject.SetActive(false);
        _newName.gameObject.SetActive(true);
        _newName.Select();
    }

    public void SetNewName(string name)
    {
        if (name.Trim().Length > 0 && 
            !ProfileManager.Instance.GetProfiles().Contains(name))
        {
            _profileName.gameObject.SetActive(true);
            _newName.gameObject.SetActive(false);
            _profileName.text = name;
            ProfileManager.Instance.AddNewProfile(name);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SetName(string name)
    {
        _profileName.gameObject.SetActive(true);
        _newName.gameObject.SetActive(false);
        _profileName.text = name;
    }

    public string GetName()
    {
        return _profileName.text;
    }

    public void Delete()
    {
        ProfileManager.Instance.DeleteProfile(this);
    }

    public void SetCurrent()
    {
        ProfileManager.Instance.SetCurrentProfile(GetName());
    }
}
