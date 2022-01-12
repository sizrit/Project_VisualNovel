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
            if (obj == null)
            {
                Debug.LogError("Error! StoryBoardGainClueEventManager is disable now");
                return null;
            }
            else
            {
                _instance = obj;
            }
        }
        return _instance;
    }

    #endregion
    
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

         StoryBoardClickSystem.GetInstance().SubscribeCheckClick(CheckClick);
    }
    
    public void SetGainClueEvent(string storyBoardIdValue)
    {
        _currentStoryBoardId = storyBoardIdValue;
        StoryBoardClickSystem.GetInstance().DisableStoryBoardCheckClick();
        SetPrefabs();
    }

    private void CheckClick( RaycastHit2D hit)
    {
        if (hit.transform == this.transform.GetChild(0))
        {
            Destroy(this.transform.GetChild(0).gameObject);
            StoryBoardClickSystem.GetInstance().UnsubscribeCheckClick(CheckClick);
            StoryBoardClickSystem.GetInstance().SubscribeCheckClick(LastCheckClick);
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
        StoryBoardClickSystem.GetInstance().UnsubscribeCheckClick(LastCheckClick);
        StoryBoardClickSystem.GetInstance().EnableStoryBoardCheckClick();
        StoryBoardManager.GetInstance().SetNextStoryBoard(_nextStoryBoardId);
        StoryBoardManager.GetInstance().SetStoryBoard();
    }
    
}
