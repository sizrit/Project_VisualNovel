using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class StoryBoardClickSystem : I_ClickSystem
{
    #region Singleton

    private static StoryBoardClickSystem _instance;

    public static StoryBoardClickSystem GetInstance()
    {
        if (_instance == null)
        {
            _instance = new StoryBoardClickSystem();
        }
        return _instance;
    }

    #endregion

    public delegate void CheckClickFunc(RaycastHit2D hit);
    CheckClickFunc _checkClickFunc = delegate{  };
    CheckClickFunc _storyBoardCheckClickFunc =delegate{  };
    readonly List<CheckClickFunc> _checkClickFuncList= new List<CheckClickFunc>();
    readonly List<CheckClickFunc> _uiCheckClickFuncList = new List<CheckClickFunc>();

    public void SubscribeUiCheckClick(CheckClickFunc func)
    {
        if (!_uiCheckClickFuncList.Contains(func))
        {
            _uiCheckClickFuncList.Add(func);
        }
    }
    
    public void SetStoryBoardCheckClick(CheckClickFunc func)
    {
        _storyBoardCheckClickFunc = func;
        _checkClickFuncList.Add(_storyBoardCheckClickFunc);
    }
    
    public void SubscribeCheckClick(CheckClickFunc func)
    {
        if (!_checkClickFuncList.Contains(func))
        {
            _checkClickFuncList.Add(func);
        }
    }

    public void UnsubscribeCheckClick(CheckClickFunc func)
    {
        if (_checkClickFuncList.Contains(func))
        {
            _checkClickFuncList.Remove(func);
        }
    }
    
    public void EnableStoryBoardCheckClick()
    {
        if (!_checkClickFuncList.Contains(_storyBoardCheckClickFunc))
        {
            _checkClickFuncList.Add(_storyBoardCheckClickFunc);
        }
    }
    
    public void DisableStoryBoardCheckClick()
    {
        if (_checkClickFuncList.Contains(_storyBoardCheckClickFunc))
        {
            _checkClickFuncList.Remove(_storyBoardCheckClickFunc);
        }
    }

    private void MakeCheckClickList()
    {
        _checkClickFunc = delegate { };
        foreach (var func in _checkClickFuncList)
        {
            _checkClickFunc += func;
        }

        foreach (var func in _uiCheckClickFuncList)
        {
            _checkClickFunc += func;
        }
    }
    
    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitList = Physics2D.GetRayIntersectionAll(ray);

            MakeCheckClickList();
            
            foreach (var hit in hitList)
            {
                _checkClickFunc(hit);
            }
        }
    }
}
