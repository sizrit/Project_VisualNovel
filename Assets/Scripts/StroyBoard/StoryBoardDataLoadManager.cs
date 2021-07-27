using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonStoryBoardData
{
    public List<StoryBoard> storyBoardList =new List<StoryBoard>();
}

public class StoryBoardDataLoadManager
{
    #region Singleton

    private static StoryBoardDataLoadManager _instance;

    public static StoryBoardDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            _instance= new StoryBoardDataLoadManager();
        }

        return _instance;
    }

    #endregion
    
    private readonly Dictionary<string,StoryBoard> _storyBoardList = new Dictionary<string, StoryBoard>();

    public void OnEnable()
    {
        LoadData();
    }
    
    private void LoadData()
    {
        string loadPath = "JsonData/StoryBoard/JsonStoryBoardData";
        List<StoryBoard> tempList = LoadJsonFiles<JsonStoryBoardData>(loadPath).storyBoardList;
        
        foreach (var storyBoard in tempList)
        {
            _storyBoardList.Add(storyBoard.storyBoardId,storyBoard);
        }
    }


    public StoryBoard GetStoryBoard(string storyBoardId)
    {
        return _storyBoardList[storyBoardId];
    }

    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }
}
