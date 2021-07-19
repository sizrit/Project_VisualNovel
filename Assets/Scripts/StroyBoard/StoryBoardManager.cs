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

    private void OnEnable()
    {
        _currentStoryBoard = StoryBoardLoadManager.GetInstance().GetStoryBoard("S0001");
    }

    private void SetNextStoryBoard()
    {
        string nextStroyBoardId = _currentStoryBoard.GetNextStoryBoardId();
        _currentStoryBoard = StoryBoardLoadManager.GetInstance().GetStoryBoard(nextStroyBoardId);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DialogueManager.GetInstance().SetDialogue(_currentStoryBoard.GetStroyBoardId());
            SetNextStoryBoard();
        }
    }

}
