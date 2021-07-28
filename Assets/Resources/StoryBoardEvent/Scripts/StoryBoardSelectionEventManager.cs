using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardSelectionEventManager : MonoBehaviour
{
    #region Singleton

    private static StoryBoardSelectionEventManager _instance;

    public static StoryBoardSelectionEventManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardSelectionEventManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("");
                _instance = gameObject.AddComponent<StoryBoardSelectionEventManager>();
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

    private ClickSystem _clickSystem;
    private StoryBoardSelectionEventDataLoadManager _selectionEventDataLoadManager;
    private StoryBoardManager _storyBoardManager;
    private DialogueManager _dialogueManager;

    private List<string> _storyBoardIdList = new List<string>();
    private List<string> _textList = new List<string>();

    private GameObject _selectionGameObject;

    private delegate void EventDelegate();

    private EventDelegate _eventDelegate;
    
    private void func0(){}
    
    public void Start()
    {
        _selectionEventDataLoadManager = StoryBoardSelectionEventDataLoadManager.GetInstance();
        
        _eventDelegate = new EventDelegate(func0);
        _selectionGameObject = Resources.Load<GameObject>("StoryBoardEvent/Prefabs/SelectionObject");
    }

    private void RestObject()
    {
        _eventDelegate=new EventDelegate(func0);
        _storyBoardIdList= new List<string>();
        _textList =new List<string>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    public void SetSelectionEvent(string storyBoardIdValue)
    {
        _clickSystem = ClickSystem.GetInstance();
        _clickSystem.UnsubscribeStoryBoardCheckClick();
        
        _storyBoardManager = StoryBoardManager.GetInstance();
        _dialogueManager = DialogueManager.GetInstance();

        SelectionInfo info = _selectionEventDataLoadManager.GetStoryBoardSelectionEventData(storyBoardIdValue);
        _storyBoardIdList = info.nextStoryIdList;
        _textList = info.textList;

        _eventDelegate += ShowSelectionEvent;
    }

    private void ShowSelectionEvent()
    {
        if (_dialogueManager.CheckIsAnimationEnd())
        {
            for (int i = 0; i < _storyBoardIdList.Count; i++)
            {
                GameObject obj =  Instantiate(_selectionGameObject, this.transform);
                obj.transform.localPosition = new Vector3(0, 100 * _storyBoardIdList.Count - 200 * i, 0);
                obj.transform.GetChild(0).GetComponent<Text>().text = _textList[i];
            }
            _eventDelegate=new EventDelegate(func0);
            _clickSystem.SubscribeCheckClick(CheckClick);
        }
    }

    private void CheckClick(RaycastHit2D hit)
    {
        for (int i = 0; i < _storyBoardIdList.Count; i++)
        {
            if (hit.transform == this.transform.GetChild(i))
            {
                _storyBoardManager.SetNextStoryBoard(_storyBoardIdList[i]);
                _storyBoardManager.SetStoryBoard();

                RestObject();
                
                _clickSystem.UnsubscribeCheckClick(CheckClick);
                _clickSystem.SubscribeStoryBoardCheckClick();
            }
        }
    }

    private void Update()
    {
        _eventDelegate();
    }
}
