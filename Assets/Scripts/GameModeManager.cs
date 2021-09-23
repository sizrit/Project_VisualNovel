using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    StoryBoard,
    Adventure,
}

public class GameModeManager
{
    #region Singleton

    private static GameModeManager _instance;

    public static GameModeManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameModeManager();
        }
        return _instance;
    }

    #endregion

    private GameObject _gameModManagerGameObject;
    private GameMode _gameMode = GameMode.StoryBoard;
    
    StoryBoardManager _storyBoardManager = StoryBoardManager.GetInstance();
    
    public void ChangeGameMode(GameMode mode, string stroyBoardId)
    {
        _gameModManagerGameObject = GameObject.Find("GameModeManager");
        _gameModManagerGameObject.transform.GetChild(0).gameObject.SetActive(true);
        
        _gameMode = mode;
        _storyBoardManager.SetNextStoryBoard(stroyBoardId);
        _storyBoardManager.SetStoryBoard();
        
    }

    public void ChangeGameMode()
    {
        _gameModManagerGameObject.transform.GetChild(0).gameObject.SetActive(false);
        
        //_gameMode 
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeGameMode(GameMode.StoryBoard,"S0001");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeGameMode();
        }
    }
    
}
