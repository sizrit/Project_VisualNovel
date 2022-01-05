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
    
    [SerializeField] private GameMode gameMode = GameMode.Idle;
    
    readonly StoryBoardManager _storyBoardManager = StoryBoardManager.GetInstance();

    private void OnEnable()
    {
        ChangeGameModeToIdle();
    }

    public void ChangeGameModeToIdle()
    {
        storyBoardMode.SetActive(false);
        pointAndClickMode.SetActive(false);
        ClickSystem.GetInstance().DisableClick();
    }

    public void ChangeGameModeToStoryBoard (string storyBoardId)
    {
        storyBoardMode.SetActive(true);
        gameMode = GameMode.StoryBoard;
        ClickSystem.GetInstance().SetClickMode(ClickMode.StoryBoard);
        _storyBoardManager.SetNextStoryBoard(storyBoardId);
        StoryBoardSwitchEffectManager.GetInstance().SwitchEffectOn(StoryBoardSwitchOnCallBack);
    }

    private void StoryBoardSwitchOnCallBack()
    {
        _storyBoardManager.SetStoryBoard();
        ClickSystem.GetInstance().EnableClick();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeGameModeToStoryBoard("S0001");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {

        }
    }
    
}
