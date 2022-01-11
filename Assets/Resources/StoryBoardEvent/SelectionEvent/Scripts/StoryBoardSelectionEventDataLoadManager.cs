using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SelectionInfo
{
    public string storyBoardId;
    public List<string> nextStoryIdList;
    public List<string> textList;
}

[Serializable]
public class JsonStoryBoardSelectionEventData
{
    public List<SelectionInfo> JsonStoryBoardSelectionEventDataList =new List<SelectionInfo>();
}

public class StoryBoardSelectionEventDataLoadManager
{
    #region SingleTon

    private static StoryBoardSelectionEventDataLoadManager _instance;

    public static StoryBoardSelectionEventDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new StoryBoardSelectionEventDataLoadManager();
        }
        return _instance;
    }

    #endregion

    private readonly Dictionary<string,SelectionInfo> _storyBoardSelectionInfoList = new Dictionary<string, SelectionInfo>();
    
    public void LoadData()
    {
        string loadPath = "JsonData/StoryBoardEvent/JsonStoryBoardSelectionEventInfoData";
        List<SelectionInfo> infoList = LoadJsonFiles<JsonStoryBoardSelectionEventData>(loadPath)
            .JsonStoryBoardSelectionEventDataList;
        foreach (var info in infoList)
        {
            _storyBoardSelectionInfoList.Add(info.storyBoardId,info);
        }
    }
    
    public List<string> GetSelectionEventId()
    {
        return new List<string>(_storyBoardSelectionInfoList.Keys);
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
}
