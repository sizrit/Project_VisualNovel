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

    public void SetStoryBoard()
    {
        if (DialogueManager.GetInstance().CheckIsAnimationEnd())
        {
            BgLoadManager.GetInstance().SetBg(_currentStoryBoard.bgId);
            ImageLoadManager.GetInstance().SetImage(_currentStoryBoard.imageId);
            StoryBoardEventManager.GetInstance().CheckEvent(_currentStoryBoard.storyBoardId);
            DialogueManager.GetInstance().SetDialogue(_currentStoryBoard.dialogueId);
            SetNextStoryBoard();
        }
        else
        {
            DialogueManager.GetInstance().EndAnimationForced();
        }
    }

    private void Update()
    {
        if (_isClickOn)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetStoryBoard();
            }
        }
    }
}
