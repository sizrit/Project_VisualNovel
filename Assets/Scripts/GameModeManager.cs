using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public enum GameMode
{
    StoryBoard,
    PointAndClick,
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
                GameObject gameObject = new GameObject("GameModeManager");
                _instance = gameObject.AddComponent<GameModeManager>();
            }
        }
        return _instance;
    }

    #endregion

    [SerializeField] private GameObject storyBoardMode;
    [SerializeField] private GameObject pointAndClickMode;
    
    [SerializeField] private GameMode currentMode = GameMode.Idle;
    
    [SerializeField] private GameMode nextMode = GameMode.Idle;
    [SerializeField] private string nextId = "";
    
    readonly StoryBoardManager _storyBoardManager = StoryBoardManager.GetInstance();

    private void OnEnable()
    {
        ChangeGameModeToIdle();
    }

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
            
            case GameMode.PointAndClick:
                break;
            
            case GameMode.Idle:
                // for test
                StoryBoardSwitchEffectManager.GetInstance().SwitchOffEffect(EndGameModeCallBack);
                break;
        }
    }

    private void ChangeGameModeToIdle()
    {
        storyBoardMode.SetActive(false);
        pointAndClickMode.SetActive(false);
        ClickSystem.GetInstance().DisableClick();
    }
    
    private void EndGameModeCallBack()
    {
        Debug.Log("EndGameModeCallBack");
        storyBoardMode.SetActive(false);
        pointAndClickMode.SetActive(false);
        switch (nextMode)
        {
            case GameMode.StoryBoard:
                ChangeGameModeToStoryBoard();
                break;
            
            case GameMode.PointAndClick:
                ChangeGameModeToPointAndClick();
                break;
        }
    }
    
    private void ChangeGameModeToStoryBoard ()
    {
        Debug.Log("ChangeGameModeToStoryBoard");
        storyBoardMode.SetActive(true);
        currentMode = GameMode.StoryBoard;
        ClickSystem.GetInstance().SetClickMode(ClickMode.StoryBoard);
        _storyBoardManager.SetNextStoryBoard(nextId);
        StoryBoardSwitchEffectManager.GetInstance().SwitchOnEffect(StoryBoardSwitchOnCallBack);
    }
    
    private void StoryBoardSwitchOnCallBack()
    {
        Debug.Log("StoryBoardSwitchOnCallBack");
        _storyBoardManager.SetStoryBoard();
        ClickSystem.GetInstance().EnableClick();
        ResetNextData();
    }

    private void ChangeGameModeToPointAndClick()
    {
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

        }
    }
    
}
