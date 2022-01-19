using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void LoadData()
    {
        _storyBoardList.Add("S0000",
            new StoryBoard("S0000", StoryBoardMode.Selection, BgId.Chapter01Room, "ImageSet001", "S0001"));
        _storyBoardList.Add("S0001",
            new StoryBoard("S0001", StoryBoardMode.Dialogue, BgId.Chapter01Room, "ImageSet001", "S0002"));
        _storyBoardList.Add("S0002",
            new StoryBoard("S0001", StoryBoardMode.Dialogue, BgId.Chapter01Room, "ImageSet002", "S0003"));
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
