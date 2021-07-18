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

    public void SetNextStroyBoard()
    {
        
    }

    private void  SimulationStoryBoard()
    {
        string divId = "";
        DivInfo divInfo = JsonDivInfoDataLoadManager.GetInstance().GetDivInfo(Chapter.Chapter01, "");
        
        _currentStoryBoard.GetChapter();
        //_currentStoryBoard
        DialogueManager.GetInstance().SetCurrentDialogueList(divInfo.divId,Chapter.Chapter01,divInfo.startDialogueId);
    }

    private void SetNetDialogue()
    {
        
    }

    // public (Chapter, int) GetStoryBoardIndex()
    // {
    //     return (_storyBoard.)
    // }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            DialogueManager.GetInstance().SetMainDialogue(Chapter.Chapter01);
            DialogueManager.GetInstance().SetCurrentDialogueList("Main",Chapter.Chapter01,"");
            DialogueManager.GetInstance().ShowDialogue();
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            DialogueManager.GetInstance().SetCurrentDialogueList("Main",Chapter.Chapter01,"Main0003");
            DialogueManager.GetInstance().ShowDialogue();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            DialogueManager.GetInstance().SetCurrentDialogueList("KK",Chapter.Chapter01,"");
            DialogueManager.GetInstance().ShowDialogue();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DialogueManager.GetInstance().SetCurrentDialogueList("AA",Chapter.Chapter01,"");
            DialogueManager.GetInstance().ShowDialogue();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DialogueManager.GetInstance().SetCurrentDialogueList("SS",Chapter.Chapter01,"");
            DialogueManager.GetInstance().ShowDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DialogueManager.GetInstance().SetNextDialogue();
            DialogueManager.GetInstance().ShowDialogue();
        }
        
    }
}
