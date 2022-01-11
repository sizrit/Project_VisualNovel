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
    
    readonly List<Action<RaycastHit2D>> _checkClickFuncList= new List<Action<RaycastHit2D>>();

    private bool _isStoryBoardClickEnable = true;
    
    readonly List<Action<RaycastHit2D>> _uiCheckClickFuncList = new List<Action<RaycastHit2D>>();

    public void SubscribeUiCheckClick(Action<RaycastHit2D> func)
    {
        if (!_uiCheckClickFuncList.Contains(func))
        {
            _uiCheckClickFuncList.Add(func);
        }
    }

    public void SubscribeCheckClick(Action<RaycastHit2D> func)
    {
        if (!_checkClickFuncList.Contains(func))
        {
            _checkClickFuncList.Add(func);
        }
    }

    public void UnsubscribeCheckClick(Action<RaycastHit2D> func)
    {
        if (_checkClickFuncList.Contains(func))
        {
            _checkClickFuncList.Remove(func);
        }
    }
    
    public void EnableStoryBoardCheckClick()
    {
        _isStoryBoardClickEnable = true;
    }
    
    public void DisableStoryBoardCheckClick()
    {
        _isStoryBoardClickEnable = false;
    }

    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickEffectManager.GetInstance().MakeClickEffect();
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
        foreach (var func in _checkClickFuncList)
        {
            func(hit);
        }
        
        if (_isStoryBoardClickEnable)
        {
            StoryBoardCheckClick(hit);
        }
        
        foreach (var func in _uiCheckClickFuncList)
        {
            func(hit);
        }
    }
    
    private void StoryBoardCheckClick(RaycastHit2D hit)
    {
        GameObject dialogueClickZone = GameObject.Find("DialogueClickZone");
        if (dialogueClickZone != null)
        {
            if (hit.transform == dialogueClickZone.transform)
            {
                StoryBoardManager.GetInstance().SetStoryBoard();
            }
        }
    }
}
