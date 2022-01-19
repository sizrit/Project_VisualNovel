using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardManager
{
    #region Singleton

    private static StoryBoardManager _instance;

    public static StoryBoardManager GetInstance()
    {
        if (_instance == null)
        {
            _instance=new StoryBoardManager();
        }
        return _instance;
    }

    #endregion
    
    private StoryBoard _currentStoryBoard;

    public void TestRun()
    {
        _currentStoryBoard = StoryBoardDataLoadManager.GetInstance().GetStoryBoard("S0000");
    }

    private void SetNextStoryBoard()
    {
        string nextStoryBoardId = _currentStoryBoard.nextStoryBoardId;
        _currentStoryBoard = StoryBoardDataLoadManager.GetInstance().GetStoryBoard(nextStoryBoardId);
    }
    
    public void SetNextStoryBoard(string storyBoardIdValue)
    {
        _currentStoryBoard =StoryBoardDataLoadManager.GetInstance().GetStoryBoard(storyBoardIdValue);
    }

    public void SetStoryBoard()
    {
        if (DialogueManager.GetInstance().CheckIsAnimationEnd())
        {
            if (StoryBoardSwitchManager.GetInstance().CheckSwitch(_currentStoryBoard.storyBoardId))
            {
                StoryBoardSwitchManager.GetInstance().Switch(_currentStoryBoard.storyBoardId);
                return;
            }
            
            StoryBoardBgLoadManager.GetInstance().SetBg(_currentStoryBoard.bgId);
            StoryBoardImageLoadManager.GetInstance().SetImage(_currentStoryBoard.imageId);
            if (StoryBoardEventManager.GetInstance().IsStoryBoardEvent(_currentStoryBoard.storyBoardId))
            {
                StoryBoardEventManager.GetInstance().StoryBoardEventOn(_currentStoryBoard.storyBoardId);
            }
            else
            {
                DialogueManager.GetInstance().SetDialogue(_currentStoryBoard.storyBoardId);
                SetNextStoryBoard();
            }
        }
        else
        {
            DialogueManager.GetInstance().EndAnimationForced();
        }
    }
}
