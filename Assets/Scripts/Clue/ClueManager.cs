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
    
    private readonly Dictionary<string,Clue> _allClueList = new Dictionary<string, Clue>();
    
    private readonly List<string> _currentClueList = new List<string>();
    private readonly Dictionary<string,Clue> _gainClueStoryBoardEvent = new Dictionary<string, Clue>();

    public void GainClue(string idValue)
    {
        if (!_currentClueList.Contains(idValue))
        {
            _currentClueList.Add(idValue);
        }
    }

    public List<string> GetCurrentClueList()
    {
        return _currentClueList;
    }

    public Clue GetClueData(string idValue)
    {
        return _allClueList[idValue];
    }

    public Clue GetClueByStoryBoardId(string storyBoardId)
    {
        return _gainClueStoryBoardEvent[storyBoardId];
    }

    public List<string> GetGainClueEventStoryBoardIdList()
    {
        return _gainClueStoryBoardEvent.Keys.ToList();
    }

    public void OnEnable()
    {
        List<Clue> allClueList = ClueDataLoadManager.GetInstance().GetAllClueList().OrderBy(t=>t.id).ToList();
        foreach (var clue in allClueList)
        {
            _allClueList.Add(clue.id,clue);
        }

        foreach (var clue in _allClueList)
        {
            if (clue.Value.storyBoardId != "")
            {
                _gainClueStoryBoardEvent.Add(clue.Value.storyBoardId,clue.Value);
            }
        }
    }
}
