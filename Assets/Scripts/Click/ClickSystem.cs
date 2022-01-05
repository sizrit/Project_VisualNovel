using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClickMode
{
    StoryBoard,
    Menu,
    Disable
}

public class ClickSystem
{
    #region Singleton

    private static ClickSystem _instance;

    public static ClickSystem GetInstance()
    {
        if (_instance == null)
        {
            _instance=new ClickSystem();
        }
        return _instance;
    }

    #endregion
    
    readonly Dictionary<ClickMode,I_ClickSystem> _clickSystemList = new Dictionary<ClickMode, I_ClickSystem>();
    private ClickMode _currentClickMode = ClickMode.StoryBoard;

    private bool _isClickEnable = true;
    
    public void OnEnable()
    {
        _clickSystemList.Add(ClickMode.StoryBoard,StoryBoardClickSystem.GetInstance());
        _clickSystemList.Add(ClickMode.Menu,UI_GameMenuClickSystem.GetInstance());
        //_clickSystemList.Add(ClickMode.Disable,);
    }

    public void DisableClick()
    {
        _isClickEnable = false;
    }

    public void EnableClick()
    {
        _isClickEnable = true;
    }

    public void SetClickMode(ClickMode mode)
    {
        _currentClickMode = mode;
    }

    private void Click()
    {
        _clickSystemList[_currentClickMode].Click();
    }

    // Update is called once per frame
    public void Update()
    {
        if (_isClickEnable)
        {
            Click();
        }
    }
}
