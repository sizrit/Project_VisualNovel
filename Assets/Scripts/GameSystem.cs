using System;
using System.Collections.Generic;
using DialogueSystem;
using StoryBoardSystem;
using UI_GameMenu;
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
    
    private readonly Dictionary<string, Action> updateFuncList = new Dictionary<string, Action>();

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        LoadAllData();
        Initialize();
        GameSetting();
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
        
        ClueManager.GetInstance().MakeClueList();
        
        DialogueTextEffectManager.GetInstance().Initialize();
    }

    private void Initialize()
    {
        ClickSystem.ClickSystem.GetInstance().Initialize();
        DialogueTextAnimationManager.GetInstance().Initialize();
        DialogueTextEffectManager.GetInstance().Initialize();
    }

    private void GameSetting()
    {
        UI_GameMenuManager.GetInstance().Hide_UI_GameMenu();
    }

    public void SubscribeUpdateFunction(string id, Action action)
    {
        if (!updateFuncList.ContainsKey(id))
        {
            updateFuncList.Add(id, action);
        }
    }
    
    public void UnSubscribeUpdateFunction(string id)
    {
        if (updateFuncList.ContainsKey(id))
        {
            updateFuncList.Remove(id);
        }
    }

    private void MakeUpdateFunction()
    {
        Action targetAction = delegate {};
        foreach (var updateFunc in updateFuncList)
        {
            targetAction += updateFunc.Value;
        }

        targetAction();
    }
    
    void Update()
    {
        MakeUpdateFunction();
        ClickSystem.ClickSystem.GetInstance().Update();
    }
    
}
