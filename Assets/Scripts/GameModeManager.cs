using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public enum GameMode
{
    StoryBoard,
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

    public void ChangeGameMode(GameMode mode,string id)
    {
        nextMode = mode;
        nextId = id;
        
        ClickSystem.GetInstance().DisableClick();
        
        switch (currentMode)
        {
            case GameMode.StoryBoard:
                StoryBoardSwitchEffectManager.GetInstance().SwitchOffEffect(EndGameModeCallBack);
                break;
            
            case GameMode.Research:
                EndGameModeCallBack();
                break;
            
            case GameMode.Idle:
                // for test
                StoryBoardSwitchEffectManager.GetInstance().SwitchOffEffect(EndGameModeCallBack);
                //ChangeGameModeToIdle();
                break;
        }
    }

    private void ChangeGameModeToIdle()
    {
        storyBoardMode.SetActive(false);
        researchMode.SetActive(false);
        ClickSystem.GetInstance().DisableClick();
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
                ChangeGameModeToStoryBoard();
                break;
            
            case GameMode.Research:
                ChangeGameModeToResearch();
                break;
        }
    }
    
    private void ChangeGameModeToStoryBoard ()
    {
        Debug.Log("ChangeGameModeToStoryBoard");
        storyBoardMode.SetActive(true);
        ClickSystem.GetInstance().SetClickMode(ClickMode.StoryBoard);
        StoryBoardManager.GetInstance().SetNextStoryBoard(nextId);
        StoryBoardSwitchEffectManager.GetInstance().SwitchOnEffect(StoryBoardSwitchOnCallBack);
    }
    
    private void StoryBoardSwitchOnCallBack()
    {
        Debug.Log("StoryBoardSwitchOnCallBack");
        StoryBoardManager.GetInstance().SetStoryBoard();
        ClickSystem.GetInstance().EnableClick();
        ResetNextData();
    }

    private void ChangeGameModeToResearch()
    {
        researchMode.SetActive(true);
        ResearchManager.GetInstance().SetResearch("R001");
        ClickSystem.GetInstance().EnableClick();
        
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
            ChangeGameMode(GameMode.StoryBoard,"S0001");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeGameMode(GameMode.Research, "");
        }
    }
    
}
