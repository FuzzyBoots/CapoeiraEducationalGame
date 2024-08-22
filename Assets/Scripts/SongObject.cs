using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongObject", menuName = "ScriptableObjects/SongObject")]
public class SongObject : ScriptableObject
{
    public string _songTitle;
    public SongLine[] _songLines;
}
