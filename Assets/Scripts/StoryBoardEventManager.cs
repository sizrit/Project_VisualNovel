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
    private StoryBoardGainClueEventManager _clueEventManager;
    
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

        EventDelegate gainClue = GainClueEvent;
        IEnumerable<string> gainClueIdList = ClueManager.GetInstance().GetGainClueEventStoryBoardIdList();
        foreach (var gainClueId in gainClueIdList)
        {
            _eventList.Add(gainClueId,gainClue);
        }
    }

    public bool IsStoryBoardEvent(string storyBoardIdValue)
    {
        return _eventList.ContainsKey(storyBoardIdValue);
    }

    public void StoryBoardEventOn(string storyBoardIdValue)
    {
        _currentStoryBoardId = storyBoardIdValue;
        _eventList[storyBoardIdValue]();
    }

    private void SelectionEvent()
    {
        StoryBoardSelectionEventManager.GetInstance().SetSelectionEvent(_currentStoryBoardId);
    }

    private void GainClueEvent()
    {
        _clueEventManager.SetGainClueEvent(_currentStoryBoardId);
    }

    public void OnEnable()
    {
        _selectionEventDataLoadManager = StoryBoardSelectionEventDataLoadManager.GetInstance();
        _clueEventManager = StoryBoardGainClueEventManager.GetInstance();
        ;
        MakeEventList();
    }
}
