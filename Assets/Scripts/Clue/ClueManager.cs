using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClueManager
{
    #region Singleton

    private static ClueManager _instance;

    public static ClueManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ClueManager();
        }
        return _instance;
    }

    #endregion
    
    private readonly Dictionary<string,Clue> _currentClueList = new Dictionary<string,Clue>();
    private Dictionary<string,Clue> _allClueList = new Dictionary<string, Clue>();

    public void GetClue(string idValue)
    {
        if (!_currentClueList.ContainsKey(idValue))
        {
            _currentClueList.Add(idValue,_allClueList[idValue]);
        }
    }

    public Dictionary<string, Clue> GetCurrentClueList()
    {
        return _currentClueList;
    }

    public Clue GetClueData(string idValue)
    {
        return _allClueList[idValue];
    }

    public void OnEnable()
    {
        List<Clue> allClueList = ClueDataLoadManager.GetInstance().GetAllClueList().OrderBy(t=>t.id).ToList();
        foreach (var clue in allClueList)
        {
            _allClueList.Add(clue.id,clue);
        }
    }
}
