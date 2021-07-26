using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardEventManager : MonoBehaviour
{
    #region SingleTon

    private static StoryBoardEventManager _instance;

    public static StoryBoardEventManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardEventManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("StoryBoardEventManager");
                _instance = gameObject.AddComponent<StoryBoardEventManager>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<StoryBoardEventManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private StoryBoardSelectionEventDataLoadManager _selectionEventDataLoadManager;
    
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
        SelectionInfo selectionInfo = _selectionEventDataLoadManager
            .GetStoryBoardSelectionEventData(_currentStoryBoardId);
        List<string> idList = selectionInfo.nextStoryIdList;
        List<string> textList = selectionInfo.textList;
        this.transform.GetChild(0).GetComponent<StoryBoardSelectionEventManager>().SetSelectionEvent(idList,textList);
    }

    private void GettingClueEvent()
    {
        
    }

    private void OnEnable()
    {
        _selectionEventDataLoadManager = StoryBoardSelectionEventDataLoadManager.GetInstance();
        MakeEventList();
    }
}
