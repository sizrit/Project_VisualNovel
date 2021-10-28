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
    
    private DialogueManager _dialogueManager;
    private BgLoadManager _bgLoadManager;
    private ImageLoadManager _imageLoadManager;
    private StoryBoardEventManager _storyBoardEventManager;

    public void OnEnable()
    {
        _dialogueManager = DialogueManager.GetInstance();
        _bgLoadManager = BgLoadManager.GetInstance();
        _imageLoadManager = ImageLoadManager.GetInstance();
        _storyBoardEventManager = StoryBoardEventManager.GetInstance();
        
        _currentStoryBoard = StoryBoardDataLoadManager.GetInstance().GetStoryBoard("S0001");
        
        StoryBoardClickSystem.GetInstance().SetStoryBoardCheckClick(CheckClick);
    }

    private void SetNextStoryBoard()
    {
        string nextStoryBoardId = _currentStoryBoard.nextStoryBoardId;
        if (nextStoryBoardId == "End")
        {
            
        }
        _currentStoryBoard = StoryBoardDataLoadManager.GetInstance().GetStoryBoard(nextStoryBoardId);
    }
    
    public void SetNextStoryBoard(string storyBoardIdValue)
    {
        _currentStoryBoard =StoryBoardDataLoadManager.GetInstance().GetStoryBoard(storyBoardIdValue);
    }

    public void SetStoryBoard()
    {
        if (_dialogueManager.CheckIsAnimationEnd())
        {
            _bgLoadManager.SetBg(_currentStoryBoard.bgId);
            _imageLoadManager.SetImage(_currentStoryBoard.imageId);
            if (_storyBoardEventManager.IsStoryBoardEvent(_currentStoryBoard.storyBoardId))
            {
                _storyBoardEventManager.StoryBoardEventOn(_currentStoryBoard.storyBoardId);
            }
            else
            {
                _dialogueManager.SetDialogue(_currentStoryBoard.storyBoardId);
                SetNextStoryBoard();
            }
        }
        else
        {
            _dialogueManager.EndAnimationForced();
        }
    }

    private void CheckClick(RaycastHit2D hit)
    {
        GameObject dialogueClickZone = GameObject.Find("DialogueClickZone");
        if (dialogueClickZone != null)
        {
            if (hit.transform == dialogueClickZone.transform)
            {
                SetStoryBoard();
            }
        }
    }
}
