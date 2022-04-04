using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Clue
{
    Null,
    Clue01,
    Clue02
}

public struct ClueEventData
{
    public string startStoryBoardId;
    public string nextStoryBoardId;
    public Clue clue;
    
    public ClueEventData(string startStoryBoardId, string nextStoryBoardId, Clue clue)
    {
        this.startStoryBoardId = startStoryBoardId;
        this.nextStoryBoardId = nextStoryBoardId;
        this.clue = clue;
    }
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

    private List<Clue> _allClueList= Enum.GetValues(typeof(Clue)).Cast<Clue>().ToList();
    private readonly List<Clue> _currentClueList = new List<Clue>();
    private readonly Dictionary<string,ClueEventData> _gainClueStoryBoardEvent = new Dictionary<string, ClueEventData>();

    public void GainClue(Clue clue)
    {
        if (!_currentClueList.Contains(clue))
        {
            _currentClueList.Add(clue);
        }
    }

    public IEnumerable<Clue> GetCurrentClueList()
    {
        return _currentClueList;
    }

    public ClueEventData GetClueEventByStoryBoardId(string storyBoardId)
    {
        return _gainClueStoryBoardEvent[storyBoardId];
    }

    public IEnumerable<string> GetGainClueEventStoryBoardIdList()
    {
        return _gainClueStoryBoardEvent.Keys.ToList();
    }

    public void MakeClueList()
    {
        _gainClueStoryBoardEvent.Add("S0005",new ClueEventData("S0005","S0006",Clue.Clue01));
    }

    public static Clue ConvertToClue(string stringValue)
    {
        List<Clue> clueList = Enum.GetValues(typeof(Clue)).Cast<Clue>().ToList();
        foreach (var clue in clueList)
        {
            if (stringValue == clue.ToString())
            {
                return clue;
            }
        }

        return Clue.Null;
    }
}
