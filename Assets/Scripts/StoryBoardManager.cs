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
            GameObject gameObject = new GameObject("StoryBoardManager");
            _instance = gameObject.AddComponent<StoryBoardManager>();
        }
        return _instance;
    }

    private void Awake()
    {
        var obj = GameObject.FindObjectsOfType(typeof(StoryBoardManager));
        if (obj.Length != 1)
        {
            Destroy(this);
        }

        _instance = this;
    }

    #endregion
    
    private StoryBoard _storyBoard;
    private int _dialogueNum = 0;

    private void OnEnable()
    {
        
    }

    private void SetNextStoryBoardId()
    {
        
    }

    public void GetStroyBoardChapter()
    {
        
    }

    // public (Chapter, int) GetStoryBoardIndex()
    // {
    //     return (_storyBoard.)
    // }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
        }
    }
}
