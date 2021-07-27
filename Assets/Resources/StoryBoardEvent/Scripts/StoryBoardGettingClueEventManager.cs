using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardGettingClueEventManager
{
    #region SingleTon

    private static StoryBoardGettingClueEventManager _instance;

    public static StoryBoardGettingClueEventManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new StoryBoardGettingClueEventManager();
        }

        return _instance;
    }

    #endregion
    
    private readonly Dictionary<string,string> _clueEventList = new Dictionary<string, string>();

    private void MakeClueEvent()
    {
        _clueEventList.Add("S0003","Clue01");
    }

    public List<string> GetClueEventIdList()
    {
        return new List<string>(_clueEventList.Keys);
    }

    public void SetGettingClueEvent(string storyBoardIdValue)
    {
        Debug.Log("Get "+_clueEventList[storyBoardIdValue] );
    }

    public void OnEnable()
    {
        MakeClueEvent();
    }
}
