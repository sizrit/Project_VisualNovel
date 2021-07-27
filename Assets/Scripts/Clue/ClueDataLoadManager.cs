using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Clue
{
    public int index;
    public string name;
    public string info;
    public string etc;
}

[Serializable]
public class JsonClueData
{
    public List<List<string>> JsonClueDataList = new List<List<string>>();
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
    
    private readonly List<Clue> _allClueList = new List<Clue>();

    private bool _isLoadDone = false;
    
    public List<Clue> GetAllClueList()
    {
        if (!_isLoadDone)
        {
            LoadData();
        }
        return _allClueList;
    }
    
    private void LoadData()
    {
        string loadPath = "JsonData/ClueData";
        JsonClueData jsonData = LoadJsonFiles<JsonClueData>(loadPath);

        for (int i = 0; i < jsonData.JsonClueDataList.Count; i++)
        {
            List<string> tempClue = jsonData.JsonClueDataList[i];
            Clue newClue = new Clue
            {
                index = i, name = tempClue[0], info = tempClue[1],
                etc = tempClue[2]
            };
            _allClueList.Add(newClue);
        }

        _isLoadDone = true;
    }

    private T LoadJsonFiles<T>(string loadPath)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(loadPath);
        return JsonUtility.FromJson<T>(jsonData.ToString());
    }
}
