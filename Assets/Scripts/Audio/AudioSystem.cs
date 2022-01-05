using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum AudioList
{
    UI00_Sample01,
    St01_Sample01,
}

public enum AudioPlayMode
{
    Play,
    Duplicated,
    Loop
}

public class AudioSystem : MonoBehaviour
{
    #region Singleton

    private static AudioSystem _instance;

    public static AudioSystem GetInstance()
    {
        if (_instance == null)
        {
            var obj = GameObject.FindObjectOfType<AudioSystem>();
            if (obj == null)
            {
                GameObject gameObject = new GameObject("AudioSystem");
                _instance = gameObject.AddComponent<AudioSystem>();
            }
            else
            {
                _instance = obj;
            }
        }
        return _instance;
    }
    
    #endregion

    private GameObject _audioPrefabs;
    private readonly Dictionary<AudioList, AudioClip> _audioClipList = new Dictionary<AudioList, AudioClip>();
    private readonly Dictionary<AudioList, GameObject> _currentPlayingAudioList = new Dictionary<AudioList, GameObject>();
    private readonly List<GameObject> _currentDuplicatedAudioList = new List<GameObject>();

    public void LoadAudio()
    {
        _audioPrefabs = Resources.Load<GameObject>("Audio/AudioPrefab");
        
        List<AudioList> newAudioLists = Enum.GetValues(typeof(AudioList)).Cast<AudioList>().ToList();
        foreach (var audioName in newAudioLists)
        {
            string path = "Audio/"+audioName;
            _audioClipList.Add(audioName,Resources.Load<AudioClip>(path));
        }
    }

    public void PlayAudio(AudioList audioNameValue, AudioPlayMode mode)
    {
        if (mode != AudioPlayMode.Duplicated)
        {
            bool isDuplicated = _currentPlayingAudioList.ContainsKey(audioNameValue);
            if (isDuplicated)
            {
                return;
            }
        }
        
        if (mode == AudioPlayMode.Loop)
        {
            _audioPrefabs.GetComponent<AudioSource>().loop = true;
        }
        else
        {
            _audioPrefabs.GetComponent<AudioSource>().loop = false;  
        }
        
        _audioPrefabs.GetComponent<AudioSource>().clip = _audioClipList[audioNameValue];
        _audioPrefabs.GetComponent<AudioSource>().playOnAwake = true;
        GameObject audioGameObject = Instantiate(_audioPrefabs, this.transform);
        audioGameObject.name = audioNameValue.ToString();
        if (mode != AudioPlayMode.Duplicated)
        {
            _currentPlayingAudioList.Add(audioNameValue, audioGameObject);
        }
        else
        {
            _currentDuplicatedAudioList.Add(audioGameObject);
        }
    }
    
    public void StopAllAudio()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        _currentPlayingAudioList.Clear();
    }

    public void StopAudio(AudioList audioNameValue)
    {
        bool isPlaying = _currentPlayingAudioList.ContainsKey(audioNameValue);
        if (isPlaying)
        {
            Destroy(_currentPlayingAudioList[audioNameValue]);
            _currentPlayingAudioList.Remove(audioNameValue);
        }
    }

    private void CheckAudioEnd()
    {
        List<AudioList> keyList = _currentPlayingAudioList.Keys.ToList();
        foreach (var key in keyList)
        {
            GameObject audioGameObject = _currentPlayingAudioList[key];
            if (!audioGameObject.GetComponent<AudioSource>().isPlaying)
            {
                _currentPlayingAudioList.Remove(key);
                Destroy(audioGameObject);
            }
        }

        foreach (var audioGameObject in _currentDuplicatedAudioList)
        {
            if (!audioGameObject.GetComponent<AudioSource>().isPlaying)
            {
                _currentDuplicatedAudioList.Remove(audioGameObject);
                Destroy(audioGameObject);
            }
        }
    }

    private void OnEnable()
    {
        LoadAudio();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAudio(AudioList.UI00_Sample01,AudioPlayMode.Duplicated);
        }
        CheckAudioEnd();
    }
}
