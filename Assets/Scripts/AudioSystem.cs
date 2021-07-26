using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioList
{
    Sample01
}

public class AudioSystem : MonoBehaviour
{
    #region Singleton

    private static AudioSystem _instance;

    public static AudioSystem GetInstance()
    {
        if (_instance == null)
        {
            
        }
        
        return _instance;
    }
    

    #endregion
    
    private Dictionary<AudioList, GameObject> _audioList = new Dictionary<AudioList, GameObject>();

    private void MakeAudioList()
    {
        
    }
    
    public void PlayAudio(AudioList audioNameValue)
    {
        Instantiate(_audioList[audioNameValue], this.transform);
    }

    private void OnEnable()
    {

    }
}
