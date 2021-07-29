using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;


public class ClickSystem : MonoBehaviour
{
    #region Singleton

    private static ClickSystem _instance;

    public static ClickSystem GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<ClickSystem>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("GameSystem");
                _instance = gameObject.AddComponent<ClickSystem>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<ClickSystem>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }
    
    #endregion

    public delegate void CheckClickFunc(RaycastHit2D hit);
    CheckClickFunc _checkClickFunc = delegate{  };
    CheckClickFunc _storyBoardCheckFunc =delegate{  };
    
    readonly List<CheckClickFunc> _checkClickFuncList= new List<CheckClickFunc>();

    public void SetStoryBoardCheckClickFunc(CheckClickFunc func)
    {
        _storyBoardCheckFunc = func;
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

    public void SubscribeStoryBoardCheckClick()
    {
        if (!_checkClickFuncList.Contains(_storyBoardCheckFunc))
        {
            _checkClickFuncList.Add(_storyBoardCheckFunc);
        }
    }
    
    public void UnsubscribeStoryBoardCheckClick()
    {
        if (_checkClickFuncList.Contains(_storyBoardCheckFunc))
        {
            _checkClickFuncList.Remove(_storyBoardCheckFunc);
        }
    }

    private void MakeCheckClickList()
    {
        _checkClickFunc = delegate(RaycastHit2D hit) { };
        foreach (var func in _checkClickFuncList)
        {
            _checkClickFunc += func;
        }
    }
    

    private void Click()
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

    private void Update()
    {
        Click();
    }
}
