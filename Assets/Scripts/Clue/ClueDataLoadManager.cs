using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public struct Clue
{
    public string id;
    public string en;
    public string ko;
    public string info;
    public string etc;
}

[Serializable]
public class JsonClueData
{
    public List<Clue> jsonClueDataList = new List<Clue>();
}

public class ClueDataLoadManager
{
    #region Singleton

    private static ClueDataLoadManager _instance;

    public static ClueDataLoadManager GetInstance()
    {
        if (_instance == null)
        {
            _instance= new ClueDataLoadManager();
        }

        return _instance;
    }

    #endregion
    
    private List<Clue> _allClueList = new List<Clue>();

    public void OnEnable()
    {
        LoadData();
    }
    
    public List<Clue> GetAllClueList()
    {
        return _allClueList;
    }
    
    private void LoadData()
    {
        string loadPath = "JsonData/ClueData";
        JsonClueData jsonData = LoadJsonFiles<JsonClueData>(loadPath);
        _allClueList = jsonData.jsonClueDataList;
    }

    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }
}
