using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonStoryBoardData
{
    public List<StoryBoard> storyBoardList =new List<StoryBoard>();
}

public class StoryBoardLoadManager
{
    private Dictionary<string,StoryBoard> _storyBoardList = new Dictionary<string, StoryBoard>();

    private bool _isLoad = false;
    
    #region Singleton

    private static StoryBoardLoadManager _instance;

    public static StoryBoardLoadManager GetInstance()
    {
        if (_instance == null)
        {
            _instance= new StoryBoardLoadManager();
        }

        return _instance;
    }

    #endregion

    private void LoadData()
    {
        string loadPath = "JsonData/StoryBoard/JsonStoryBoardData";
        List<StoryBoard> tempList = LoadJsonFiles<JsonStoryBoardData>(loadPath).storyBoardList;
        
        foreach (var storyBoard in tempList)
        {
            _storyBoardList.Add(storyBoard.storyBoardId,storyBoard);
        }

        _isLoad = true;
    }


    public StoryBoard GetStoryBoard(string storyBoardId)
    {
        if (!_isLoad)
        {
            LoadData();
        }

        return _storyBoardList[storyBoardId];
    }

    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }
}
