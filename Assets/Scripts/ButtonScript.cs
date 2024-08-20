using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] AudioClip _clickSound;

    private void Awake()
    {
        if (_clickSound == null)
        {
            Debug.LogError("No button click sound assigned", this);
        }
    }

    public void PlayClickSound()
    {
        Debug.Log("Playing?");
        AudioManager.Instance.PlayClip(_clickSound);
    }

    public void LoadScene(string sceneName)
    {
        SceneLoader.Instance.LoadScene(sceneName);
    }
}
