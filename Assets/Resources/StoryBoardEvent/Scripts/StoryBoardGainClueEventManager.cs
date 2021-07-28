using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

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

    private readonly Dictionary<string,string> _clueEventList = new Dictionary<string, string>();

    private GameObject _eventPrefab;
    private DialogueManager _dialogueManager;
    private string _currentStoryBoardId;

    private ClickSystem _clickSystem;

    delegate void EventDelegate();
    
    EventDelegate _eventDelegate;

    private void Func0(){}
    
    private void MakeClueEvent()
    {
        _clueEventList.Add("S0004","Clue01");
    }

    public List<string> GetClueEventIdList()
    {
        MakeClueEvent();
        return new List<string>(_clueEventList.Keys);
    }

    private void LoadPrefab()
    {
        string loadPath = "Clue/GainClue/Prefabs/GainCluePrefab";
        _eventPrefab = Resources.Load<GameObject>(loadPath);
    }

    private void SetPrefabs()
    {
        if (_dialogueManager.CheckIsAnimationEnd())
        {
            _clickSystem.UnsubscribeStoryBoardCheckClick();
            
            string clueName = _clueEventList[_currentStoryBoardId];
        
            GameObject obj = Instantiate(_eventPrefab, this.transform);
            obj.name = clueName;
        
            string loadPath = "Clue/GainClue/Images/clueName";
            obj.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(loadPath);

            string showText = "";
            LanguageType type = LanguageManager.GetInstance().GetLanguageType();
            switch (type)
            {
                case LanguageType.English:
                    showText ="단서 '" + clueName + "' 을 획득했습니다";
                    break;
                case LanguageType.Korean:
                
                    break;
            }

            obj.transform.GetChild(2).GetComponent<Text>().text = showText;
            
            _clickSystem.SubscribeCheckClick(CheckClick);
            
            _eventDelegate = new EventDelegate(Func0);
        }
    }

    public void SetGettingClueEvent(string storyBoardIdValue)
    {
        _clickSystem = ClickSystem.GetInstance();

        _currentStoryBoardId = storyBoardIdValue;
        _dialogueManager = DialogueManager.GetInstance();
        _eventDelegate += SetPrefabs;
    }

    private void CheckClick( RaycastHit2D hit)
    {
        if (hit.transform == this.transform.GetChild(0))
        {
            EndEvent();
        }
    }

    private void EndEvent()
    {
        _clickSystem.UnsubscribeCheckClick(CheckClick);
        _clickSystem.SubscribeStoryBoardCheckClick();
        
        Destroy(this.transform.GetChild(0).gameObject);
        _eventDelegate = new EventDelegate(Func0);
    }
    
    public void OnEnable()
    {
        _eventDelegate = new EventDelegate(Func0);
        //MakeClueEvent();
        LoadPrefab();
    }

    public void Update()
    {
        _eventDelegate();
    }
}
