using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public enum StoryBoardCheckClickPreSet
{
    StoryBoard,
    Menu
}

public class StoryBoardClickSystem : MonoBehaviour
{
    #region Singleton

    private static StoryBoardClickSystem _instance;

    public static StoryBoardClickSystem GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardClickSystem>();
            if (obj != null)
            {
                _instance = obj;
            }
            else
            {
                GameObject gameObject = new GameObject("GameSystem");
                _instance = gameObject.AddComponent<StoryBoardClickSystem>();
            }
        }

        return _instance;
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<StoryBoardClickSystem>();
        if (obj.Length != 1)
        {
            Destroy(gameObject);
        }
    }
    
    #endregion

    public delegate void CheckClickFunc(RaycastHit2D hit);
    CheckClickFunc _checkClickFunc = delegate{  };

    readonly List<CheckClickFunc> _checkClickFuncList= new List<CheckClickFunc>();
    readonly Dictionary<StoryBoardCheckClickPreSet, CheckClickFunc> _checkClickPreSetList = new Dictionary<StoryBoardCheckClickPreSet, CheckClickFunc>();
    
    public void SetCheckClickPreset(CheckClickFunc func, StoryBoardCheckClickPreSet preSet)
    {
        _checkClickPreSetList.Add(preSet,func);
    }
    
    public void SubscribeCheckClick(CheckClickFunc func)
    {
        if (!_checkClickFuncList.Contains(func))
        {
            _checkClickFuncList.Add(func);
        }
    }

    public void SubscribeCheckClick(StoryBoardCheckClickPreSet preSet)
    {
        CheckClickFunc preSetFunc = _checkClickPreSetList[preSet];
        if (!_checkClickFuncList.Contains(preSetFunc))
        {
            _checkClickFuncList.Add(preSetFunc);
        }
    }

    public void UnsubscribeCheckClick(CheckClickFunc func)
    {
        if (_checkClickFuncList.Contains(func))
        {
            _checkClickFuncList.Remove(func);
        }
    }

    public void UnsubscribeCheckClick(StoryBoardCheckClickPreSet preSet)
    {
        CheckClickFunc preSetFunc = _checkClickPreSetList[preSet];
        if (_checkClickFuncList.Contains(preSetFunc))
        {
            _checkClickFuncList.Remove(preSetFunc);
        }
    }
    
    private void MakeCheckClickList()
    {
        _checkClickFunc = delegate { };
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
