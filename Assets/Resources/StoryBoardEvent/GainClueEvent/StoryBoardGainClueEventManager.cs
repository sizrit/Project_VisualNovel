using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardGainClueEventManager : MonoBehaviour
{
    #region Singleton

    private static StoryBoardGainClueEventManager _instance;

    public static StoryBoardGainClueEventManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardGainClueEventManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("StoryBoardGettingClueEventManager");
                _instance = gameObject.AddComponent<StoryBoardGainClueEventManager>();
            }
        }
        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<StoryBoardSelectionEventManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private StoryBoardClickSystem _storyBoardClickSystem;
    private DialogueManager _dialogueManager;
    private string _currentStoryBoardId;
    private GameObject _eventPrefab;
    
    delegate void EventDelegate();
    
    EventDelegate _eventDelegate =delegate {  };

    private void CheckStart()
    {
        if (_dialogueManager.CheckIsAnimationEnd())
        {
            _storyBoardClickSystem.DisableStoryBoardCheckClick();
            _storyBoardClickSystem.SubscribeCheckClick(CheckClickToStart);
        }
    }
    
    private void LoadPrefab()
    {
        string loadPath = "StoryBoardEvent/GainClueEvent/Prefabs/GainCluePrefab";
        _eventPrefab = Resources.Load<GameObject>(loadPath);
    }

    private void SetPrefabs()
    {
        ClueManager clueManager = ClueManager.GetInstance();

        Clue clue = clueManager.GetClueByStoryBoardId(_currentStoryBoardId);

        clueManager.GainClue(clue);
        
        string loadPath = "StoryBoardEvent/GainClueEvent/Prefabs/"+clue;
        
        LanguageType type = LanguageManager.GetInstance().GetLanguageType();
        switch (type)
        {
            case LanguageType.English:
                loadPath += "_En";
                break;
            case LanguageType.Korean:
                loadPath += "_Ko";
                break;
        }
        
        GameObject obj = Instantiate(Resources.Load<GameObject>(loadPath), this.transform);
        obj.name = clue.ToString();

        /*
        GameObject obj = Instantiate(_eventPrefab, this.transform);

        string loadPath = "";
        Sprite sprite = Resources.Load<Sprite>(loadPath);

        obj.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        */
        
        _storyBoardClickSystem.SubscribeCheckClick(CheckClickToEnd);
            
        _eventDelegate = delegate {  };
    }
    

    public void SetGettingClueEvent(string storyBoardIdValue)
    {
        _storyBoardClickSystem = StoryBoardClickSystem.GetInstance();
        _currentStoryBoardId = storyBoardIdValue;
        _dialogueManager = DialogueManager.GetInstance();
        _eventDelegate += CheckStart;
    }

    private void CheckClickToStart(RaycastHit2D hit)
    {
        if (hit.transform.CompareTag("StoryBoard"))
        {
            _storyBoardClickSystem.UnsubscribeCheckClick(CheckClickToStart);
            SetPrefabs();
        }
    }
    
    private void CheckClickToEnd( RaycastHit2D hit)
    {
        if (hit.transform == this.transform.GetChild(0))
        {
            EndEvent();
        }
    }

    private void EndEvent()
    {
        _storyBoardClickSystem.UnsubscribeCheckClick(CheckClickToEnd);
        _storyBoardClickSystem.EnableStoryBoardCheckClick();
        
        Destroy(this.transform.GetChild(0).gameObject);
        _eventDelegate = delegate {  };
    }

    public void Update()
    {
        _eventDelegate();
    }
}
