using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SelectionInfo
{
    public string eventId;
    public List<string> nextStoryIdList;
    public List<string> textList;
}

[Serializable]
public class JsonStoryBoardSelectionEventData
{
    public List<SelectionInfo> JsonStoryBoardSelectionEventDataList =new List<SelectionInfo>();
}

public class StoryBoardSelectionEventDataLoadManager : MonoBehaviour
{
    #region SingleTon

    private static StoryBoardSelectionEventDataLoadManager _instance;

    public static StoryBoardSelectionEventDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardSelectionEventDataLoadManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("StoryBoardSelectionEventDataLoadManager");
                _instance = gameObject.AddComponent<StoryBoardSelectionEventDataLoadManager>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<StoryBoardSelectionEventDataLoadManager>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private readonly Dictionary<string,SelectionInfo> _storyBoardSelectionInfoList = new Dictionary<string, SelectionInfo>();

    private void LoadData()
    {
        string loadPath = "JsonData/StoryBoardEvent/JsonStoryBoardSelectionEventInfoData";
        List<SelectionInfo> infoList = LoadJsonFiles<JsonStoryBoardSelectionEventData>(loadPath)
            .JsonStoryBoardSelectionEventDataList;
        foreach (var info in infoList)
        {
            _storyBoardSelectionInfoList.Add(info.eventId,info);
        }
    }
    
    public SelectionInfo GetStoryBoardSelectionEventData(string eventId)
    {
        return _storyBoardSelectionInfoList[eventId];
    }
    
    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }

    private void OnEnable()
    {
        LoadData();
    }
}
