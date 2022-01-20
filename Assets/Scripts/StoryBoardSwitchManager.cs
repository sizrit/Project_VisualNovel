using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StoryBoardSwitchManager 
{
    #region Singleton

    private static StoryBoardSwitchManager _instance;

    public static StoryBoardSwitchManager GetInstance()
    {
        if (_instance == null)
        {
            _instance= new StoryBoardSwitchManager();
        }

        return _instance;
    }

        #endregion

    private readonly Dictionary<string,string> _storyBoardToResearchList = new Dictionary<string, string>();

    public void LoadData()
    {
        _storyBoardToResearchList.Add("S0008","Research01");
    }

    public bool CheckSwitch(string id)
    {
        return _storyBoardToResearchList.ContainsKey(id);
    }

    public void Switch(string id)
    {
        GameModeManager.GetInstance().ChangeGameMode(GameMode.StoryBoard,GameMode.Research,_storyBoardToResearchList[id]);
    }
}
