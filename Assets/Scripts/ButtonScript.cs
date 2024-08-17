using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] static AudioManager _audioManager;

    private AudioManager audioManager { 
        get {
            if (_audioManager == null)
            {
                _audioManager = GameObject.FindObjectOfType<AudioManager>();
                if (_audioManager == null ) {
                    GameObject obj = AudioManager.createGameObject();
                    _audioManager = obj.GetComponent<AudioManager>();
                }
            }
            return _audioManager; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClickSound()
    {
        audioManager.PlayClip(_clickSound);
    }
}
