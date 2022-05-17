using System;
using System.Collections;
using System.Collections.Generic;
using ClickSystem;
using ResearchSystem;
using StoryBoardSystem;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public enum GameMode
{
    ChapterStart,
    ChapterEnd,
    StoryBoard,
    StoryBoardR,
    Research,
    Idle
}

public class GameModeManager : MonoBehaviour
{
    #region Singleton

    private static GameModeManager _instance;

    public static GameModeManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<GameModeManager>();
            if (obj == null)
            {
                Debug.LogError("Error! GameModeManager is disable now");
                return null;
            }

            _instance = obj;
        }
        return _instance;
    }

    #endregion

    [SerializeField] private GameObject storyBoardMode;
    [SerializeField] private GameObject researchMode;
    
    [SerializeField] private GameMode currentMode = GameMode.Idle;
    
    [SerializeField] private GameMode nextMode = GameMode.Idle;
    [SerializeField] private string nextId = "";

    public void ChangeGameMode(GameMode beforeMode, GameMode afterMode, string id)
    {
        ClickSystem.ClickSystem.GetInstance().DisableClick();
        nextId = id;

        switch (beforeMode,afterMode)
        {
            case (GameMode.ChapterStart,GameMode.StoryBoard):
                StoryBoardSwitchEffectManager.GetInstance().SwitchOffEffect(ChapterStart_StoryBoard);
                break;
            
            case (GameMode.StoryBoard,GameMode.Research):
                StoryBoardSwitchEffectManager.GetInstance().SwitchOffEffect(StoryBoard_Research);
                break;
            
            // case GameMode.Research:
            //     EndGameModeCallBack();
            //     break;
            //
            // case GameMode.Idle:
            //     // for test
            //     StoryBoardSwitchEffectManager.GetInstance().SwitchOffEffect(EndGameModeCallBack);
            //     //ChangeGameModeToIdle();
            //     break;
        }
    }
    
    private void ChapterStart_StoryBoard()
    {
        Debug.Log("ChapterStart_StoryBoard");
        storyBoardMode.SetActive(false);
        researchMode.SetActive(false);
        
        storyBoardMode.SetActive(true);
        ClickSystem.ClickSystem.GetInstance().SetClickMode(ClickMode.StoryBoard);
        StoryBoardManager.GetInstance().SetNextStoryBoard(nextId);
        StoryBoardManager.GetInstance().SetStoryBoard();
        StoryBoardSwitchEffectManager.GetInstance().SwitchOnEffect(ChapterStart_StoryBoardCallBack);
    }

    private void ChapterStart_StoryBoardCallBack()
    {
        Debug.Log("StoryBoardSwitchOnCallBack");
        ClickSystem.ClickSystem.GetInstance().EnableClick();
        ResetNextData();
    }

    private void StoryBoard_Research()
    {
        Debug.Log("StoryBoard_Research");
        storyBoardMode.SetActive(false);
        researchMode.SetActive(false);
        
        Debug.Log("ChangeGameModeToResearch");
        researchMode.SetActive(true);
        ClickSystem.ClickSystem.GetInstance().SetClickMode(ClickMode.Research);
        ResearchManager.GetInstance().SetResearch("R001");
        ClickSystem.ClickSystem.GetInstance().EnableClick();
    }
    
    
    private void EndGameModeCallBack()
    {
        Debug.Log("EndGameModeCallBack");
        storyBoardMode.SetActive(false);
        researchMode.SetActive(false);
        currentMode = nextMode;
        switch (nextMode)
        {
            case GameMode.StoryBoard:
                //C//hangeGameModeToStoryBoard();
                break;
            
            case GameMode.Research:
                ChangeGameModeToResearch();
                break;
        }
    }

    private void ChangeGameModeToResearch()
    {
        Debug.Log("ChangeGameModeToResearch");
        researchMode.SetActive(true);
        ClickSystem.ClickSystem.GetInstance().SetClickMode(ClickMode.Research);
        ResearchManager.GetInstance().SetResearch("R001");
        
        
        ClickSystem.ClickSystem.GetInstance().EnableClick();
    }
    

    private void ResetNextData()
    {
        nextMode = GameMode.Idle;
        nextId = "";
    }

    public void Update()
    {
        //for test
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeGameMode(GameMode.ChapterStart,GameMode.StoryBoard,"S0000");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //ChangeGameMode(GameMode.Research, "");
        }
    }
    
}
