using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Clue
{
    Clue01,
    Clue02
}

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

    private List<Clue> _allClueList;
    private readonly List<Clue> _currentClueList = new List<Clue>();
    private readonly Dictionary<string,Clue> _gainClueStoryBoardEvent = new Dictionary<string, Clue>();
    private bool _isLoadDone = false;

    public void GainClue(Clue clue)
    {
        if (!_currentClueList.Contains(clue))
        {
            _currentClueList.Add(clue);
        }
    }

    public List<Clue> GetCurrentClueList()
    {
        return _currentClueList;
    }

    public Clue GetClueByStoryBoardId(string storyBoardId)
    {
        return _gainClueStoryBoardEvent[storyBoardId];
    }

    public List<string> GetGainClueEventStoryBoardIdList()
    {
        if (!_isLoadDone)
        {
            MakeClueList();
        }
        return _gainClueStoryBoardEvent.Keys.ToList();
    }

    public void OnEnable()
    {
        _allClueList = Enum.GetValues(typeof(Clue)).Cast<Clue>().ToList();
        MakeClueList();
    }

    private void MakeClueList()
    {
        _gainClueStoryBoardEvent.Add("S0004",Clue.Clue01);
        _isLoadDone = true;
    }
}
