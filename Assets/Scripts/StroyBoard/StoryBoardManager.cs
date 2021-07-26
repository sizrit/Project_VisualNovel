using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardManager : MonoBehaviour
{
    #region Singleton

    private static StoryBoardManager _instance;

    public static StoryBoardManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardManager>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("StoryBoardManager");
                _instance = gameObject.AddComponent<StoryBoardManager>();
            }
        }
        return _instance;
    }

    private void Awake()
    {
        var obj = GameObject.FindObjectsOfType(typeof(StoryBoardManager));
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    #endregion
    
    private StoryBoard _currentStoryBoard;
    
    private DialogueManager _dialogueManager;
    private BgLoadManager _bgLoadManager;
    private ImageLoadManager _imageLoadManager;
    private StoryBoardEventManager _storyBoardEventManager;
    
    private bool _isClickOn = true;

    public void DisableClick()
    {
        _isClickOn = false;
    }

    public void EnableClick()
    {
        _isClickOn = true;
    }

    private void OnEnable()
    {
        _dialogueManager = DialogueManager.GetInstance();
        _bgLoadManager = BgLoadManager.GetInstance();
        _imageLoadManager = ImageLoadManager.GetInstance();
        _storyBoardEventManager = StoryBoardEventManager.GetInstance();
        
        _currentStoryBoard = StoryBoardLoadManager.GetInstance().GetStoryBoard("S0001");
    }

    private void SetNextStoryBoard()
    {
        string nextStoryBoardId = _currentStoryBoard.nextStoryBoardId;
        if (nextStoryBoardId == "End")
        {
            
        }
        _currentStoryBoard = StoryBoardLoadManager.GetInstance().GetStoryBoard(nextStoryBoardId);
    }
    
    public void SetNextStoryBoard(string storyBoardIdValue)
    {
        _currentStoryBoard =StoryBoardLoadManager.GetInstance().GetStoryBoard(storyBoardIdValue);
    }


    public void StoryBoardClick()
    {
        if (_isClickOn)
        {
            SetStoryBoard();
        }
    }

    public void SetStoryBoard()
    {
        if (_dialogueManager.CheckIsAnimationEnd())
        {
            _bgLoadManager.SetBg(_currentStoryBoard.bgId);
            _imageLoadManager.SetImage(_currentStoryBoard.imageId);
            _storyBoardEventManager.CheckEvent(_currentStoryBoard.storyBoardId);
            _dialogueManager.SetDialogue(_currentStoryBoard.storyBoardId);
            SetNextStoryBoard();
        }
        else
        {
            _dialogueManager.EndAnimationForced();
        }
    }
}
