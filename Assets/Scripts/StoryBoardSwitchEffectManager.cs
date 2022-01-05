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
            
            if (obj != null)
            {
                _instance = obj;
            }
            else
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
    private Action _effect = delegate { };

    public void SwitchOnEffect(Action func)
    {
        ResetCallBack();
        _effect = SwitchOnEffectAnimation;
        _callBack = func;
    }

    public void SwitchOffEffect(Action func)
    {
        ResetCallBack();
        _effect = SwitchOffEffectAnimation;
        _callBack = func;
    }

    // effect
    private void SwitchOnEffectAnimation()
    {
        Debug.Log("StoryBoard mode witch on");
        _effect = delegate { };
        _callBack();
    }

    // effect
    private void SwitchOffEffectAnimation()
    {
        Debug.Log("StoryBoard mode switch off");
        _effect = delegate { };
        _callBack();
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
