using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    #region Singleton Class: MusicManager
    public static MusicManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    #endregion

    [SerializeField]
    private AudioSource _audioSource;

 
    private void Start()
    {
        _audioSource.mute = PlayerPrefs.GetInt("muted",0) == 1;
    }

    public void OnChangeMusic(bool muted)
    {
        _audioSource.mute = muted; 
    }
   
}
