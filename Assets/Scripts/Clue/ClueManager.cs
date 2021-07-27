using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager
{
    #region Singleton

    private static ClueManager _instance;

    public static ClueManager GetInstance()
    {
        if (_instance == null)
        {
            _instance=new ClueManager();
        }
        return _instance;
    }

    #endregion
    
    private List<Clue> _currentClueList = new List<Clue>();
    private readonly List<Clue> _allClueList = new List<Clue>();

    public void GetClue(int indexValue)
    {
        if (!_currentClueList.Contains(_allClueList[indexValue]))
        {
            _currentClueList.Add(_allClueList[indexValue]);
        }
    }

    public List<Clue> GetCurrentClueList()
    {
        return _currentClueList;
    }

    public Clue GetClueData(int indexValue)
    {
        return _allClueList[indexValue];
    }

    public void OnEnable()
    {
        _currentClueList = ClueDataLoadManager.GetInstance().GetAllClueList();
    }
}
