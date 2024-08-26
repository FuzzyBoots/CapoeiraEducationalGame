using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Profile: MonoBehaviour
{
    [SerializeField] TMP_Text _profileName;
    [SerializeField] TMP_InputField _newName;
    
    public void SetNewProfile()
    {
        Debug.Log("Setting new name?");
        _profileName.gameObject.SetActive(false);
        _newName.gameObject.SetActive(true);
        _newName.Select();
    }

    public void SetName(string name)
    {
        Debug.Log($"Setting name to {name}");
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
        ProfileManager.SetCurrentProfile(GetName());
    }
}
