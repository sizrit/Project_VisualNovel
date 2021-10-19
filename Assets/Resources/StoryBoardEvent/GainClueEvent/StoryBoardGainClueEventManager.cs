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
    private StoryBoardManager _storyBoardManager;
    private string _currentStoryBoardId;
    private string _nextStoryBoardId;

    private void SetPrefabs()
    {
        ClueManager clueManager = ClueManager.GetInstance();

        ClueEventData clueEventData = clueManager.GetClueEventByStoryBoardId(_currentStoryBoardId);
        Clue clue = clueEventData.clue;
        _nextStoryBoardId = clueEventData.nextStoryBoardId;
        
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

        _storyBoardClickSystem.SubscribeCheckClick(CheckClick);
    }
    
    public void SetGainClueEvent(string storyBoardIdValue)
    {
        _storyBoardClickSystem = StoryBoardClickSystem.GetInstance();
        _storyBoardManager = StoryBoardManager.GetInstance();
        _currentStoryBoardId = storyBoardIdValue;
        _storyBoardClickSystem.DisableStoryBoardCheckClick();

        SetPrefabs();
    }

    private void CheckClick( RaycastHit2D hit)
    {
        if (hit.transform == this.transform.GetChild(0))
        {
            Destroy(this.transform.GetChild(0).gameObject);
            _storyBoardClickSystem.UnsubscribeCheckClick(CheckClick);
            _storyBoardClickSystem.SubscribeCheckClick(LastCheckClick);
        }
    }

    private void LastCheckClick(RaycastHit2D hit)
    {
        GameObject dialogueClickZone = GameObject.Find("DialogueClickZone");
        if (dialogueClickZone != null)
        {
            if (hit.transform == dialogueClickZone.transform)
            {
                EndEvent();
            }
        }
    }

    private void EndEvent()
    {
        _storyBoardClickSystem.UnsubscribeCheckClick(LastCheckClick);
        _storyBoardClickSystem.EnableStoryBoardCheckClick();
        _storyBoardManager.SetNextStoryBoard(_nextStoryBoardId);
        _storyBoardManager.SetStoryBoard();
    }
    
}
