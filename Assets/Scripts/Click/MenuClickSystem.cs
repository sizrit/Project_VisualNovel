using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuMode
{
    DialogueLog,
    Inventory,
}

public class MenuClickSystem : I_ClickSystem
{
    #region Singleton

    private static MenuClickSystem _instance;

    public static MenuClickSystem GetInstance()
    {
        if (_instance == null)
        {
            _instance=new MenuClickSystem();
        }
        return _instance;
    }

    #endregion

    private PauseMenuManager _pauseMenuManager;
    
    private MenuMode _currentMode = MenuMode.Inventory;

    public void OnEnable()
    {
        _pauseMenuManager = PauseMenuManager.GetInstance();
    }
    
    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);

            foreach (var hit in hitList)
            {
                CheckClick(hit);
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
                    _pauseMenuManager.SetMenuMode(MenuMode.Inventory);
                }
                break;
            
            case "DialogueLog":
                if (_currentMode != MenuMode.DialogueLog)
                {
                    _currentMode = MenuMode.DialogueLog;
                    _pauseMenuManager.SetMenuMode(MenuMode.DialogueLog);
                }
                break;
            
            case "Back":
                _currentMode = MenuMode.Inventory;
                _pauseMenuManager.RemoveAllInMain();
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
