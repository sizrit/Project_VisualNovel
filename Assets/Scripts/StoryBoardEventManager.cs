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
        
    }

    public void CheckEvent(string storyBoardIdValue)
    {
        if (storyBoardIdValue == "S0001")
        {
            _eventId = storyBoardIdValue;
            SelectionEvent();
        }
    }

    private void SetSelectionEvent()
    {
        
    }

    private void LoadData()
    {
        
    }

    private void SelectionEvent()
    {
        SelectionInfo selectionInfo = StoryBoardSelectionEventDataLoadManager.GetInstance()
            .GetStoryBoardSelectionEventData(_eventId);
        List<string> idList = selectionInfo.nextStoryIdList;
        List<string> textList = selectionInfo.textList;
        this.transform.GetChild(0).GetComponent<StoryBoardSelectionEventManager>().SetSelectionEvent(idList,textList);
    }

    private void OnEnable()
    {
        MakeEventList();
    }
}
