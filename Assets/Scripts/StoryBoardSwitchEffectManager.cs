using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardSwitchEffectManager : MonoBehaviour
{
    #region Singleton
    
    private static StoryBoardSwitchEffectManager _instance;

    public static StoryBoardSwitchEffectManager GetInstance()
    {
        if (_instance == null)
        {
            var obj = FindObjectOfType<StoryBoardSwitchEffectManager>();
            if (obj == null)
            {
                GameObject gameObject = new GameObject("GameModeManager");
                _instance = gameObject.AddComponent<StoryBoardSwitchEffectManager>();
            }
        }
        return _instance;
    }

    #endregion
    
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;

    private Action _callBack = delegate { };
    private Action _effect;
    
    public void SwitchEffectOn()
    {
        _effect = SwitchEffect;
    }
    
    public void SwitchEffectOn(Action func)
    {
        _effect = SwitchEffect;
        _callBack = func;
    }

    private void SwitchEffect()
    {
        _effect = delegate { };
        _callBack();
        ResetCallBack();
    }

    private void ResetCallBack()
    {
        _callBack = delegate { };
    }
    
    public void Update()
    {
        _effect();
    }
}
