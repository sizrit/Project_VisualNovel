using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuMode
{
    DialogueLog,
    Inventory,
}

public class UI_GameMenuClickSystem : I_ClickSystem
{
    #region Singleton

    private static UI_GameMenuClickSystem _instance;

    public static UI_GameMenuClickSystem GetInstance()
    {
        if (_instance == null)
        {
            _instance=new UI_GameMenuClickSystem();
        }
        return _instance;
    }

    #endregion

    private UI_GameMenuManager _uiGameMenuManager;
    
    private MenuMode _currentMode = MenuMode.Inventory;

    public delegate void CheckClickDelegate(RaycastHit2D hit);
    private CheckClickDelegate _checkClickFunc = delegate {  };
    List<CheckClickDelegate> _checkClickList = new List<CheckClickDelegate>();

    public void SubScribeCheckClickFunc(CheckClickDelegate checkClickDelegate)
    {
        if (!_checkClickList.Contains(checkClickDelegate))
        {
            _checkClickList.Add(checkClickDelegate);
        }
    }

    public void UnSubscribeCheckClick(CheckClickDelegate checkClickDelegate)
    {
        if (_checkClickList.Contains(checkClickDelegate))
        {
            _checkClickList.Remove(checkClickDelegate);
        }
    }

    public void ResetCheckClickList()
    {
        _checkClickList = new List<CheckClickDelegate>();
    }

    private void MakeCheckClickFunc()
    {
        _checkClickFunc= delegate {  };
        foreach (var checkClick in _checkClickList)
        {
            _checkClickFunc += checkClick;
        }
    }

    public void OnEnable()
    {
        _uiGameMenuManager = UI_GameMenuManager.GetInstance();
    }

    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);

            MakeCheckClickFunc();
            
            foreach (var hit in hitList)
            {
                CheckClick(hit);
                _checkClickFunc(hit);
            }
        }
    }

    private void CheckClick(RaycastHit2D hit)
    {
        switch (hit.transform.name)
        {
            case "Inventory":
                if (_currentMode != MenuMode.Inventory)
                {
                    _currentMode = MenuMode.Inventory;
                    _uiGameMenuManager.SetMenuMode(MenuMode.Inventory);
                }
                break;
            
            case "DialogueLog":
                if (_currentMode != MenuMode.DialogueLog)
                {
                    _currentMode = MenuMode.DialogueLog;
                    _uiGameMenuManager.SetMenuMode(MenuMode.DialogueLog);
                }
                break;
            
            case "Back":
                _currentMode = MenuMode.Inventory;
                _uiGameMenuManager.RemoveAllInMain();
                _uiGameMenuManager.RemoveGameMenu();
                ClickSystem.GetInstance().SetClickMode(ClickMode.StoryBoard);
                break;
        }
    }
    
    

    // Update is called once per frame
    public void Update()
    {
        Click();
    }
}
