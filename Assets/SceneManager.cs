using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance
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
            DontDestroyOnLoad(this);
        }
    }
    
    List<string> _buildScenes;
    // Start is called before the first frame update
    void Start()
    {
        //_buildScenes = new List<string>();
        //int numScenes = SceneManager.sceneCountInBuildSettings;
        //Debug.Log($"{numScenes} scenes");
        //for (int i = 0; i < numScenes; i++)
        //{
        //    string sceneName = SceneManager.GetSceneByBuildIndex(i).name;
        //    _buildScenes.Add(sceneName);
        //    Debug.Log($"Added {sceneName}");
        //}
    }

    public bool LoadScene(string name)
    {
        if (_buildScenes.Contains(name))
        {
            SceneManager.LoadScene(name);
            return true;
        } else
        {
            Debug.Log($"Could not find in {_buildScenes}");
            LoadScene(name);
            return false;
        }
    }

    static public void LoadSceneAbs(string name)
    {
        // Instance.LoadScene(name);
        SceneManager.LoadScene(name);

    }
}
