using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    
    [SerializeField] List<string> _buildScenes;
    // Start is called before the first frame update
    void Start()
    {
        // All credit to https://discussions.unity.com/t/how-can-i-get-a-list-of-all-scenes-in-the-build/157377/4
        //_buildScenes = EditorBuildSettings.scenes
        //    .Where(scene => scene.enabled)
        //    .Select(scene => scene.path)
        //    .ToList();
    }

    public void LoadScene(string name)
    {
        if (_buildScenes.Any(x => x.Equals(name) || System.IO.Path.GetFileNameWithoutExtension(x).Equals(name)))
        {
            SceneManager.LoadScene(name);
            return;
        } else
        {
            Debug.LogWarning($"Could not find {name} in {_buildScenes.ToSeparatedString(",")}, but trying anyway.");
            SceneManager.LoadScene(name);
            return;
        }
    }
}
