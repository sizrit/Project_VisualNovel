using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueTextEffectManager
{
    #region Singleton

    private static DialogueTextEffectManager _instance;
    
    public static DialogueTextEffectManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DialogueTextEffectManager();
        }

        return _instance;
    }

    #endregion
    
    readonly Dictionary<string,EffectDelegate> _effectList =new Dictionary<string,EffectDelegate>();
    delegate void EffectDelegate();

    private EffectDelegate _effectDelegate;

    private GameObject _currentTextGameObject;
    private GameObject _pastTextGameObject;
    
    private void Func0(){}

    private void MakeEffectList()
    {
        EffectDelegate shake = Shake;
        _effectList.Add("S0001",shake);
        _effectList.Add("S0002",shake);
    }
    
    public void SetDialogueTextEffect(string storyBoardIdValue)
    {
        if (_effectList.ContainsKey(storyBoardIdValue))
        {
            _effectDelegate+=_effectList[storyBoardIdValue];
        }
    }

    public void EndEffect()
    {
        _effectDelegate = new EffectDelegate(Func0);
        _currentTextGameObject.transform.localPosition = new Vector3(0,0,0);
        _pastTextGameObject.transform.localPosition = new Vector3(0,0,0);
    }

    private void Shake()
    {
        float randY = Random.Range(-10, 10);
        float randX = Random.Range(-3, 3);
        _currentTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
        _pastTextGameObject.transform.localPosition = new Vector3(randX,randY,0);
    }

    public void OnEnable()
    {
        MakeEffectList();
        _effectDelegate=new EffectDelegate(Func0);
        _currentTextGameObject = GameObject.Find("Dialogue_CurrentText");
        _pastTextGameObject = GameObject.Find("Dialogue_PastText");
    }

    public void Update()
    {
        _effectDelegate();
    }
}
