using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardGettingClueEventManager : MonoBehaviour
{
    #region SingleTon

    private static StoryBoardGettingClueEventManager _instance;

    public static StoryBoardGettingClueEventManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardGettingClueEventManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("StoryBoardGettingClueEventManager");
                _instance = gameObject.AddComponent<StoryBoardGettingClueEventManager>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<StoryBoardGettingClueEventManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
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

    private void OnEnable()
    {
        MakeClueEvent();
    }
}
