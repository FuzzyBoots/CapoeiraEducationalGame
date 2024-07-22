using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Profile: MonoBehaviour
{
    [SerializeField] TMP_Text _profileName;
    public void setProfileName(string profileName)
    {
        _profileName.text = profileName;
    }
}
