using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    #region Singleton

    private static GameSystem _instance;

    public static GameSystem GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<GameSystem>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("GameSystem");
                _instance = gameObject.AddComponent<GameSystem>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<GameSystem>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void Start()
    {
        LoadAllData();
        MakeList();
        Initialize();
        GameSetting();
        //GameModeManager.GetInstance().ChangeGameMode(GameMode.Idle,"");
        //StoryBoardManager.GetInstance().TestRun();
    }

    private void LoadAllData()
    {
        CursorManager.GetInstance().LoadCursor();
        
        StoryBoardDataLoadManager.GetInstance().LoadData();
        StoryBoardSelectionEventDataLoadManager.GetInstance().LoadData();
        DialogueDataLoadManager.GetInstance().LoadJsonData();
        StoryBoardBgLoadManager.GetInstance().LoadBg();
        StoryBoardImageLoadManager.GetInstance().LoadAllPrefabs();
        StoryBoardSwitchManager.GetInstance().LoadData();
        
        ResearchObjectSetLoadManger.GetInstance().LoadAllObjectSet();
        ResearchEdgeController.GetInstance().LoadEdgeControlData();
        ResearchEdgeArrowManager.GetInstance().LoadImage();
    }

    private void MakeList()
    {
        ClueManager.GetInstance().MakeClueList();
        StoryBoardEventManager.GetInstance().MakeEventList();
    }

    private void Initialize()
    {
        ClickSystem.GetInstance().Initialize();
    }

    private void GameSetting()
    {
        UI_GameMenuManager.GetInstance().Hide_UI_GameMenu();
    }

    void Update()
    {
        ClickSystem.GetInstance().Update();
    }
}
