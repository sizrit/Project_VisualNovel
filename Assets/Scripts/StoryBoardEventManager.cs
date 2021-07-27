using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardEventManager 
{
    #region SingleTon

    private static StoryBoardEventManager _instance;

    public static StoryBoardEventManager GetInstance()
    {
        if (_instance == null)
        {
            _instance= new StoryBoardEventManager();
        }

        return _instance;
    }

    #endregion

    private StoryBoardSelectionEventDataLoadManager _selectionEventDataLoadManager;
    private StoryBoardGettingClueEventManager _clueEventManager;
    
    private string _currentStoryBoardId = "";
    
    private readonly Dictionary<string, EventDelegate> _eventList = new Dictionary<string, EventDelegate>();

    private delegate void EventDelegate();
    
    private void MakeEventList()
    {
        EventDelegate selection = SelectionEvent;
        List<string> selectionIdList = _selectionEventDataLoadManager.GetSelectionEventId();
        foreach (var selectionId in selectionIdList)
        {
            _eventList.Add(selectionId,selection);
        }

        EventDelegate gettingClue = GettingClueEvent;
        List<string> gettingClueIdList = _clueEventManager.GetClueEventIdList();
        foreach (var gettingClueId in gettingClueIdList)
        {
            _eventList.Add(gettingClueId,gettingClue);
        }
    }

    public void CheckEvent(string storyBoardIdValue)
    {
        _currentStoryBoardId = storyBoardIdValue;
        
        if (_eventList.ContainsKey(_currentStoryBoardId))
        {
            _eventList[_currentStoryBoardId]();
        }
    }

    private void SelectionEvent()
    {
        StoryBoardSelectionEventManager.GetInstance().SetSelectionEvent(_currentStoryBoardId);
    }

    private void GettingClueEvent()
    {
        _clueEventManager.SetGettingClueEvent(_currentStoryBoardId);
    }

    public void OnEnable()
    {
        _selectionEventDataLoadManager = StoryBoardSelectionEventDataLoadManager.GetInstance();
        _clueEventManager = StoryBoardGettingClueEventManager.GetInstance();
        ;
        MakeEventList();
    }
}
